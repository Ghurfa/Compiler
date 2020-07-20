using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class DeclAssignExpression : E14
    {
        public E13 Left { get; private set; }
        public DeclAssignToken DeclAssign { get; private set; }
        public E14 Right { get; private set; }
    
        public DeclAssignExpression (E13 left, DeclAssignToken declAssign, E14 right)
        {
            Left = left;
            DeclAssign = declAssign;
            Right = right;
        }
    
        public static bool TryParse(ParseStack ps, out DeclAssignExpression declAssignExpression)
        {
            if (ps.CheckCache(out DeclAssignExpression cached))
            {
                declAssignExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E13.TryParse(ps, out E13 left)
                && ps.TryParse(out DeclAssignToken declAssign)
                && E14.TryParse(ps, out E14 right))
            {
                declAssignExpression = new DeclAssignExpression(left, declAssign, right);
                ps.CacheAndPop(declAssignExpression);
                return true;
            }
            else
            {
                declAssignExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
