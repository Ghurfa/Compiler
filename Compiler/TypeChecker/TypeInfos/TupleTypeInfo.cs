using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeChecker.TypeInfos
{
    public class TupleTypeInfo : ValueTypeInfo
    {
        private static List<TupleTypeInfo> types = new List<TupleTypeInfo>();

        public static TupleTypeInfo Get(ValueTypeInfo[] tupleTypes)
        {
            foreach(TupleTypeInfo existing in types)
            {
                if(Enumerable.SequenceEqual(existing.Types, tupleTypes))
                {
                    return existing;
                }
            }
            var ret = new TupleTypeInfo(tupleTypes);
            types.Add(ret);
            return ret;
        }

        public ValueTypeInfo[] Types { get; set; }
        private TupleTypeInfo() { }
        private TupleTypeInfo(ValueTypeInfo[] types)
        {
            Types = types;
        }

        public override bool IsConvertibleTo(TypeInfo other)
        {
            if (other is TupleTypeInfo otherType && otherType.Types.Length == Types.Length)
            {
                for(int i = 0; i < Types.Length; i++)
                {
                    if (!Types[i].IsConvertibleTo(otherType.Types[i])) return false;
                }
                return true;
            }
            else return false;
        }
    }
}
