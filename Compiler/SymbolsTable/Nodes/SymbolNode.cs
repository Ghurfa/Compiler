using System;
using System.Collections.Generic;
using System.Text;
using SymbolsTable.TypeInfos;

namespace SymbolsTable
{
    public abstract class SymbolNode
    {
        public string Name { get; set; }
        public SymbolNode Parent { get; set; }
        private List<SymbolNode> children;

        protected SymbolNode(string name, SymbolNode parent)
        {
            Name = name;
            Parent = parent;
            children = new List<SymbolNode>();
        }

        public virtual void AddChild(SymbolNode child)
        {
            children.Add(child);
        }

        public virtual bool TryGetChild(string name, out SymbolNode child)
        {
            foreach (SymbolNode childIter in children)
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

        public virtual IEnumerable<SymbolNode> Children
        {
            get
            {
                foreach (SymbolNode child in children)
                {
                    yield return child;
                }
            }
        }
    }
}
