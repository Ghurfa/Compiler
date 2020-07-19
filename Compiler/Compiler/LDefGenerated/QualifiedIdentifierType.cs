using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.LDefGenerated
{
    public class QualifiedIdentifierType : NonArrayType
    {
        public QualifiedIdentifier QualifiedIdentifier { get; private set; }
    
        public QualifiedIdentifierType (QualifiedIdentifier qualifiedIdentifier)
        {
            QualifiedIdentifier = qualifiedIdentifier;
        }
    
        public static bool TryParse(ParseStack ps, out QualifiedIdentifierType qualifiedIdentifierType)
        {
            if (ps.CheckCache(out QualifiedIdentifierType cached))
            {
                qualifiedIdentifierType = cached;
                return true;
            }
            
            ps.Save();
            if (QualifiedIdentifier.TryParse(ps, out QualifiedIdentifier qualifiedIdentifier))
            {
                qualifiedIdentifierType = new QualifiedIdentifierType(qualifiedIdentifier);
                ps.CacheAndPop(qualifiedIdentifierType);
                return true;
            }
            else
            {
                qualifiedIdentifierType = null;
                ps.Restore();
                return false;
            }
        }
    }
}
