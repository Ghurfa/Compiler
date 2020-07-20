using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public abstract class E7 : E8
    {
        public static bool TryParse(ParseStack ps, out E7 e7)
        {
            if (ps.CheckCache(out E7 cached))
            {
                e7 = cached;
                return true;
            }
            
            ps.Save();
            if (BitwiseAndExpression.TryParse(ps, out BitwiseAndExpression bitwiseAndExpression))
            {
                e7 = bitwiseAndExpression;
                ps.Pop();
                return true;
            }
            else if (E6.TryParse(ps, out E6 e6))
            {
                e7 = e6;
                ps.Pop();
                return true;
            }
            else
            {
                e7 = null;
                ps.Restore();
                return false;
            }
        }
    }
}
