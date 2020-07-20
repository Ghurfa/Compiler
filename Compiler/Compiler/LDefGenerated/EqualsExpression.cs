using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class EqualsExpression : E6
    {
        public E6 Left { get; private set; }
        public EqualsToken Equals { get; private set; }
        public E5 Right { get; private set; }
    
        public EqualsExpression (E6 left, EqualsToken equals, E5 right)
        {
            Left = left;
            Equals = equals;
            Right = right;
        }
    
        public static bool TryParse(ParseStack ps, out EqualsExpression equalsExpression)
        {
            if (ps.CheckCache(out EqualsExpression cached))
            {
                equalsExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E6.TryParse(ps, out E6 left)
                && ps.TryParse(out EqualsToken equals)
                && E5.TryParse(ps, out E5 right))
            {
                equalsExpression = new EqualsExpression(left, equals, right);
                ps.CacheAndPop(equalsExpression);
                return true;
            }
            else
            {
                equalsExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
