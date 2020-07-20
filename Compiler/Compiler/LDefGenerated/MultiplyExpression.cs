using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class MultiplyExpression : E2
    {
        public E2 Left { get; private set; }
        public AsteriskToken Asterisk { get; private set; }
        public E1 Right { get; private set; }
    
        public MultiplyExpression (E2 left, AsteriskToken asterisk, E1 right)
        {
            Left = left;
            Asterisk = asterisk;
            Right = right;
        }
    
        public static bool TryParse(ParseStack ps, out MultiplyExpression multiplyExpression)
        {
            if (ps.CheckCache(out MultiplyExpression cached))
            {
                multiplyExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E2.TryParse(ps, out E2 left)
                && ps.TryParse(out AsteriskToken asterisk)
                && E1.TryParse(ps, out E1 right))
            {
                multiplyExpression = new MultiplyExpression(left, asterisk, right);
                ps.CacheAndPop(multiplyExpression);
                return true;
            }
            else
            {
                multiplyExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
