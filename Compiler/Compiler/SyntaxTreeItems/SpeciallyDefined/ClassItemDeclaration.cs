using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public abstract class ClassItemDeclaration
    {
        public abstract IToken LeftToken { get; }
        public abstract IToken RightToken { get; }
        public static ClassItemDeclaration ReadClassItem(TokenCollection tokens)
        {
            if (tokens.PopIfMatches(out ConstructorKeywordToken constructorKeyword))
            {
                return new ConstructorDeclaration(tokens, constructorKeyword);
            }
            else if (tokens.PopIfMatches(out IdentifierToken identifier))
            {
                if (tokens.PopIfMatches(out IToken syntaxChar) ||
                    tokens.PopIfMatches(out syntaxChar) ||
                    tokens.PopIfMatches(out syntaxChar))
                {
                    return new FieldDeclaration(tokens, identifier, syntaxChar);
                }
                else
                {
                    return new MethodDeclaration(tokens, identifier);
                }
            }
            else throw new InvalidTokenException(tokens.PeekToken());
        }
    }
}
