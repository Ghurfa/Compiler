using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class CastExpression : E1
    {
        public E1 Left { get; private set; }
        public AsKeywordToken AsKeyword { get; private set; }
        public Type Type { get; private set; }
    
        public CastExpression (E1 left, AsKeywordToken asKeyword, Type type)
        {
            Left = left;
            AsKeyword = asKeyword;
            Type = type;
        }
    
        public static bool TryParse(ParseStack ps, out CastExpression castExpression)
        {
            if (ps.CheckCache(out CastExpression cached))
            {
                castExpression = cached;
                return true;
            }
            
            ps.Save();
            if (E1.TryParse(ps, out E1 left)
                && ps.TryParse(out AsKeywordToken asKeyword)
                && Type.TryParse(ps, out Type type))
            {
                castExpression = new CastExpression(left, asKeyword, type);
                ps.CacheAndPop(castExpression);
                return true;
            }
            else
            {
                castExpression = null;
                ps.Restore();
                return false;
            }
        }
    }
}
