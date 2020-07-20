using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class QualifiedIdentifier
    {
        public IdentifierWithDot[] QualifyingIdentifiers { get; private set; }
        public IdentifierToken Identifier { get; private set; }
    
        public QualifiedIdentifier (IdentifierWithDot[] qualifyingIdentifiers, IdentifierToken identifier)
        {
            QualifyingIdentifiers = qualifyingIdentifiers;
            Identifier = identifier;
        }
    
        public static bool TryParse(ParseStack ps, out QualifiedIdentifier qualifiedIdentifier)
        {
            if (ps.CheckCache(out QualifiedIdentifier cached))
            {
                qualifiedIdentifier = cached;
                return true;
            }
            
            ps.Save();
            if (TryParseQualifyingIdentifiers(ps, out IdentifierWithDot[] qualifyingIdentifiers)
                && ps.TryParse(out IdentifierToken identifier))
            {
                qualifiedIdentifier = new QualifiedIdentifier(qualifyingIdentifiers, identifier);
                ps.CacheAndPop(qualifiedIdentifier);
                return true;
            }
            else
            {
                qualifiedIdentifier = null;
                ps.Restore();
                return false;
            }
        }
    
        private static bool TryParseQualifyingIdentifiers(ParseStack ps, out IdentifierWithDot[] qualifyingIdentifiers)
        {
            List<IdentifierWithDot> items = new List<IdentifierWithDot>();
            while (IdentifierWithDot.TryParse(ps, out IdentifierWithDot identifierWithDot))
            {
                items.Add(identifierWithDot);
            }
            qualifyingIdentifiers = items.ToArray();
            return true;
        }
    }
}
