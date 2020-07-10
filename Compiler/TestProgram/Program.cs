using Compiler;
using System;
using System.Collections.Generic;
using System.IO;

namespace TestProgram
{
    class Program
    {
        static void printTokenList(LinkedList<Token> tokens, bool showWhitespace)
        {
            foreach (Token token in tokens)
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
            Tokenizer.RemoveWhitespaceTokens(tokens);
            printTokenList(tokens, false);
            Console.ReadLine();
            NamespaceDeclaration namespaceDecl = new NamespaceDeclaration(tokens);
            Console.ReadLine();
        }
    }
}
