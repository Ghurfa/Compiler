using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class PlusAssignExpression : E14
    {
        public E13 Left { get; private set; }
        public PlusAssignToken PlusAssign { get; private set; }
        public E14 Right { get; private set; }
    
        public PlusAssignExpression (E13 left, PlusAssignToken plusAssign, E14 right)
        {
            Left = left;
            PlusAssign = plusAssign;
            Right = right;
        }
    
        public static bool TryParse(ParseStack ps, out PlusAssignExpression plusAssignExpression)
        {
            if (ps.CheckCache(out PlusAssignExpression cached))
            {
                plusAssignExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E13.TryParse(ps, out E13 left)
                && ps.TryParse(out PlusAssignToken plusAssign)
                && E14.TryParse(ps, out E14 right))
            {
                plusAssignExpression = new PlusAssignExpression(left, plusAssign, right);
                ps.CacheAndPop(plusAssignExpression);
                return true;
            }
            else
            {
                plusAssignExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
