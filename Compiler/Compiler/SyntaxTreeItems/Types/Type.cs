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
            Token peek = tokens.PeekToken();
            if(peek.Type == TokenType.OpenPeren)
            {
                return new TupleType(tokens);
            }

            Type baseType;


            if(peek.Type == TokenType.PrimitiveType)
            {
                baseType = new BuiltInType(tokens);
            }
            else
            {
                baseType = new QualifiedIdentifier(tokens);
            }

            Type typeSoFar = baseType;
            bool finished = false;
            while(!finished)
            {
                if (tokens.PeekToken().Type == TokenType.OpenBracket)
                {
                    typeSoFar = new ArrayType(tokens, typeSoFar);
                }
                else finished = true;
            }
            return typeSoFar;
        }
    }
}
