using System;
using System.Collections.Generic;
using System.Text;

namespace TypeChecker.TypeInfos
{
    class ValueTypeInfo : TypeInfo
    {
        public string Type { get; set; }

        public ValueTypeInfo(string type)
        {
            Type = type;
        }

        public override bool Equals(object obj)
        {
            return (obj as ValueTypeInfo)?.Type == Type;
        }
    }
}
