using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TypeChecker.TypeInfos
{
    class TupleTypeInfo : ValueTypeInfo
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
    }
}
