using Compiler;
using Compiler.SyntaxTreeItems;
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
            foreach (IToken token in tokens)
            {
                PrintToken(token, true, !showWhitespace);
            }
        }
        static void PrintContext(TokenCollection tokens, IToken token, int tokensBefore, int tokensAfter)
        {
            foreach (IToken contextToken in tokens.GetContext(token, tokensBefore, tokensAfter))
            {
                if (token.Index == contextToken.Index)
                {
                    PrintToken(contextToken, true, true, ConsoleColor.Magenta);
                }
                else
                {
                    PrintToken(contextToken, true);
                }
            }
        }
        static void PrintToken(IToken token, bool withIndex = false, bool skipTrivia = true, ConsoleColor? forceColor = null)
        {
            if (withIndex)
            {
                if (token is ITriviaToken && skipTrivia) return;
                Console.ForegroundColor = forceColor ?? ConsoleColor.White;
                Console.Write($"{token.Index}:\t");
            }
            if (token is ITriviaToken)
            {
                if (skipTrivia) return;
                Console.ForegroundColor = forceColor ?? ConsoleColor.White;
                Console.WriteLine(token.Text);
            }
            else if (token is IdentifierToken || token is StringLiteralExpression || token is CharLiteralExpression || token is IntLiteralExpression)
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
        static void WriteWithDepth(string text, int depth, ConsoleColor textColor)
        {
            int depthWidth = 2;
            string levelString = "|" + new string(' ', depthWidth - 1);
            for (int i = 0; i < depth; i++)
            {
                Console.ForegroundColor = (ConsoleColor)((i % 15) + 1); //Skip black
                Console.Write(levelString);
            }
            Console.ForegroundColor = textColor;
            Console.Write(text);
        }
        static void WriteWithDepth(string text, int depth) => WriteWithDepth(text, depth, Console.ForegroundColor);
        static void WriteLineWithDepth(string text, int depth, ConsoleColor textColor)
        {
            WriteWithDepth(text, depth, textColor);
            Console.WriteLine();
        }
        static void WriteLineWithDepth(string text, int depth) => WriteLineWithDepth(text, depth, Console.ForegroundColor);
        static void PrintObject(object obj, string name, bool printNames = false, bool printNullFieldNames = true, int x = 0)
        {
            System.Type objType = obj.GetType();

            if (printNames) WriteWithDepth($"{name}: ", x, name.First() == '[' ? ConsoleColor.Magenta : ConsoleColor.White);
            else WriteWithDepth("", x);

            if (obj is IToken token)
            {
                PrintToken(token);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(objType.Name);

                if (objType.IsArray)
                {
                    Array arr = (Array)obj;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        object item = arr.GetValue(i);
                        if (item == null)
                        {
                            WriteLineWithDepth("Null", x + 1, ConsoleColor.DarkGray);
                        }
                        else
                        {
                            PrintObject(item, $"[{i}]", printNames, printNullFieldNames, x + 1);
                        }
                    }
                }
                else
                {
                    foreach (FieldInfo field in objType.GetRuntimeFields())
                    {
                        var value = field.GetValue(obj);
                        string fieldName;
                        if (field.Name.EndsWith("BackingField"))
                        {
                            fieldName = field.Name.Split('>')[0].Substring(1);
                        }
                        else fieldName = field.Name;
                        if (value == null)
                        {
                            if(printNullFieldNames) WriteWithDepth($"{ fieldName}: ", x + 1, ConsoleColor.White);
                            else WriteWithDepth("", x + 1);

                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("Null");
                        }
                        else
                        {
                            PrintObject(value, fieldName, printNames, printNullFieldNames, x + 1);
                        }
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            var text = File.ReadAllText(@"..\..\..\..\..\PrecedenceTest.txt");
            var tokens = Tokenizer.Tokenize(text);
            PrintTokenList(tokens, false);
            Console.WriteLine();
            try
            {
                NamespaceDeclaration namespaceDecl = new NamespaceDeclaration(tokens);
                PrintObject(namespaceDecl, "Namespace");
            }
            catch (InvalidTokenException ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(ex.GetType().Name + ": ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(ex.Message);
                PrintContext(tokens, ex.Token, 10, 10);
            }
            catch (InvalidStatementException ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(ex.GetType().Name + ": ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(ex.Message);
            }
            catch (InvalidAssignmentLeftException ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(ex.GetType().Name + ": ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
