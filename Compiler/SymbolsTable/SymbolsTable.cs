using Compiler.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SymbolsTable.Nodes;
using SymbolsTable.TypeInfos;

namespace SymbolsTable
{
    public partial class SymbolsTable
    {
        protected class ScopeInfo
        {
            public int IndexInParent { get; set; }
            public Dictionary<string, BodyLocalInfo> Locals { get; set; }
            public ScopeInfo(int index)
            {
                IndexInParent = index;
                Locals = new Dictionary<string, BodyLocalInfo>();
            }
            public ScopeInfo(Dictionary<string, BodyLocalInfo> firstDict)
            {
                Locals = firstDict;
            }
        }

        internal BuiltInClassNode ArrayNode { get; set; }
        protected GlobalNode globalNode;
        protected BuiltInClassNode objectNode;
        protected Stack<Dictionary<string, ClassNode>> namespaceStack;
        protected Dictionary<string, ParamLocalInfo> parameters;
        protected Stack<ScopeInfo> scopeStack;
        protected Dictionary<string, ClassNode> defaultCachedClasses;

        public SymbolsTable()
        {
            namespaceStack = new Stack<Dictionary<string, ClassNode>>();
            scopeStack = new Stack<ScopeInfo>();
            defaultCachedClasses = new Dictionary<string, ClassNode>();
            parameters = new Dictionary<string, ParamLocalInfo>();
        }

        public Result AddLocal(string name, ValueTypeInfo type, int index)
        {
            bool isFirst = true;
            foreach (ScopeInfo scope in scopeStack)
            {
                if (scope.Locals.ContainsKey(name))
                {
                    if (isFirst) return Result.LocalAlreadyDefinedInScope;
                    else return Result.LocalDefinedInEnclosingScope;
                }
                isFirst = false;
            }

            if (parameters.ContainsKey(name)) return Result.LocalDefinedInEnclosingScope;
            if (current.Fields.ContainsKey(name)) return Result.LocalShadowsField;

            scopeStack.Peek().Locals.Add(name, new BodyLocalInfo(type, index + 1));

            return Result.Success;
        }

        public Result GetLocal(string name, int statementIndex, out LocalInfo local)
        {
            foreach (ScopeInfo scope in scopeStack)
            {
                if (scope.Locals.TryGetValue(name, out BodyLocalInfo bodyLocal))
                {
                    if (bodyLocal.Index > statementIndex)
                    {
                        local = null;
                        return Result.UsingLocalBeforeDeclaration;
                    }
                    local = bodyLocal;
                    return Result.Success;
                }
                else statementIndex = scope.IndexInParent;
            }

            if (parameters.TryGetValue(name, out ParamLocalInfo paramLocal))
            {
                local = paramLocal;
                return Result.Success;
            }

            local = null;
            return Result.LocalNotFound;
        }

        public Result GetField(string name, bool isStatic, out FieldInfo field)
        {
            if (current.Fields.TryGetValue(name, out field))
            {
                if (!isStatic && field.Modifiers.IsStatic) return Result.InvalidStaticReference;
                else if (isStatic && !field.Modifiers.IsStatic) return Result.InvalidInstanceReference;
                else return Result.Success;
            }
            else return Result.NoSuchMember;
        }

        public Result GetField(string baseIdentifier, string fieldName, int statementIndex, out FieldInfo field)
        {
            if (GetLocal(baseIdentifier, statementIndex, out LocalInfo baseLocal) == Result.Success)
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

        public Result GetField(ClassNode classNode, string fieldName, bool isStatic, out FieldInfo field)
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

        public Result GetMethod(string methodName, IEnumerable<ValueTypeInfo> paramTypes, out MethodInfo method)
            => GetMethod(current, methodName, paramTypes, out method);

        public Result GetMethod(ClassNode classNode, string methodName, IEnumerable<ValueTypeInfo> paramTypes, out MethodInfo method)
        {
            ClassNode current = classNode;
            bool foundMethodWithName = false;
            while (current != null)
            {
                foreach (MethodInfo other in current.Methods)
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

        internal BuiltInClassNode GetBuiltInClass(string name)
        {
            if (globalNode.TryGetChild(name, out SymbolNode child))
            {
                return (BuiltInClassNode)child;
            }
            throw new InvalidOperationException();
        }

        public Result GetClass(string name, out ClassNode classNode)
        {
            if (current.CachedClasses.TryGetValue(name, out classNode))
                return Result.Success;
            else
            {
                NamespaceNode parent = (NamespaceNode)current.Parent;
                while (parent != null)
                {
                    if (parent.TryGetChild(name, out SymbolNode child))
                    {
                        if (child is ClassNode ret)
                        {
                            current.CachedClasses.Add(name, ret);
                            classNode = ret;
                            return Result.Success;
                        }
                        else if (child is NamespaceNode)
                        {
                            classNode = null;
                            return Result.NamespaceUsedAsType;
                        }
                        else throw new InvalidOperationException();
                    }
                    parent = (NamespaceNode)parent.Parent;
                }
                classNode = null;
                return Result.ClassNotFound;
            }
        }
    }
}
