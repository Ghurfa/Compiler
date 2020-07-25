using Compiler.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace TypeChecker.TypeInfos
{
    class ValueTypeInfo : TypeInfo
    {
        private static Dictionary<string, ValueTypeInfo> types = new Dictionary<string, ValueTypeInfo>();

        public static ValueTypeInfo Get(string name)
        {
            if (types.TryGetValue(name, out ValueTypeInfo existing)) return existing;
            else
            {
                var ret = new ValueTypeInfo(name);
                types.Add(name, ret);
                return ret;
            }
        }

        public static ValueTypeInfo Get(Compiler.SyntaxTreeItems.Type type)
        {
            switch (type)
            {
                case TupleType tupleType:
                    {
                        var subTypes = new ValueTypeInfo[tupleType.Items.Length];
                        for(int i = 0; i < subTypes.Length; i++)
                        {
                            subTypes[i] = Get(tupleType.Items[i].Type);
                        }
                        return TupleTypeInfo.Get(subTypes);
                    }
                default: return Get(type.ToString());
            }
        }

        public string Type { get; set; }
        protected ValueTypeInfo() {}
        private ValueTypeInfo(string type) { Type = type; }

        public override string ToString() => Type;
    }
}
