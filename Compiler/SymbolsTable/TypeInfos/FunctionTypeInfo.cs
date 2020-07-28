using System;
using System.Collections.Generic;
using System.Text;

namespace SymbolsTable.TypeInfos
{
    public class FunctionTypeInfo : TypeInfo
    {
        public TypeInfo ReturnType { get; set; }
        public ValueTypeInfo[] Parameters { get; set; }
        public FunctionTypeInfo(TypeInfo retType, ValueTypeInfo[] parameters)
        {
            ReturnType = retType;
            Parameters = parameters;
        }

        public override bool IsConvertibleTo(TypeInfo other) => throw new NotImplementedException();
    }
}
