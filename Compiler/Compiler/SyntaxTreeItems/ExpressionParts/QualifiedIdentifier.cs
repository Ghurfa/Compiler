using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public class QualifiedIdentifier
    {
        public readonly QualifiedIdentifierPart[] Parts;

        public  IToken LeftToken => Parts.First().LeftToken;
        public  IToken RightToken => Parts.Last().RightToken;

        public QualifiedIdentifier(TokenCollection tokens, QualifiedIdentifierPart[] parts = null)
        {
            var partsList = new LinkedList<QualifiedIdentifierPart>();
            if (parts != null)
            {
                foreach (var item in parts)
                {
                    partsList.AddLast(item);
                }
            }
            bool lastMissingDot = false;
            while(!lastMissingDot)
            {
                var newItem = new QualifiedIdentifierPart(tokens);
                partsList.AddLast(newItem);
                lastMissingDot = newItem.Dot == null;
            }
            Parts = partsList.ToArray();
        }

        public override string ToString()
        {
            string ret = "";
            return ret;
        }
    }
}
