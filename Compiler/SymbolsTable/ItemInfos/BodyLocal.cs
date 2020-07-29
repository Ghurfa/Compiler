using SymbolsTable.TypeInfos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SymbolsTable
{
    public class BodyLocal : Local
    {
        public int Index { get; private set; }
        public BodyLocal(ValueTypeInfo type, int index)
            : base(type)
        {
            Index = index;
        }
    }
}
