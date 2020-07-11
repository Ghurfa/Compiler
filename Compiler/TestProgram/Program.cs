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
            foreach (Token token in (showWhitespace ? tokens : tokens.NonWhitespaceTokens()))
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
        static void PrintObject(object obj, string name, int x = 0)
        {
            int depthWidth = 3;
            Type objType = obj.GetType();

            Console.CursorLeft = x;
            Console.ForegroundColor = name.First() == '[' ? ConsoleColor.Magenta : ConsoleColor.White;
            Console.Write($"{name}: ");

            if (obj is Token token)
            {
                if(token.Type == TokenType.Identifier || token.Type == TokenType.StringLiteral || token.Type == TokenType.CharLiteral || token.Type == TokenType.IntLiteral)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(token.Text);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(((Token)obj).Type);
                }
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
            NamespaceDeclaration namespaceDecl = new NamespaceDeclaration(tokens);
            PrintObject(namespaceDecl, "Namespace");
            Console.ReadLine();
        }
    }
}
