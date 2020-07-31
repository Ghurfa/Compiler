using Parser.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SymbolsTable.Nodes;
using SymbolsTable.TypeInfos;
using System.Xml.Serialization;

namespace SymbolsTable
{
    public partial class SymbolsTable
    {
        protected GlobalNode globalNode;
        protected LibraryNamespaceNode systemNamespace;
        protected Stack<Dictionary<string, ClassNode>> namespaceStack;

        public SymbolsTable()
        {
            namespaceStack = new Stack<Dictionary<string, ClassNode>>();
        }

        public Result AddLocal(string name, ValueTypeInfo type, int index)
        {
            Scope ancestor = currentScope;
            while (!(ancestor is FunctionScope))
            {
                if (ancestor.Locals.ContainsKey(name))
                {
                    if (ancestor == currentScope) return Result.LocalAlreadyDefinedInScope;
                    else return Result.LocalDefinedInEnclosingScope;
                }
                ancestor = ancestor.ParentScope;
            }

            if (((FunctionScope)ancestor).Parameters.ContainsKey(name)) return Result.LocalDefinedInEnclosingScope;

            currentScope.Locals.Add(name, new BodyLocal(type, index + 1));

            return Result.Success;
        }

        public Result GetLocal(string name, out Local local)
            => GetLocal(name, -1, out local);

        public Result GetLocal(string name, int statementIndex, out Local local)
        {
            Scope ancestor = currentScope;
            while (!(ancestor is FunctionScope))
            {
                if (ancestor.Locals.TryGetValue(name, out BodyLocal bodyLocal))
                {
                    if (bodyLocal.Index > statementIndex && statementIndex >= 0)
                    {
                        local = null;
                        return Result.UsingLocalBeforeDeclaration;
                    }
                    local = bodyLocal;
                    return Result.Success;
                }
                statementIndex = ancestor.IndexInParent;
                ancestor = ancestor.ParentScope;
            }

            if (ancestor.Locals.TryGetValue(name, out BodyLocal funcBodyLocal))
            {
                if (funcBodyLocal.Index > statementIndex && statementIndex >= 0)
                {
                    local = null;
                    return Result.UsingLocalBeforeDeclaration;
                }
                local = funcBodyLocal;
                return Result.Success;
            }
            else if (((FunctionScope)ancestor).Parameters.TryGetValue(name, out ParamLocal paramLocal))
            {
                local = paramLocal;
                return Result.Success;
            }

            local = null;
            return Result.LocalNotFound;
        }

        public Result GetField(string name, bool isStatic, out Field field)
        {
            if (currentClass.Fields.TryGetValue(name, out field))
            {
                if (!isStatic && field.Modifiers.IsStatic) return Result.InvalidStaticReference;
                else if (isStatic && !field.Modifiers.IsStatic) return Result.InvalidInstanceReference;
                else return Result.Success;
            }
            else return Result.NoSuchMember;
        }

        public Result GetField(string baseIdentifier, string fieldName, int statementIndex, out Field field)
        {
            if (GetLocal(baseIdentifier, statementIndex, out Local baseLocal) == Result.Success)
            {
                return GetField(baseLocal.Type.Class, fieldName, false, out field);
            }
            else
            {
                Result getClassResult = GetClass(baseIdentifier, out ClassNode classNode);
                if (getClassResult != Result.Success)
                {
                    field = null;
                    return getClassResult;
                }

                return GetField(classNode, fieldName, true, out field);
            }
        }

        public Result GetField(ClassNode classNode, string fieldName, bool isStatic, out Field field)
        {
            if (classNode.Fields.TryGetValue(fieldName, out field))
            {
                if (!isStatic && field.Modifiers.IsStatic) return Result.InvalidStaticReference;
                else if (isStatic && !field.Modifiers.IsStatic) return Result.InvalidInstanceReference;
                else return Result.Success;
            }
            else
            {
                field = null;
                return Result.NoSuchMember;
            }
        }

        public Result GetMethod(string methodName, IEnumerable<ValueTypeInfo> paramTypes, out Method method)
            => GetMethod(currentClass, methodName, paramTypes, out method);

        public Result GetMethod(ClassNode classNode, string methodName, IEnumerable<ValueTypeInfo> paramTypes, out Method method)
        {
            ClassNode current = classNode;
            bool foundMethodWithName = false;
            while (current != null)
            {
                foreach (Method other in current.Methods)
                {
                    if (other.Name == methodName)
                    {
                        if (Enumerable.SequenceEqual(other.Type.Parameters, paramTypes))
                        {
                            method = other;
                            return Result.Success;
                        }
                        foundMethodWithName = true;
                    }
                }
                current = current.ParentClass;
            }
            method = null;
            if (foundMethodWithName) return Result.NoSuchOverload;
            else return Result.NoSuchMember;
        }

        public Result GetConstructor(ClassNode classNode, IEnumerable<ValueTypeInfo> paramTypes, out Constructor constructor)
        {
            ClassNode current = classNode;
            while (current != null)
            {
                foreach (Constructor other in current.Constructors)
                {
                    if (Enumerable.SequenceEqual(other.ParamTypes, paramTypes))
                    {
                        constructor = other;
                        return Result.Success;
                    }
                }
                current = current.ParentClass;
            }
            constructor = null;
            return Result.NoSuchConstructor;
        }

        public virtual Result GetClass(string name, out ClassNode classNode)
        {
            if (currentClass.CachedClasses.TryGetValue(name, out classNode))
                return Result.Success;
            else return Result.ClassNotFound;
        }

        public Result GetSystemClass(string name, out ClassNode classNode)
        {
            if (systemNamespace.TryGetChild(name, out SymbolNode child))
            {
                if (child is ClassNode classChild)
                {
                    classNode = classChild;
                    return Result.Success;
                }
                else if (child is NamespaceNode _)
                {
                    classNode = null;
                    return Result.NamespaceUsedAsType;
                }
                else throw new InvalidOperationException();
            }
            classNode = null;
            return Result.ClassNotFound;
        }

        public Result GetLibraryClass(System.Type type, out LibraryClassNode node)
        {
            if (systemNamespace.TryGetChild(type, out node))
                return Result.Success;
            else return Result.ClassNotFound;
        }
    }
}
