using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SymbolsTable.Nodes
{
    public class LibraryNamespaceNode : NamespaceNode
    {
        public LibraryNamespaceNode(SymbolsTable table, GlobalNode globalNode)
            :base("System", globalNode)
        {
            var mscorlib = typeof(string).Assembly;
            var types = mscorlib.GetTypes()
                                .Where(t => t.Namespace == "System");
            foreach(var type in types)
            {
                if (char.IsLetter(type.Name.First()))
                {
                    AddChild(new LibraryClassNode(table, this, type));
                }
            }
        }

        public bool TryGetChild(Type type, out LibraryClassNode node)
        {
            foreach (var child in Children)
            {
                if (child is LibraryClassNode libClassNode)
                {
                    if (libClassNode.Type == type)
                    {
                        node = libClassNode;
                        return true;
                    }
                }
            }
            node = null;
            return false;
        }
    }
}
