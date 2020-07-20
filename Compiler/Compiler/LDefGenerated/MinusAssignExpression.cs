using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class MinusAssignExpression : E14
    {
        public E13 Left { get; private set; }
        public MinusAssignToken MinusAssign { get; private set; }
        public E14 Right { get; private set; }
    
        public MinusAssignExpression (E13 left, MinusAssignToken minusAssign, E14 right)
        {
            Left = left;
            MinusAssign = minusAssign;
            Right = right;
        }
    
        public static bool TryParse(ParseStack ps, out MinusAssignExpression minusAssignExpression)
        {
            if (ps.CheckCache(out MinusAssignExpression cached))
            {
                minusAssignExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E13.TryParse(ps, out E13 left)
                && ps.TryParse(out MinusAssignToken minusAssign)
                && E14.TryParse(ps, out E14 right))
            {
                minusAssignExpression = new MinusAssignExpression(left, minusAssign, right);
                ps.CacheAndPop(minusAssignExpression);
                return true;
            }
            else
            {
                minusAssignExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
