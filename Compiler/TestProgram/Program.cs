using Compiler;
using System;
using System.Collections.Generic;
using System.IO;

namespace TestProgram
{
    class Program
    {
        static void printTokenList(TokenCollection tokens, bool showWhitespace)
        {
            foreach (Token token in (showWhitespace ? tokens: tokens.NonWhitespaceTokens()))
            {
                if (token.Type == TokenType.Whitespace)
                {
                    if (showWhitespace)
                    {
                        Console.WriteLine($"Whitespace");
                    }
                }
                else
                {
                    Console.WriteLine($"{token.Type} \t {token.Text}");
                }
            }
        }
        static void Main(string[] args)
        {
            var text = File.ReadAllText(@"..\..\..\..\..\guessingGame.txt");
            var tokens = Tokenizer.Tokenize(text);
            printTokenList(tokens, false);
            Console.ReadLine();
            NamespaceDeclaration namespaceDecl = new NamespaceDeclaration(tokens);
            Console.ReadLine();
        }
    }
}
