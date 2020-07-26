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
        private class ScopeInfo
        {
            public int IndexInParent { get; set; }
            public Dictionary<string, (TypeInfos.TypeInfo type, int index)> Locals { get; set; }
            public ScopeInfo(int index)
            {
                IndexInParent = index;
                Locals = new Dictionary<string, (TypeInfos.TypeInfo, int)>();
            }
            public ScopeInfo(Dictionary<string, (TypeInfos.TypeInfo, int)> firstDict)
            {
                Locals = firstDict;
            }
        }
        private GlobalNode globalNode;
        private Stack<Dictionary<string, ClassNode>> namespaceStack;
        private Stack<ScopeInfo> classStack;

        public SymbolsTable()
        {
            globalNode = new GlobalNode();
            globalNode.AddChild(new ClassNode("int", globalNode, Modifiers.Public));
            globalNode.AddChild(new ClassNode("bool", globalNode, Modifiers.Public));
            globalNode.AddChild(new ClassNode("string", globalNode, Modifiers.Public));
            globalNode.AddChild(new ClassNode("char", globalNode, Modifiers.Public));

            namespaceStack = new Stack<Dictionary<string, ClassNode>>();
            EnterNamespace(globalNode);
            classStack = new Stack<ScopeInfo>();
        }

        public void EnterMethod(ParameterListDeclaration parameters)
        {
            EnterScope(1);
            foreach (ParameterDeclaration param in parameters.Parameters)
            {
                AddSymbol(param.Identifier.Text, ValueTypeInfo.Get(param.Type), -1);
            }
        }

        public void ExitMethod() => ExitScope();

        public void EnterScope(int indexInParent)
        {
            classStack.Push(new ScopeInfo(indexInParent));
        }

        public void ExitScope() => classStack.Pop();

        public void AddSymbol(string name, TypeInfos.TypeInfo type, int index)
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
            classStack.Peek().Locals.Add(name, (type, index + 1));
        }

        public TypeInfos.TypeInfo GetSymbol(string name, int statementIndex)
        {
            foreach (ScopeInfo scope in classStack)
            {
                if (scope.Locals.TryGetValue(name, out (TypeInfos.TypeInfo type, int index) ret))
                {
                    if (ret.index > statementIndex) throw new UsingLocalBeforeDeclarationException();
                    return ret.type;
                }
                else statementIndex = scope.IndexInParent;
            }
            throw new InvalidOperationException();
        }
    }
}
