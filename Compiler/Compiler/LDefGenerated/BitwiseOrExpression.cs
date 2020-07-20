using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class BitwiseOrExpression : E9
    {
        public E9 Left { get; private set; }
        public BitwiseOrToken BitwiseOr { get; private set; }
        public E8 Right { get; private set; }
    
        public BitwiseOrExpression (E9 left, BitwiseOrToken bitwiseOr, E8 right)
        {
            Left = left;
            BitwiseOr = bitwiseOr;
            Right = right;
        }
    
        public static bool TryParse(ParseStack ps, out BitwiseOrExpression bitwiseOrExpression)
        {
            if (ps.CheckCache(out BitwiseOrExpression cached))
            {
                bitwiseOrExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E9.TryParse(ps, out E9 left)
                && ps.TryParse(out BitwiseOrToken bitwiseOr)
                && E8.TryParse(ps, out E8 right))
            {
                bitwiseOrExpression = new BitwiseOrExpression(left, bitwiseOr, right);
                ps.CacheAndPop(bitwiseOrExpression);
                return true;
            }
            else
            {
                bitwiseOrExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
