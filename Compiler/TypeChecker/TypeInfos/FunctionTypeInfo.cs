using System;
using System.Collections.Generic;
using System.Text;

namespace TypeChecker.TypeInfos
{
    class FunctionTypeInfo : TypeInfo
    {
        public TypeInfo ReturnType { get; set; }
        public ValueTypeInfo[] Parameters { get; set; }
        public FunctionTypeInfo(TypeInfo retType, ValueTypeInfo[] parameters)
        {
            ReturnType = retType;
            Parameters = parameters;
        }
    }
}
