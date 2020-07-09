using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Compiler
{
    public enum TokenType
    {
        Keyword,
        Identifier,
        Whitespace,
        SyntaxChar,
        Operator,
        IntLiteral,
        StringLiteral,
        CharLiteral,
    }
    [DebuggerDisplay("{Type} \t {Text}")]
    public struct Token
    {
        public string Text;
        public TokenType Type;
        public Token(string text, TokenType type)
        {
            Text = text;
            Type = type;
        }
    }
    public static class Tokenizer
    {
        static string[] keywords = new string[]
        {
            "public",
            "private",
            "static",
            "class",
            "namespace",
            "int",
            "float",
            "double",
            "long",
            "byte",
            "bool",
            "string",
            "void",
            "return",
            "var",
            "ctor"
        };
        static string[] syntaxChars = new string[] { ".", ",", "(", ")", "[", "]", "{", "}", "<", ">", ">", "?", ":", ";" };
        static string[] operators = new string[] {  "=", ":=",
                                                    "+", "-", "*", "/", "%",
                                                    "+=", "-=", "*-", "/=", "%=",
                                                    "++", "--",
                                                    "==", "!=", "<", ">", "<=", ">=",
                                                    "&&", "||", "!",
                                                    "&", "|", "^", "<<", ">>", "~",
        };

        static char[] nonPunctuationSymbols = new char[] { '+', '=', '<', '>', '|' };
        static bool isSymbol(char character)
        {
            if (char.IsPunctuation(character))
            {
                return character != '@';
            }
            return nonPunctuationSymbols.Contains(character);
        }
        static string readWord(string text, ref int i)
        {
            int start = i;
            for (; i < text.Length; i++)
            {
                if (char.IsWhiteSpace(text[i]) || isSymbol(text[i]))
                {
                    break;
                }
            }
            if (i - start == 0) throw new TokenizingException(i);
            return text.Substring(start, i - start);
        }
        static void parseSymbols(string text, ref int i, LinkedList<Token> tokens)
        {
            string symbols = "";
            bool isOperator = false;
            bool isSyntaxChar = false;
            for (; i < text.Length; i++)
            {
                string newSymbols = symbols + text[i];
                if (operators.Contains(newSymbols))
                {
                    symbols = newSymbols;
                    isOperator = true;
                    isSyntaxChar = false;
                }
                else if (syntaxChars.Contains(newSymbols))
                {
                    symbols = newSymbols;
                    isOperator = false;
                    isSyntaxChar = true;
                }
                else break;
            }
            if (isOperator)
            {
                tokens.AddLast(new Token(symbols, TokenType.Operator));
            }
            else if (isSyntaxChar)
            {
                tokens.AddLast(new Token(symbols, TokenType.SyntaxChar));
            }
            else
            {
                throw new TokenizingException(i);
            }
        }
        static void parseNumber(string text, ref int i, LinkedList<Token> tokens)
        {
            int start = i;
            for (; i < text.Length; i++)
            {
                if (isSymbol(text[i]) || char.IsWhiteSpace(text[i]))
                {
                    break;
                }
                else if (!char.IsNumber(text[i]))
                {
                    throw new TokenizingException(i);
                }
            }
            if (i - start == 0) throw new TokenizingException(i);
            string numString = text.Substring(start, i - start);
            tokens.AddLast(new Token(numString, TokenType.IntLiteral));
        }
        static void parseStringLiteral(string text, ref int i, LinkedList<Token> tokens)
        {
            tokens.AddLast(new Token("\"", TokenType.SyntaxChar));
            i++;
            int start = i;
            string literal = "";
            bool foundCloseQuote = false;
            for (; i < text.Length; i++)
            {
                if (text[i] == '\\')
                {
                    //todo: implement escaped chars
                    i++;
                }
                else if (text[i] == '"')
                {
                    foundCloseQuote = true;
                    literal = text.Substring(start, i - start);
                    break;
                }
            }
            if (!foundCloseQuote) throw new TokenizingException(i);
            tokens.AddLast(new Token(literal, TokenType.StringLiteral));
            tokens.AddLast(new Token("\"", TokenType.SyntaxChar));
            i++;
        }
        static void parseCharLiteral(string text, ref int i, LinkedList<Token> tokens)
        {
            tokens.AddLast(new Token("\'", TokenType.SyntaxChar));
            i++;
            int start = i;
            bool foundCloseQuote = false;
            string literal = "";
            for (; i < text.Length; i++)
            {
                if (text[i] == '\\')
                {
                    i++;
                }
                else if (text[i] == '\'')
                {
                    foundCloseQuote = true;
                    literal = text.Substring(start, i - start);
                    break;
                }
            }
            if (!foundCloseQuote) throw new TokenizingException(i);
            tokens.AddLast(new Token(literal, TokenType.CharLiteral));
            tokens.AddLast(new Token("\'", TokenType.SyntaxChar));
            i++;
        }
        static void parseText(string text, ref int i, LinkedList<Token> tokens)
        {
            string word = readWord(text, ref i);
            if (keywords.Contains(word))
            {
                tokens.AddLast(new Token(word, TokenType.Keyword));
            }
            else
            {
                tokens.AddLast(new Token(word, TokenType.Identifier));
            }
        }
        public static LinkedList<Token> Tokenize(string text)
        {
            LinkedList<Token> tokens = new LinkedList<Token>();
            int i = 0;
            while (i < text.Length)
            {
                char nextChar = text[i];
                if (char.IsWhiteSpace(nextChar))
                {
                    tokens.AddLast(new Token(nextChar.ToString(), TokenType.Whitespace));
                    i++;
                }
                else if (char.IsNumber(nextChar))
                {
                    parseNumber(text, ref i, tokens);
                }
                else if (nextChar == '"')
                {
                    parseStringLiteral(text, ref i, tokens);
                }
                else if (nextChar == '\'')
                {
                    parseCharLiteral(text, ref i, tokens);
                }
                else if (isSymbol(nextChar))
                {
                    parseSymbols(text, ref i, tokens);
                }
                else
                {
                    parseText(text, ref i, tokens);
                }
            }
            return tokens;
        }
        public static void RemoveWhitespaceTokens(LinkedList<Token> tokens)
        {
            if (tokens.Count == 0) return;
            LinkedListNode<Token> node = tokens.First;
            while (node != null)
            {
                LinkedListNode<Token> nextNode = node.Next;
                if (node.Value.Type == TokenType.Whitespace)
                {
                    tokens.Remove(node);
                }
                node = nextNode;
            }
        }
    }
}
