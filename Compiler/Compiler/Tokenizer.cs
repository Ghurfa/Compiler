using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
        static bool IsSymbol(char character)
        {
            if (char.IsPunctuation(character))
            {
                return character != '@';
            }
            return nonPunctuationSymbols.Contains(character);
        }
        static string ReadWord(string text, ref int i)
        {
            int start = i;
            for (; i < text.Length; i++)
            {
                if (char.IsWhiteSpace(text[i]) || IsSymbol(text[i]))
                {
                    break;
                }
            }
            if (i - start == 0) throw new TokenizingException(i);
            return text.Substring(start, i - start);
        }
        static void ParseCommentOrDivOp(string text, ref int i, LinkedList<Token> tokens)
        {
            int start = i;
            i++;
            if (i < text.Length)
            {
                char nextChar = text[i];
                if (nextChar == '/')
                {
                    string newLine = Environment.NewLine;
                    while (text.Substring(i - newLine.Length, newLine.Length) != newLine)
                    {
                        i++;
                    }
                    tokens.AddLast(new Token(text.Substring(start, i - newLine.Length - start), TokenType.SingleLineComment));
                    tokens.AddLast(new Token(newLine, TokenType.Whitespace));
                    i++;
                }
                else if (nextChar == '*')
                {
                    string endCommentString = "*/";
                    while (text.Substring(i - endCommentString.Length, endCommentString.Length) != endCommentString)
                    {
                        i++;
                    }
                    tokens.AddLast(new Token(text.Substring(start, i - start), TokenType.MultiLineComment));
                    i++;
                }
                else if(nextChar == '=')
                {
                    tokens.AddLast(new Token("/=", TokenType.DivideAssign));
                    i++;
                }
                else
                {
                    tokens.AddLast(new Token("/", TokenType.Divide));
                }
            }
            else
            {
                tokens.AddLast(new Token("/", TokenType.Divide));
            }
        }
        static bool IsValidSymbol(string symbols, out TokenType type)
        {
            switch (symbols)
            {
                case ".": type = TokenType.Dot; return true;
                case ",": type = TokenType.Comma; return true;
                case "(": type = TokenType.OpenPeren; return true;
                case ")": type = TokenType.ClosePeren; return true;
                case "[": type = TokenType.OpenBracket; return true;
                case "]": type = TokenType.CloseBracket; return true;
                case "{": type = TokenType.OpenCurly; return true;
                case "}": type = TokenType.CloseCurly; return true;
                case "?": type = TokenType.QuestionMark; return true;
                case ":": type = TokenType.Colon; return true;
                case ";": type = TokenType.Semicolon; return true;
                case "=": type = TokenType.Assign; return true;
                case ":=": type = TokenType.DeclAssign; return true;
                case "+": type = TokenType.Plus; return true;
                case "-": type = TokenType.Minus; return true;
                case "*": type = TokenType.Asterisk; return true;
                case "/": type = TokenType.Divide; return true;
                case "%": type = TokenType.Modulo; return true;
                case "+=": type = TokenType.PlusAssign; return true;
                case "-=": type = TokenType.MinusAssign; return true;
                case "*-": type = TokenType.TimesAssign; return true;
                case "/=": type = TokenType.DivideAssign; return true;
                case "%=": type = TokenType.ModuloAssign; return true;
                case "++": type = TokenType.Increment; return true;
                case "--": type = TokenType.Decrement; return true;
                case "==": type = TokenType.Equals; return true;
                case "!=": type = TokenType.NotEquals; return true;
                case "<": type = TokenType.LessThan; return true;
                case ">": type = TokenType.GreaterThan; return true;
                case "<=": type = TokenType.LessThanOrEqual; return true;
                case ">=": type = TokenType.GreaterThanOrEqual; return true;
                case "&&": type = TokenType.And; return true;
                case "||": type = TokenType.Or; return true;
                case "!": type = TokenType.Not; return true;
                case "&": type = TokenType.BitwiseAnd; return true;
                case "|": type = TokenType.BitwiseOr; return true;
                case "^": type = TokenType.BitwiseXor; return true;
                case "~": type = TokenType.BitwiseNot; return true;
                case "<<": type = TokenType.LeftShift; return true;
                case ">>": type = TokenType.RightShift; return true;
                default: type = default; return false;
            }
        }
        static void ParseSymbols(string text, ref int i, LinkedList<Token> tokens)
        {
            string symbols = "";
            TokenType tokenType = TokenType.Identifier;
            for (; i < text.Length; i++)
            {
                string newSymbols = symbols + text[i];
                if (IsValidSymbol(newSymbols, out TokenType type))
                {
                    symbols = newSymbols;
                    tokenType = type;
                }
                else break;
            }
            if (tokenType == TokenType.Identifier)
            {
                throw new TokenizingException(i);
            }
            else
            {
                tokens.AddLast(new Token(symbols, tokenType));
            }
        }
        static void ParseNumber(string text, ref int i, LinkedList<Token> tokens)
        {
            int start = i;
            for (; i < text.Length; i++)
            {
                if (IsSymbol(text[i]) || char.IsWhiteSpace(text[i]))
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
        static char ParseEscapedChar(string text, ref int i)
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
        static void ParseStringLiteral(string text, ref int i, LinkedList<Token> tokens)
        {
            tokens.AddLast(new Token("\"", TokenType.DoubleQuote));
            i++;
            LinkedList<char> literal = new LinkedList<char>();
            bool foundCloseQuote = false;
            while (i < text.Length)
            {
                if (text[i] == '\\')
                {
                    literal.AddLast(ParseEscapedChar(text, ref i));
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
            tokens.AddLast(new Token("\"", TokenType.DoubleQuote));
            i++;
        }
        static void ParseCharLiteral(string text, ref int i, LinkedList<Token> tokens)
        {
            tokens.AddLast(new Token("\'", TokenType.SingleQuote));
            i++;
            char literal;

            if (text[i] == '\\') literal = ParseEscapedChar(text, ref i);
            else if (text[i] == '\'') throw new TokenizingException(i);
            else literal = text[i];

            i++;
            if (text[i] != '\'') throw new TokenizingException(i);
            i++;

            tokens.AddLast(new Token(literal.ToString(), TokenType.CharLiteral));
            tokens.AddLast(new Token("\'", TokenType.SingleQuote));
        }
        static bool IsKeyword(string word, out TokenType tokenType)
        {
            switch (word)
            {
                case "new": tokenType = TokenType.NewKeyword; return true;
                case "namespace": tokenType = TokenType.NamespaceKeyword; return true;
                case "class": tokenType = TokenType.ClassKeyword; return true;
                case "ctor": tokenType = TokenType.ConstructorKeyword; return true;
                case "if": tokenType = TokenType.IfKeyword; return true;
                case "for": tokenType = TokenType.ForKeyword; return true;
                case "foreach": tokenType = TokenType.ForeachKeyword; return true;
                case "while": tokenType = TokenType.WhileKeyword; return true;
                case "return": tokenType = TokenType.ReturnKeyword; return true;
                case "break": tokenType = TokenType.BreakKeyword; return true;
                case "continue": tokenType = TokenType.ContinueKeyword; return true;
                case "as": tokenType = TokenType.AsKeyword; return true;
                default:
                    if (modifierKeywords.Contains(word))
                    {
                        tokenType = TokenType.Modifier;
                        return true;
                    }
                    else if (primitiveTypes.Contains(word))
                    {
                        tokenType = TokenType.PrimitiveType;
                        return true;
                    }
                    else if (valueKeywords.Contains(word))
                    {
                        tokenType = TokenType.Modifier;
                        return true;
                    }
                    else
                    {
                        tokenType = default;
                        return false;
                    }
            }
        }
        static void ParseText(string text, ref int i, LinkedList<Token> tokens)
        {
            string word = ReadWord(text, ref i);
            if (IsKeyword(word, out TokenType keywordType))
            {
                tokens.AddLast(new Token(word, keywordType));
            }
            else
            {
                tokens.AddLast(new Token(word, TokenType.Identifier));
            }
        }
        public static TokenCollection Tokenize(string text)
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
                    ParseNumber(text, ref i, tokens);
                }
                else if (nextChar == '"')
                {
                    ParseStringLiteral(text, ref i, tokens);
                }
                else if (nextChar == '\'')
                {
                    ParseCharLiteral(text, ref i, tokens);
                }
                else if (nextChar == '/')
                {
                    ParseCommentOrDivOp(text, ref i, tokens);
                }
                else if (IsSymbol(nextChar))
                {
                    ParseSymbols(text, ref i, tokens);
                }
                else
                {
                    ParseText(text, ref i, tokens);
                }
            }
            return new TokenCollection(tokens);
        }
    }
}
