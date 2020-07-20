using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class RightShiftExpression : E4
    {
        public E4 Left { get; private set; }
        public RightShiftToken RightShift { get; private set; }
        public E3 Right { get; private set; }
    
        public RightShiftExpression (E4 left, RightShiftToken rightShift, E3 right)
        {
            Left = left;
            RightShift = rightShift;
            Right = right;
        }
    
        public static bool TryParse(ParseStack ps, out RightShiftExpression rightShiftExpression)
        {
            if (ps.CheckCache(out RightShiftExpression cached))
            {
                rightShiftExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E4.TryParse(ps, out E4 left)
                && ps.TryParse(out RightShiftToken rightShift)
                && E3.TryParse(ps, out E3 right))
            {
                rightShiftExpression = new RightShiftExpression(left, rightShift, right);
                ps.CacheAndPop(rightShiftExpression);
                return true;
            }
            else
            {
                rightShiftExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
