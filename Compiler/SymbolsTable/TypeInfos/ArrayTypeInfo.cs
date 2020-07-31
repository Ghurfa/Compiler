using SymbolsTable.Nodes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SymbolsTable.TypeInfos
{
    public class ArrayTypeInfo : ValueTypeInfo
    {
        public ValueTypeInfo BaseType { get; set; }

        public ArrayTypeInfo(SymbolsTable table, ValueTypeInfo baseType, ClassNode arrayClass)
            :base(arrayClass)
        {
            BaseType = baseType;
        }

        public override bool IsConvertibleTo(TypeInfo other)
        {
            if (other is ArrayTypeInfo otherArrType)
            {
                return BaseType.IsConvertibleTo(otherArrType.BaseType);
            }
            else return false;
        }
    }
}
