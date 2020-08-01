using Parser.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SymbolsTable.Nodes;
using SymbolsTable.TypeInfos;
using System.Xml.Serialization;
using Parser;

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

        public Result GetLocal(string name, out Local local)
        {
            Scope ancestor = currentScope;
            while (!(ancestor is FunctionScope))
            {
                if (ancestor.Locals.TryGetValue(name, out BodyLocal bodyLocal))
                {
                    local = bodyLocal;
                    return Result.Success;
                }
                ancestor = ancestor.ParentScope;
            }

            if (ancestor.Locals.TryGetValue(name, out BodyLocal funcBodyLocal))
            {
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

        public virtual Result GetField(PrimaryExpression fieldExpr, out Field field)
        {
            if (currentScope.ReferencedFields.TryGetValue(fieldExpr, out field))
                return Result.Success;
            else return Result.NoSuchField;
        }

        public virtual Result GetConstructor(NewObjectExpression newObjExpr, out Constructor constructor)
        {
            if (currentScope.ReferencedConstructors.TryGetValue(newObjExpr, out constructor))
                return Result.Success;
            else return Result.NoSuchConstructor;
        }

        public Result GetMethod(MethodCallExpression methodCall, out Method method)
        {
            if (currentScope.ReferencedMethods.TryGetValue(methodCall, out method))
                return Result.Success;
            else return Result.NoSuchMethod;
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
            else if (type.IsArray)
            {
                node = new LibraryClassNode(this, systemNamespace, type);
                systemNamespace.AddChild(node);
                return Result.Success;
            }
            else return Result.ClassNotFound;
        }
    }
}
