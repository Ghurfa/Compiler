using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public abstract class E2 : E3
    {
        public static bool TryParse(ParseStack ps, out E2 e2)
        {
            if (ps.CheckCache(out E2 cached))
            {
                e2 = cached;
                return true;
            }
            
            ps.Save();
            if (MultiplyExpression.TryParse(ps, out MultiplyExpression multiplyExpression))
            {
                e2 = multiplyExpression;
                ps.Pop();
                return true;
            }
            else if (DivideExpression.TryParse(ps, out DivideExpression divideExpression))
            {
                e2 = divideExpression;
                ps.Pop();
                return true;
            }
            else if (E1.TryParse(ps, out E1 e1))
            {
                e2 = e1;
                ps.Pop();
                return true;
            }
            else
            {
                e2 = null;
                ps.Restore();
                return false;
            }
        }
    }
}
