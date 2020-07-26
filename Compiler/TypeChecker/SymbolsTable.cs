using Compiler.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TypeChecker.Exceptions;
using TypeChecker.SymbolNodes;
using TypeChecker.TypeInfos;

namespace TypeChecker
{
    partial class SymbolsTable
    {
        private struct LocalInfo
        {
            public ValueTypeInfo Type { get; set; }
            public int Index { get; set; }
            public bool IsInstanceField { get; set; }
            public LocalInfo(ValueTypeInfo type, int index, bool isInstanceField)
            {
                Type = type;
                Index = index;
                IsInstanceField = isInstanceField;
            }
        }

        private class ScopeInfo
        {
            public int IndexInParent { get; set; }
            public Dictionary<string, LocalInfo> Locals { get; set; }
            public ScopeInfo(int index)
            {
                IndexInParent = index;
                Locals = new Dictionary<string, LocalInfo>();
            }
            public ScopeInfo(Dictionary<string, LocalInfo> firstDict)
            {
                Locals = firstDict;
            }
        }
        private GlobalNode globalNode;
        private Stack<Dictionary<string, ClassNode>> namespaceStack;
        private Stack<ScopeInfo> classStack;
        private Dictionary<string, ClassNode> defaultCachedClasses;

        public SymbolsTable()
        {
            namespaceStack = new Stack<Dictionary<string, ClassNode>>();
            classStack = new Stack<ScopeInfo>();
            defaultCachedClasses = new Dictionary<string, ClassNode>();
        }

        public void EnterMethod(ParameterListDeclaration parameters)
        {
            EnterScope(1);
            foreach (ParameterDeclaration param in parameters.Parameters)
            {
                AddLocal(param.Identifier.Text, ValueTypeInfo.Get(this, param.Type), -1);
            }
        }

        public void ExitMethod() => ExitScope();

        public void EnterScope(int indexInParent)
        {
            classStack.Push(new ScopeInfo(indexInParent));
        }

        public void ExitScope() => classStack.Pop();

        public void AddLocal(string name, ValueTypeInfo type, int index)
        {
            bool isFirst = true;
            foreach (ScopeInfo scope in classStack)
            {
                if (scope.Locals.ContainsKey(name))
                {
                    if (isFirst) throw new AlreadyDefinedInScopeException();
                    else throw new DefinedInEnclosingScopeException();
                }
                isFirst = false;
            }
            classStack.Peek().Locals.Add(name, new LocalInfo(type, index + 1, false));
        }

        public bool TryGetSymbol(string name, int statementIndex, out ValueTypeInfo type, bool requireStatic)
        {
            foreach (ScopeInfo scope in classStack)
            {
                if (scope.Locals.TryGetValue(name, out LocalInfo ret))
                {
                    if (ret.Index > statementIndex) throw new UsingLocalBeforeDeclarationException();
                    if (requireStatic && ret.IsInstanceField) throw new InvalidInstanceReferenceException();
                    type = ret.Type;
                    return true;
                }
                else statementIndex = scope.IndexInParent;
            }
            type = null;
            return false;
        }

        public ValueTypeInfo GetFieldType(string baseIdentifier, string fieldName, int statementIndex)
        {
            if (TryGetSymbol(baseIdentifier, statementIndex, out ValueTypeInfo localType, false))
            {
                return GetInstanceFieldType(localType, fieldName);
            }
            else
            {
                ClassNode firstClass = GetClass(baseIdentifier);
                if (firstClass.Fields.TryGetValue(fieldName, out FieldNode field))
                {
                    if (!field.Modifiers.IsStatic)
                        throw new ObjectReferenceRequiredException();
                    else return field.Type;
                }
                else throw new NoSuchMemberException();
            }
        }

        public ValueTypeInfo GetInstanceFieldType(ValueTypeInfo baseType, string fieldName)
        {
            if (baseType.Class.Fields.TryGetValue(fieldName, out FieldNode ret))
            {
                if (ret.Modifiers.IsStatic)
                    throw new InvalidStaticReferenceException();
                else return ret.Type;
            }
            else throw new NoSuchMemberException();
        }

        public BuiltInClassNode GetBuiltInClass(string name)
        {
            if (globalNode.TryGetChild(name, out SymbolNode child))
            {
                return (BuiltInClassNode)child;
            }
            throw new ClassNotFoundException();
        }

        public ClassNode GetClass(string name)
        {
            if (current.CachedClasses.TryGetValue(name, out ClassNode classNode))
                return classNode;
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
                            return ret;
                        }
                        else if (child is NamespaceNode _) throw new NamespaceUsedAsTypeException();
                        else throw new InvalidOperationException();
                    }
                    parent = (NamespaceNode)parent.Parent;
                }
                throw new ClassNotFoundException();
            }
        }
    }
}
