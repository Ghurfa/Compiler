using System;
using System.Collections.Generic;
using System.Text;

namespace TypeChecker.TypeInfos
{
    abstract class TypeInfo
    {
        public override bool Equals(object obj)
        {
            switch(this)
            {
                case ClassTypeInfo classType: return classType.Equals(obj as ClassTypeInfo);
                case FunctionTypeInfo funcType: return funcType.Equals(obj as FunctionTypeInfo);
                case ValueTypeInfo valueType: return valueType.Equals(obj as ValueTypeInfo);
                default: throw new NotImplementedException();
            }
        }
    }
}
