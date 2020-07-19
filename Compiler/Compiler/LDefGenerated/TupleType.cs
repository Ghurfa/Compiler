using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class TupleType : NonArrayType
    {
        public OpenPerenToken OpenPeren { get; private set; }
        public TypeWithComma[] NonLastTypes { get; private set; }
        public Type Type { get; private set; }
        public ClosePerenToken ClosePeren { get; private set; }
    
        public TupleType (OpenPerenToken openPeren, TypeWithComma[] nonLastTypes, Type type, ClosePerenToken closePeren)
        {
            OpenPeren = openPeren;
            NonLastTypes = nonLastTypes;
            Type = type;
            ClosePeren = closePeren;
        }
    
        public static bool TryParse(ParseStack ps, out TupleType tupleType)
        {
            if (ps.CheckCache(out TupleType cached))
            {
                tupleType = cached;
                return true;
            }
            
            ps.Save();
            if (ps.TryParse(out OpenPerenToken openPeren)
                && TryParseNonLastTypes(out TypeWithComma[] nonLastTypes)
                && Type.TryParse(ps, out Type type)
                && ps.TryParse(out ClosePerenToken closePeren))
            {
                tupleType = new TupleType(openPeren, nonLastTypes, type, closePeren);
                ps.CacheAndPop(tupleType);
                return true;
            }
            else
            {
                tupleType = null;
                ps.Restore();
                return false;
            }
        }
    
        private static bool TryParseNonLastTypes(ParseStack ps, out TypeWithComma[] nonLastTypes)
        {
            List<TypeWithComma> items = new List<TypeWithComma
            while (TypeWithComma.TryParse(ps, out TypeWithComma typeWithComma)
            {
            items.Add(typeWithComma);
            }
            nonLastTypes = items.ToArray();
            return true;
        }
    }
}
