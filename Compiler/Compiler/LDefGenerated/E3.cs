using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public abstract class E3 : E4
    {
        public static bool TryParse(ParseStack ps, out E3 e3)
        {
            if (ps.CheckCache(out E3 cached))
            {
                e3 = cached;
                return true;
            }
            
            ps.Save();
            if (PlusExpression.TryParse(ps, out PlusExpression plusExpression))
            {
                e3 = plusExpression;
                ps.Pop();
                return true;
            }
            else if (MinusExpression.TryParse(ps, out MinusExpression minusExpression))
            {
                e3 = minusExpression;
                ps.Pop();
                return true;
            }
            else if (E2.TryParse(ps, out E2 e2))
            {
                e3 = e2;
                ps.Pop();
                return true;
            }
            else
            {
                e3 = null;
                ps.Restore();
                return false;
            }
        }
    }
}
