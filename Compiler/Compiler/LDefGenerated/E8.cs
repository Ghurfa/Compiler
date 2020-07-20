using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public abstract class E8 : E9
    {
        public static bool TryParse(ParseStack ps, out E8 e8)
        {
            if (ps.CheckCache(out E8 cached))
            {
                e8 = cached;
                return true;
            }
            
            ps.Save();
            if (BitwiseXorExpression.TryParse(ps, out BitwiseXorExpression bitwiseXorExpression))
            {
                e8 = bitwiseXorExpression;
                ps.Pop();
                return true;
            }
            else if (E7.TryParse(ps, out E7 e7))
            {
                e8 = e7;
                ps.Pop();
                return true;
            }
            else
            {
                e8 = null;
                ps.Restore();
                return false;
            }
        }
    }
}
