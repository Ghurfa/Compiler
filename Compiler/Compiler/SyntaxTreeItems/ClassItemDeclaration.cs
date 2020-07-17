using Compiler.SyntaxTreeItems;
using Compiler.SyntaxTreeItems.ClassItemDeclarations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    public abstract class ClassItemDeclaration
    {
        public static ClassItemDeclaration ReadClassItem(TokenCollection tokens)
        {
            if (tokens.PopIfMatches(out ConstructorKeywordToken ctorKeyword))
            {
                return new ConstructorDeclaration(tokens, ctorKeyword);
            }
            else if (tokens.PopIfMatches(out IdentifierToken identifier))
            {
                if (tokens.PopIfMatches(out ColonToken colon))
                {
                    return new SimpleFieldDeclaration(tokens, identifier, colon);
                }
                else if (tokens.PopIfMatches(out DeclAssignToken declAssign))
                {
                    return new InferredFieldDeclaration(tokens, identifier, declAssign);
                }
                else
                {
                    return new MethodDeclaration(tokens, identifier);
                }
            }
            else throw new InvalidTokenException(tokens);
        }
    }
}
