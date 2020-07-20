using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public abstract class E5 : E6
    {
        public static bool TryParse(ParseStack ps, out E5 e5)
        {
            if (ps.CheckCache(out E5 cached))
            {
                e5 = cached;
                return true;
            }
            
            ps.Save();
            if (GreaterThanExpression.TryParse(ps, out GreaterThanExpression greaterThanExpression))
            {
                e5 = greaterThanExpression;
                ps.Pop();
                return true;
            }
            else if (LessThanExpression.TryParse(ps, out LessThanExpression lessThanExpression))
            {
                e5 = lessThanExpression;
                ps.Pop();
                return true;
            }
            else if (GreaterThanOrEqualToExpression.TryParse(ps, out GreaterThanOrEqualToExpression greaterThanOrEqualToExpression))
            {
                e5 = greaterThanOrEqualToExpression;
                ps.Pop();
                return true;
            }
            else if (LessThanOrEqualToExpression.TryParse(ps, out LessThanOrEqualToExpression lessThanOrEqualToExpression))
            {
                e5 = lessThanOrEqualToExpression;
                ps.Pop();
                return true;
            }
            else if (E4.TryParse(ps, out E4 e4))
            {
                e5 = e4;
                ps.Pop();
                return true;
            }
            else
            {
                e5 = null;
                ps.Restore();
                return false;
            }
        }
    }
}
