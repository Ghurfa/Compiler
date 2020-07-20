using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class IdentifierWithDot
    {
        public IdentifierToken Identifier { get; private set; }
        public DotToken Dot { get; private set; }
    
        public IdentifierWithDot (IdentifierToken identifier, DotToken dot)
        {
            Identifier = identifier;
            Dot = dot;
        }
    
        public static bool TryParse(ParseStack ps, out IdentifierWithDot identifierWithDot)
        {
            if (ps.CheckCache(out IdentifierWithDot cached))
            {
                identifierWithDot = cached;
                return true;
            }
            
            ps.Save();
            if (ps.TryParse(out IdentifierToken identifier)
                && ps.TryParse(out DotToken dot))
            {
                identifierWithDot = new IdentifierWithDot(identifier, dot);
                ps.CacheAndPop(identifierWithDot);
                return true;
            }
            else
            {
                identifierWithDot = null;
                ps.Restore();
                return false;
            }
        }
    }
}
