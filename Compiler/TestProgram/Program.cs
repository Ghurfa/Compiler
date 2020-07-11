using Compiler;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TestProgram
{
    class Program
    {
        static void PrintTokenList(TokenCollection tokens, bool showWhitespace)
        {
            foreach (Token token in tokens)
            {
                PrintToken(token, true, !showWhitespace);
            }
        }
        static void PrintContext(TokenCollection tokens, Token token, int tokensBefore, int tokensAfter)
        {
            foreach (Token contextToken in tokens.GetContext(token, tokensBefore, tokensAfter))
            {
                if(token.Index == contextToken.Index)
                {
                    PrintToken(contextToken, true, true, ConsoleColor.Magenta);
                }
                else
                {
                    PrintToken(contextToken, true);
                }    
            }
        }
        static void PrintToken(Token token, bool withIndex = false, bool skipTrivia = true, ConsoleColor? forceColor = null)
        {
            if (withIndex)
            {
                if (token.IsTrivia && skipTrivia) return;
                Console.ForegroundColor = forceColor ?? ConsoleColor.White;
                Console.Write($"{token.Index}:\t");
            }
            if (token.IsTrivia)
            {
                if (skipTrivia) return;
                Console.ForegroundColor = forceColor ?? ConsoleColor.White;
                Console.WriteLine(token.Text);
            }
            else if (token.Type == TokenType.Identifier || token.Type == TokenType.StringLiteral || token.Type == TokenType.CharLiteral || token.Type == TokenType.IntLiteral)
            {
                Console.ForegroundColor = forceColor ?? ConsoleColor.Red;
                Console.WriteLine(token.Text);
            }
            else
            {
                Console.ForegroundColor = forceColor ?? ConsoleColor.Blue;
                Console.WriteLine(token.Text);
            }
        }
        static void PrintObject(object obj, string name, int x = 0)
        {
            int depthWidth = 3;
            Type objType = obj.GetType();

            Console.CursorLeft = x;
            Console.ForegroundColor = name.First() == '[' ? ConsoleColor.Magenta : ConsoleColor.White;
            Console.Write($"{name}: ");

            if (obj is Token token)
            {
                PrintToken(token);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(objType.Name);

                if (objType.IsArray)
                {
                    Array arr = (Array)obj;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        object item = arr.GetValue(i);
                        if (item == null)
                        {
                            Console.CursorLeft = x + depthWidth;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Null");
                        }
                        else
                        {
                            PrintObject(item, $"[{i}]", x + depthWidth);
                        }
                    }
                }
                else
                {
                    foreach (FieldInfo field in objType.GetRuntimeFields())
                    {
                        var value = field.GetValue(obj);
                        string fieldName = field.Name;
                        if (field.FieldType.IsPrimitive || field.FieldType == typeof(string) || field.FieldType == typeof(object) || field.FieldType.IsEnum)
                        {
                            Console.CursorLeft = x + depthWidth;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write($"{fieldName}: ");
                            if (field.FieldType == typeof(string))
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                            }
                            else if (field.FieldType.IsEnum)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                            }
                            Console.WriteLine(value.ToString());
                        }
                        else if (value == null)
                        {
                            Console.CursorLeft = x + depthWidth;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write($"{fieldName}: ");
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.WriteLine("Null");
                        }
                        else
                        {
                            PrintObject(value, fieldName, x + depthWidth);
                        }
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            var text = File.ReadAllText(@"..\..\..\..\..\FizzBuzz.txt");
            var tokens = Tokenizer.Tokenize(text);
            PrintTokenList(tokens, false);
            Console.WriteLine();
            try
            {
                NamespaceDeclaration namespaceDecl = new NamespaceDeclaration(tokens);
                PrintObject(namespaceDecl, "Namespace");
            }
            catch (SyntaxTreeBuildingException ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Syntax tree building exception");
                PrintContext(tokens, ex.Token, 10, 10);
            }
            Console.ReadLine();
        }
    }
}
