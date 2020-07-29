using Parser.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Tokenizer;

namespace TestProgram
{
    [Flags]
    public enum VisualizationOptions
    {
        PrintNames = 1,
        PrintNullFieldNames = 2,
        PrintTypes = 4,
        LineFeedAfterType = 8,
    }

    public enum DepthWriteStyle
    {
        Empty,
        ColorPipes,
        Arrows
    }
    public static class Visualizer
    {
        public static void PrintTokenList(TokenCollection tokens, bool showWhitespace)
        {
            foreach (IToken token in tokens)
            {
                PrintToken(token, true, !showWhitespace);
            }
        }
        public static void PrintContext(TokenCollection tokens, IToken token, int tokensBefore, int tokensAfter)
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
        public static void PrintObject(object obj, string name, DepthWriteStyle style, VisualizationOptions options, int x = 0)
        {
            System.Type objType = obj.GetType();

            if (options.HasFlag(VisualizationOptions.PrintNames))
            {
                WriteWithDepth($"{name}: ", x, name.First() == '[' ? ConsoleColor.Magenta : ConsoleColor.White, style);
            }
            else WriteWithDepth("", x, ConsoleColor.Black, style);

            if (obj is IToken token)
            {
                PrintToken(token);
            }
            else
            {
                if (options.HasFlag(VisualizationOptions.PrintTypes))
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(objType.Name);
                }
                else if (options.HasFlag(VisualizationOptions.LineFeedAfterType)) Console.WriteLine();

                if (objType.IsArray)
                {
                    Array arr = (Array)obj;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        object item = arr.GetValue(i);
                        if (item == null)
                        {
                            WriteLineWithDepth("Null", x + 1, ConsoleColor.DarkGray, style);
                        }
                        else
                        {
                            PrintObject(item, $"[{i}]", style, options, x + 1);
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
                            if (options.HasFlag(VisualizationOptions.PrintNullFieldNames))
                            {
                                WriteWithDepth($"{ fieldName}: ", x + 1, ConsoleColor.White, style);
                            }
                            else WriteWithDepth("", x + 1, ConsoleColor.Black, style);

                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("Null");
                        }
                        else
                        {
                            PrintObject(value, fieldName, style, options, x + 1);
                        }
                    }
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
            else if (token is IdentifierToken || token is StringLiteralToken || token is CharLiteralToken || token is IntLiteralToken)
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
        static void WriteWithDepth(string text, int depth, ConsoleColor textColor, DepthWriteStyle style)
        {
            int depthWidth = 2;
            switch (style)
            {
                case DepthWriteStyle.Empty:
                    Console.CursorLeft = depthWidth * depth;
                    break;
                case DepthWriteStyle.ColorPipes:
                    string levelString = "|" + new string(' ', depthWidth - 1);
                    Console.CursorLeft = 0;
                    for (int i = 0; i < depth; i++)
                    {
                        Console.ForegroundColor = (ConsoleColor)((i % 15) + 1); //Skip black
                        Console.Write(levelString);
                    }
                    break;
                case DepthWriteStyle.Arrows:
                    if (depth == 0) break;
                    Console.CursorLeft = depthWidth * depth;
                    Console.ForegroundColor = (ConsoleColor)(((depth - 1) % 15) + 1);
                    Console.Write("> ");
                    break;
                default: throw new NotImplementedException();
            }
            Console.ForegroundColor = textColor;
            Console.Write(text);
        }
        static void WriteLineWithDepth(string text, int depth, ConsoleColor textColor, DepthWriteStyle style)
        {
            WriteWithDepth(text, depth, textColor, style);
            Console.WriteLine();
        }
    }
}
