using Parser;
using SymbolsTable;
using SymbolsTable.Nodes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    class ClassBuildingInfo
    {
        public TypeBuilder Builder { get; private set; }
        public List<(FieldBuilder field, Expression value)> DefaultedFields { get; private set; }
        public ReadOnlyDictionary<string, ClassNode> ReferencedClasses { get; set; }
        public ClassBuildingInfo(TypeBuilder builder)
        {
            Builder = builder;
            DefaultedFields = new List<(FieldBuilder field, Expression value)>();
        }
    }
}
