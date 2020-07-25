using System;
using System.Collections.Generic;
using System.Text;
using TypeChecker.Exceptions;
using TypeChecker.TypeInfos;

namespace TypeChecker
{
    class SymbolsTable
    {
        private Stack<Dictionary<string, TypeInfo>> stack;

        public SymbolsTable(Dictionary<string, TypeInfo> firstDict)
        {
            stack = new Stack<Dictionary<string, TypeInfo>>();
            stack.Push(new Dictionary<string, TypeInfo>(firstDict));
        }

        public void EnterScope()
        {
            stack.Push(new Dictionary<string, TypeInfo>());
        }

        public void ExitScope() => stack.Pop();

        public void AddSymbol(string name, TypeInfo type)
        {
            foreach (Dictionary<string, TypeInfo> dict in stack)
            {
                if (dict.ContainsKey(name)) throw new DuplicateLocalException();
            }
            stack.Peek().Add(name, type);
        }

        public TypeInfo GetSymbol(string name)
        {
            foreach (Dictionary<string, TypeInfo> dict in stack)
            {
                if (dict.TryGetValue(name, out TypeInfo ret)) return ret;
            }
            throw new InvalidSymbolException();
        }
    }
}
