using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class DivideAssignExpression : E14
    {
        public E13 Left { get; private set; }
        public DivideAssignToken DivideAssign { get; private set; }
        public E14 Right { get; private set; }
    
        public DivideAssignExpression (E13 left, DivideAssignToken divideAssign, E14 right)
        {
            Left = left;
            DivideAssign = divideAssign;
            Right = right;
        }
    
        public static bool TryParse(ParseStack ps, out DivideAssignExpression divideAssignExpression)
        {
            if (ps.CheckCache(out DivideAssignExpression cached))
            {
                divideAssignExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E13.TryParse(ps, out E13 left)
                && ps.TryParse(out DivideAssignToken divideAssign)
                && E14.TryParse(ps, out E14 right))
            {
                divideAssignExpression = new DivideAssignExpression(left, divideAssign, right);
                ps.CacheAndPop(divideAssignExpression);
                return true;
            }
            else
            {
                divideAssignExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
