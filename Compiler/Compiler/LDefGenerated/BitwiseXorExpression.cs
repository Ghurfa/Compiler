using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class BitwiseXorExpression : E8
    {
        public E8 Left { get; private set; }
        public BitwiseXorToken BitwiseXor { get; private set; }
        public E7 Right { get; private set; }
    
        public BitwiseXorExpression (E8 left, BitwiseXorToken bitwiseXor, E7 right)
        {
            Left = left;
            BitwiseXor = bitwiseXor;
            Right = right;
        }
    
        public static bool TryParse(ParseStack ps, out BitwiseXorExpression bitwiseXorExpression)
        {
            if (ps.CheckCache(out BitwiseXorExpression cached))
            {
                bitwiseXorExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E8.TryParse(ps, out E8 left)
                && ps.TryParse(out BitwiseXorToken bitwiseXor)
                && E7.TryParse(ps, out E7 right))
            {
                bitwiseXorExpression = new BitwiseXorExpression(left, bitwiseXor, right);
                ps.CacheAndPop(bitwiseXorExpression);
                return true;
            }
            else
            {
                bitwiseXorExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
