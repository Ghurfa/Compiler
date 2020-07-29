using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tokenizer;

namespace Parser.SyntaxTreeItems
{
    public abstract class Type : Expression
    {
        public override int Precedence => 0;
        public override Expression LeftExpr { get => null; set => throw new InvalidOperationException(); }
        public override Expression RightExpr { get => null; set => throw new InvalidOperationException(); }
        public static Type ReadType(TokenCollection tokens)
        {
            IToken peek = tokens.PeekToken();
            if (peek is OpenPerenToken)
            {
                return new TupleType(tokens);
            }

            Type baseType;


            if (peek is PrimitiveTypeToken)
                baseType = new PrimitiveType(tokens);
            else if (peek is VoidKeywordToken)
                baseType = new VoidType(tokens);
            else
                baseType = new QualifiedIdentifierType(tokens, new QualifiedIdentifier(tokens));

            Type typeSoFar = baseType;
            bool finished = false;
            while (!finished)
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
