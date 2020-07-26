using System;
using System.Collections.Generic;
using System.Text;

namespace TypeChecker.TypeInfos
{
    abstract class TypeInfo
    {
        public bool IsConvertibleTo(TypeInfo other)
        {
            if (this is ValueTypeInfo thisValType && other is ValueTypeInfo otherValType)
            {
                while(thisValType != null)
                {
                    if (thisValType == otherValType) return true;
                    thisValType = ValueTypeInfo.Get(thisValType.Class.ParentClass);
                }
                return false;
            }
            else return this == other;
        }
    }
}
