using Compiler.SyntaxTreeItems.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.SyntaxTreeItems
{
    public abstract class Type
    {
        public static Type ReadType(TokenCollection tokens)
        {
            IToken peek = tokens.PeekToken();
            if(peek is OpenPerenToken)
            {
                return new TupleType(tokens);
            }

            Type baseType;


            if(peek is PrimitiveTypeToken)
            {
                baseType = new PrimitiveType(tokens);
            }
            else
            {
                baseType = new QualifiedIdentifierType(tokens);
            }

            Type typeSoFar = baseType;
            bool finished = false;
            while(!finished)
            {
                if (tokens.PeekToken() is OpenBracketToken)
                {
                    typeSoFar = new ArrayType(tokens, typeSoFar);
                }
                else finished = true;
            }
            return typeSoFar;
        }
        public abstract override string ToString();
    }
}
