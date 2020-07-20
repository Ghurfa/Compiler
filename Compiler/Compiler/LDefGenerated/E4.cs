using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public abstract class E4 : E5
    {
        public static bool TryParse(ParseStack ps, out E4 e4)
        {
            if (ps.CheckCache(out E4 cached))
            {
                e4 = cached;
                return true;
            }
            
            ps.Save();
            if (LeftShiftExpression.TryParse(ps, out LeftShiftExpression leftShiftExpression))
            {
                e4 = leftShiftExpression;
                ps.Pop();
                return true;
            }
            else if (RightShiftExpression.TryParse(ps, out RightShiftExpression rightShiftExpression))
            {
                e4 = rightShiftExpression;
                ps.Pop();
                return true;
            }
            else if (E3.TryParse(ps, out E3 e3))
            {
                e4 = e3;
                ps.Pop();
                return true;
            }
            else
            {
                e4 = null;
                ps.Restore();
                return false;
            }
        }
    }
}
