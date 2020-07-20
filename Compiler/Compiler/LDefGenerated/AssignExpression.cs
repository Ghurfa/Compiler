using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class AssignExpression : E14
    {
        public E13 Left { get; private set; }
        public AssignToken Assign { get; private set; }
        public E14 Right { get; private set; }
    
        public AssignExpression (E13 left, AssignToken assign, E14 right)
        {
            Left = left;
            Assign = assign;
            Right = right;
        }
    
        public static bool TryParse(ParseStack ps, out AssignExpression assignExpression)
        {
            if (ps.CheckCache(out AssignExpression cached))
            {
                assignExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E13.TryParse(ps, out E13 left)
                && ps.TryParse(out AssignToken assign)
                && E14.TryParse(ps, out E14 right))
            {
                assignExpression = new AssignExpression(left, assign, right);
                ps.CacheAndPop(assignExpression);
                return true;
            }
            else
            {
                assignExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
