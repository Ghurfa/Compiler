using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler
{
    public static class Tokenizer
    {
        static string[] modifierKeywords = new string[]
        {
            "public",
            "private",
            "static"
        };
        static string[] markerKeywords = new string[]
        {
            "namespace",
            "class",
            "ctor"
        };
        static string[] primitiveTypes = new string[]
        {
            "int",
            "float",
            "double",
            "long",
            "byte",
            "bool",
            "string",
            "void",
            "object",
        };
        static string[] controlKeywords = new string[]
        {
            "if",
            "for",
            "foreach",
            "while",
            "return",
            "break",
            "continue"
        };
        static string[] valueKeywords = new string[]
        {
            "this",
            "base",
            "value"
        };

        static string[] syntaxChars = new string[] { ".", ",", "(", ")", "[", "]", "{", "}", "?", ":", ";" };
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
        static bool IsHexadecimal(char character) => (character >= '0' && character <= '9') ||
                                                       (character >= 'a' && character <= 'f') ||
                                                       (character >= 'A' && character <= 'F');
        static char parseEscapedChar(string text, ref int i)
        {
            int start;
            i++;
            switch (text[i++])
            {
                case '\\':
                case '\'':
                case '"':
                    return text[i];
                case '0': return (char)0;
                case 'a': return (char)7;
                case 'b': return (char)8;
                case 't': return (char)9;
                case 'n': return (char)10;
                case 'v': return (char)11;
                case 'f': return (char)12;
                case 'r': return (char)13;
                case 'u':
                    for (start = i; i < start + 4; i++)
                    {
                        if (!IsHexadecimal(text[i])) throw new TokenizingException(i);
                    }
                    return (char)int.Parse(text.Substring(start, 4), System.Globalization.NumberStyles.HexNumber);
                case 'x':
                    for (start = i; i < start + 4; i++)
                    {
                        if (!IsHexadecimal(text[i])) break;
                    }
                    if (i == start) throw new TokenizingException(i);
                    return (char)int.Parse(text.Substring(start, i - start), System.Globalization.NumberStyles.HexNumber);
                case 'U':
                    for (start = i; i < start + 8; i++)
                    {
                        if (!IsHexadecimal(text[i])) throw new TokenizingException(i);
                    }
                    return (char)int.Parse(text.Substring(start, 8), System.Globalization.NumberStyles.HexNumber);
                default: throw new TokenizingException(i);
            }
        }
        static void parseStringLiteral(string text, ref int i, LinkedList<Token> tokens)
        {
            tokens.AddLast(new Token("\"", TokenType.SyntaxChar));
            i++;
            int start = i;
            LinkedList<char> literal = new LinkedList<char>();
            bool foundCloseQuote = false;
            for (; i < text.Length; )
            {
                if (text[i] == '\\')
                {
                    literal.AddLast(parseEscapedChar(text, ref i));
                }
                else if (text[i] == '"')
                {
                    foundCloseQuote = true;
                    i++;
                    break;
                }
                else
                {
                    literal.AddLast(text[i]);
                    i++;
                }
            }
            if (!foundCloseQuote) throw new TokenizingException(i);
            tokens.AddLast(new Token(new string(literal.ToArray()), TokenType.StringLiteral));
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
            if (modifierKeywords.Contains(word))
                tokens.AddLast(new Token(word, TokenType.Modifier));

            else if (controlKeywords.Contains(word))
                tokens.AddLast(new Token(word, TokenType.ControlKeyword));

            else if (word == "new")
                tokens.AddLast(new Token(word, TokenType.NewKeyword));

            else if (primitiveTypes.Contains(word))
                tokens.AddLast(new Token(word, TokenType.PrimitiveType));

            else if (markerKeywords.Contains(word))
                tokens.AddLast(new Token(word, TokenType.BlockMarker));

            else if (valueKeywords.Contains(word))
                tokens.AddLast(new Token(word, TokenType.ValueKeyword));

            else
                tokens.AddLast(new Token(word, TokenType.Identifier));
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
