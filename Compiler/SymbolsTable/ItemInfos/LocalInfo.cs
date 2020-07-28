using SymbolsTable.TypeInfos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SymbolsTable
{
    public class LocalInfo
    {
        public ValueTypeInfo Type { get; private set; }
        public LocalInfo(ValueTypeInfo type)
        {
            Type = type;
        }
    }
}
