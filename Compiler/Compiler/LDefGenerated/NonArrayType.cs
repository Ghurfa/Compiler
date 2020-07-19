using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public abstract class NonArrayType : Type
    {
        public static bool TryParse(ParseStack ps, out NonArrayType nonArrayType)
        {
            if (ps.CheckCache(out NonArrayType cached))
            {
                nonArrayType = cached;
                return true;
            }
            
            ps.Save();
            if (BuiltInType.TryParse(ps, out BuiltInType builtInType)
            {
                nonArrayType = builtInType;
                ps.Pop();
                return true;
            }
            else if (QualifiedIdentifierType.TryParse(ps, out QualifiedIdentifierType qualifiedIdentifierType)
            {
                nonArrayType = qualifiedIdentifierType;
                ps.Pop();
                return true;
            }
            else if (TupleType.TryParse(ps, out TupleType tupleType)
            {
                nonArrayType = tupleType;
                ps.Pop();
                return true;
            }
            else
            {
                nonArrayType = null;
                ps.Restore();
                return false;
            }
        }
    }
}
