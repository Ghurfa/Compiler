using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public abstract class E6 : E7
    {
        public static bool TryParse(ParseStack ps, out E6 e6)
        {
            if (ps.CheckCache(out E6 cached))
            {
                e6 = cached;
                return true;
            }
            
            ps.Save();
            if (EqualsExpression.TryParse(ps, out EqualsExpression equalsExpression))
            {
                e6 = equalsExpression;
                ps.Pop();
                return true;
            }
            else if (NotEqualsExpression.TryParse(ps, out NotEqualsExpression notEqualsExpression))
            {
                e6 = notEqualsExpression;
                ps.Pop();
                return true;
            }
            else if (E5.TryParse(ps, out E5 e5))
            {
                e6 = e5;
                ps.Pop();
                return true;
            }
            else
            {
                e6 = null;
                ps.Restore();
                return false;
            }
        }
    }
}
