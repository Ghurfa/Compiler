using System;
using System.Collections.Generic;
using System.IO;

namespace SimpleTokenizer
{
    class Program
    {
        static void printTokenList(LinkedList<Token> tokens, bool showWhitespace)
        {
            foreach (Token token in tokens)
            {
                if (token.Type == TokenType.Whitespace)
                {
                    if(showWhitespace)
                    {
                        Console.WriteLine($"Whitespace");
                    }
                }
                else
                {
                    Console.WriteLine($"{token.Type} \t {token.Text}");
                }
            }
            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            var text = File.ReadAllText(@"..\..\..\Program.txt");
            var tokens = SimpleTokenizer.Tokenize(text);
            printTokenList(tokens, false);
        }
    }
}
