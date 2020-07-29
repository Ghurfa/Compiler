using SymbolsTable.TypeInfos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SymbolsTable
{
    public class Local
    {
        public ValueTypeInfo Type { get; private set; }
        public Local(ValueTypeInfo type)
        {
            Type = type;
        }
    }
}
