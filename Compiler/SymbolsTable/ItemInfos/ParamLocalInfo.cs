using SymbolsTable.TypeInfos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SymbolsTable
{
    public class ParamLocalInfo : LocalInfo
    {
        public int Index { get; private set; }
        public ParamLocalInfo(ValueTypeInfo type, int index)
            :base(type)
        {
            Index = index;
        }
    }
}
