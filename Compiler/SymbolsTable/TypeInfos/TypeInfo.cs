using System;
using System.Collections.Generic;
using System.Text;

namespace SymbolsTable.TypeInfos
{
    public abstract class TypeInfo
    {
        public abstract bool IsConvertibleTo(TypeInfo other);
    }
}
