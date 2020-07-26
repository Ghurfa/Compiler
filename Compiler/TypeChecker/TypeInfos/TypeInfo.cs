using System;
using System.Collections.Generic;
using System.Text;

namespace TypeChecker.TypeInfos
{
    abstract class TypeInfo
    {
        public abstract bool IsConvertibleTo(TypeInfo other);
    }
}
