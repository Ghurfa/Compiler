using System;
using System.Collections.Generic;
using System.Text;
using TypeChecker.TypeInfos;

namespace TypeChecker
{
    class SymbolNode
    {
        public string Name { get; set; }
        public SymbolNode Parent { get; set; }
        public List<SymbolNode> Children { get; set; }

        public SymbolNode(string name, SymbolNode parent)
        {
            Name = name;
            Parent = parent;
            Children = new List<SymbolNode>();
        }

        public void AddChild(SymbolNode child)
        {
            Children.Add(child);
        }

        public bool TryGetChild(string name, out SymbolNode child)
        {
            foreach (SymbolNode childIter in Children)
            {
                if (childIter.Name == name)
                {
                    child = childIter;
                    return true;
                }
            }
            child = null;
            return false;
        }
    }
}
