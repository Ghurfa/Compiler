using SymbolsTable.TypeInfos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SymbolsTable
{
    public class BodyLocalInfo : LocalInfo
    {
        public int Index { get; private set; }
        public BodyLocalInfo(ValueTypeInfo type, int index)
            : base(type)
        {
            Index = index;
        }
    }
}
