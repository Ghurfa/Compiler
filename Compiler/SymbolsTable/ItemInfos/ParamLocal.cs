using SymbolsTable.TypeInfos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SymbolsTable
{
    public class ParamLocal : Local
    {
        public int Index { get; private set; }
        public ParamLocal(ValueTypeInfo type, int index)
            :base(type)
        {
            Index = index;
        }
    }
}
