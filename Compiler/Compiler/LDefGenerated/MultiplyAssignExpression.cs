using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class MultiplyAssignExpression : E14
    {
        public E13 Left { get; private set; }
        public MultiplyAssignToken MultiplyAssign { get; private set; }
        public E14 Right { get; private set; }
    
        public MultiplyAssignExpression (E13 left, MultiplyAssignToken multiplyAssign, E14 right)
        {
            Left = left;
            MultiplyAssign = multiplyAssign;
            Right = right;
        }
    
        public static bool TryParse(ParseStack ps, out MultiplyAssignExpression multiplyAssignExpression)
        {
            if (ps.CheckCache(out MultiplyAssignExpression cached))
            {
                multiplyAssignExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E13.TryParse(ps, out E13 left)
                && ps.TryParse(out MultiplyAssignToken multiplyAssign)
                && E14.TryParse(ps, out E14 right))
            {
                multiplyAssignExpression = new MultiplyAssignExpression(left, multiplyAssign, right);
                ps.CacheAndPop(multiplyAssignExpression);
                return true;
            }
            else
            {
                multiplyAssignExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
