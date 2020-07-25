using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TypeChecker.Exceptions;
using TypeChecker.SymbolNodes;
using TypeChecker.TypeInfos;

namespace TypeChecker
{
    class SymbolsTable
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

        private Stack<ScopeInfo> stack;
        private ClassNode classNode;

        public SymbolsTable(Dictionary<string, TypeInfos.TypeInfo> firstDict, ClassNode node)
        {
            stack = new Stack<ScopeInfo>();

            var newDict = new Dictionary<string, (TypeInfos.TypeInfo, int)>();
            foreach(KeyValuePair<string, TypeInfos.TypeInfo> pair in firstDict)
            {
                newDict.Add(pair.Key, (pair.Value, 0));
            }
            stack.Push(new ScopeInfo(newDict));

            classNode = node;
        }

        public void EnterScope(int indexInParent)
        {
            stack.Push(new ScopeInfo(indexInParent));
        }

        public void ExitScope() => stack.Pop();

        public void AddSymbol(string name, TypeInfos.TypeInfo type, int index)
        {
            bool isFirst = true;
            foreach (ScopeInfo scope in stack)
            {
                if (scope.Locals.ContainsKey(name))
                {
                    if (isFirst) throw new AlreadyDefinedInScopeException();
                    else throw new DefinedInEnclosingScopeException();
                }
                isFirst = false;
            }
            stack.Peek().Locals.Add(name, (type, index + 1));
        }

        public TypeInfos.TypeInfo GetSymbol(string name, int index)
        {
            foreach (ScopeInfo scope in stack)
            {
                if (scope.Locals.TryGetValue(name, out (TypeInfos.TypeInfo type, int index) ret))
                {
                    if (ret.index > index) throw new UsingLocalBeforeDeclarationException();
                    return ret.type;
                }
                else index = scope.IndexInParent;
            }
            throw new InvalidSymbolException();
        }
    }
}
