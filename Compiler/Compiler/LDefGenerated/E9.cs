using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public abstract class E9 : E10
    {
        public static bool TryParse(ParseStack ps, out E9 e9)
        {
            if (ps.CheckCache(out E9 cached))
            {
                e9 = cached;
                return true;
            }
            
            ps.Save();
            if (BitwiseOrExpression.TryParse(ps, out BitwiseOrExpression bitwiseOrExpression))
            {
                e9 = bitwiseOrExpression;
                ps.Pop();
                return true;
            }
            else if (E8.TryParse(ps, out E8 e8))
            {
                e9 = e8;
                ps.Pop();
                return true;
            }
            else
            {
                e9 = null;
                ps.Restore();
                return false;
            }
        }
    }
}
