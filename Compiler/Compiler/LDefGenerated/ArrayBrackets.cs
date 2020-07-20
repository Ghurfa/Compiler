using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class ArrayBrackets
    {
        public OpenBracketToken OpenBracket { get; private set; }
        public CloseBracketToken CloseBracket { get; private set; }
    
        public ArrayBrackets (OpenBracketToken openBracket, CloseBracketToken closeBracket)
        {
            OpenBracket = openBracket;
            CloseBracket = closeBracket;
        }
    
        public static bool TryParse(ParseStack ps, out ArrayBrackets arrayBrackets)
        {
            if (ps.CheckCache(out ArrayBrackets cached))
            {
                arrayBrackets = cached;
                return true;
            }
            
            ps.Save();
            if (ps.TryParse(out OpenBracketToken openBracket)
                && ps.TryParse(out CloseBracketToken closeBracket))
            {
                arrayBrackets = new ArrayBrackets(openBracket, closeBracket);
                ps.CacheAndPop(arrayBrackets);
                return true;
            }
            else
            {
                arrayBrackets = null;
                ps.Restore();
                return false;
            }
        }
    }
}
