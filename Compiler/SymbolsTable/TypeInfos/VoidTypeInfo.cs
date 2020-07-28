using System;
using System.Collections.Generic;
using System.Text;

namespace SymbolsTable.TypeInfos
{
    public class VoidTypeInfo : TypeInfo
    {
        private static VoidTypeInfo instance = new VoidTypeInfo();
        public static VoidTypeInfo Get() => instance;

        public override bool IsConvertibleTo(TypeInfo other) => true;
    }
}
