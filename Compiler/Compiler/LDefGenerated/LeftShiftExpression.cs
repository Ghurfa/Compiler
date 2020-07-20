using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class LeftShiftExpression : E4
    {
        public E4 Left { get; private set; }
        public LeftShiftToken LeftShift { get; private set; }
        public E3 Right { get; private set; }
    
        public LeftShiftExpression (E4 left, LeftShiftToken leftShift, E3 right)
        {
            Left = left;
            LeftShift = leftShift;
            Right = right;
        }
    
        public static bool TryParse(ParseStack ps, out LeftShiftExpression leftShiftExpression)
        {
            if (ps.CheckCache(out LeftShiftExpression cached))
            {
                leftShiftExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E4.TryParse(ps, out E4 left)
                && ps.TryParse(out LeftShiftToken leftShift)
                && E3.TryParse(ps, out E3 right))
            {
                leftShiftExpression = new LeftShiftExpression(left, leftShift, right);
                ps.CacheAndPop(leftShiftExpression);
                return true;
            }
            else
            {
                leftShiftExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
