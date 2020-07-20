using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class BitwiseAndExpression : E7
    {
        public E7 Left { get; private set; }
        public BitwiseAndToken BitwiseAnd { get; private set; }
        public E6 Right { get; private set; }
    
        public BitwiseAndExpression (E7 left, BitwiseAndToken bitwiseAnd, E6 right)
        {
            Left = left;
            BitwiseAnd = bitwiseAnd;
            Right = right;
        }
    
        public static bool TryParse(ParseStack ps, out BitwiseAndExpression bitwiseAndExpression)
        {
            if (ps.CheckCache(out BitwiseAndExpression cached))
            {
                bitwiseAndExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E7.TryParse(ps, out E7 left)
                && ps.TryParse(out BitwiseAndToken bitwiseAnd)
                && E6.TryParse(ps, out E6 right))
            {
                bitwiseAndExpression = new BitwiseAndExpression(left, bitwiseAnd, right);
                ps.CacheAndPop(bitwiseAndExpression);
                return true;
            }
            else
            {
                bitwiseAndExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
