using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace CSharpTokenizer
{
    enum TokenType
    {
        Modifier,
        ClassKeyword,
        ClassName,
        ClassBegin,
        ClassEnd,
        FieldType,
        FieldName,
        PropertyType,
        PropertyName,
        PropertyBegin,
        PropertyEnd,
        PropertyArrow,
        FunctionType,
        FunctionName,
        FunctionBegin,
        FunctionEnd,
        ParamsBegin,
        ParamsEnd,
        ParamType,
        ParamName,
        EndLine,
        SetOperator,
    }
    [DebuggerDisplay("{Type} \t {Text}")]
    struct Token
    {
        string Text;
        TokenType Type;
        public Token(string text, TokenType type)
        {
            Text = text;
            Type = type;
        }
    }
    class Program
    {
        static string[] modifierKeywords = new string[]
        {
            "public",
            "static"
        };
        static string[] typeKeywords = new string[]
        {
            "int",
            "float",
            "double",
            "long",
            "byte",
            "bool",
            "string",
            "void",
        };
        static char[] whitespaceChars = new char[] { ' ', '\r', '\n', '\t' };
        static char[] punctuation = new char[] { '{', '}', '[', ']', '(', ')', '=', '+', '-', '<', '>', ';', ',', '.' };
        static bool isWhitespace(char character) => whitespaceChars.Contains(character);
        static bool isPunctuation(char character) => punctuation.Contains(character);
        static string readWord(string text, ref int i)
        {
            for (; i < text.Length; i++)
            {
                if (!isWhitespace(text[i]))
                {
                    break;
                }
            }
            int wordStart = i;
            for (; i < text.Length; i++)
            {
                if (isWhitespace(text[i]) || isPunctuation(text[i]))
                {
                    return text.Substring(wordStart, i - wordStart);
                }
            }
            throw new InvalidOperationException();
        }
        static char readChar(string text, ref int i)
        {
            for (; i < text.Length; i++)
            {
                if (!isWhitespace(text[i]))
                {
                    return text[i++];
                }
            }
            throw new InvalidOperationException();
        }
        static string readSymbols(string text, ref int i)
        {
            for (; i < text.Length; i++)
            {
                if (!isWhitespace(text[i]))
                {
                    break;
                }
            }
            int wordStart = i;
            for (; i < text.Length; i++)
            {
                if (!isPunctuation(text[i]))
                {
                    return text.Substring(wordStart, i - wordStart);
                }
            }
            throw new InvalidOperationException();
        }
        static bool isValidName(string name) => true;
        static string ReadType(string text, ref int i)
        {
            string baseType = readWord(text, ref i);
            return ParseType(text, ref i, baseType);
        }
        static string ParseType(string text, ref int i, string baseType)
        {
            //Todo: handle arrays and generics
            if (typeKeywords.Contains(baseType))
            {
                return baseType;
            }
            else
            {
                throw new TokenizingException(i);
            }
        }
        static string ReadName(string text, ref int i)
        {
            string word;
            if (isValidName(word = readWord(text, ref i)))
            {
                return word;
            }
            else throw new TokenizingException(i);
        }
        static string ParseModifiers(string text, ref int i, LinkedList<Token> tokens)
        {
            string word;
            while (modifierKeywords.Contains(word = readWord(text, ref i)))
            {
                tokens.AddLast(new Token(word, TokenType.Modifier));
            }
            return word;
        }
        static void ParseClassDeclaration(string text, ref int i, LinkedList<Token> tokens)
        {
            string classWord = ParseModifiers(text, ref i, tokens);
            if (classWord != "class") throw new TokenizingException(i);
            tokens.AddLast(new Token("class", TokenType.ClassKeyword));
            tokens.AddLast(new Token(ReadName(text, ref i), TokenType.ClassName));
        }
        static void ParseClassItem(string text, ref int i, LinkedList<Token> tokens)
        {
            string typeStartWord = ParseModifiers(text, ref i, tokens);
            string typeName = ParseType(text, ref i, typeStartWord);
            string itemName = ReadName(text, ref i);

            string startSymbol = readSymbols(text, ref i);

            switch (startSymbol)
            {
                case ";":
                    tokens.AddLast(new Token(typeName, TokenType.FieldType));
                    tokens.AddLast(new Token(itemName, TokenType.FieldName));
                    tokens.AddLast(new Token(startSymbol, TokenType.EndLine));
                    return;
                case "=":
                    tokens.AddLast(new Token(typeName, TokenType.FieldType));
                    tokens.AddLast(new Token(itemName, TokenType.FieldName));
                    tokens.AddLast(new Token(startSymbol, TokenType.SetOperator));
                    ParseFieldDefaultValue(text, ref i, tokens);
                    return;
                case "{":
                    tokens.AddLast(new Token(typeName, TokenType.PropertyType));
                    tokens.AddLast(new Token(itemName, TokenType.PropertyName));
                    tokens.AddLast(new Token(startSymbol, TokenType.PropertyBegin));
                    ParseProperty(text, ref i, tokens);
                    return;
                case "=>":
                    tokens.AddLast(new Token(typeName, TokenType.PropertyType));
                    tokens.AddLast(new Token(itemName, TokenType.PropertyName));
                    tokens.AddLast(new Token(startSymbol, TokenType.PropertyArrow));
                    ParsePropertyLambda(text, ref i, tokens);
                    return;
                case "(":
                    tokens.AddLast(new Token(typeName, TokenType.FunctionType));
                    tokens.AddLast(new Token(itemName, TokenType.FunctionName));
                    tokens.AddLast(new Token(startSymbol, TokenType.ParamsBegin));
                    ParseFunction(text, ref i, tokens);
                    return;
                default:
                    throw new TokenizingException(i);
            }
        }
        static void ParseFieldDefaultValue(string text, ref int i, LinkedList<Token> tokens)
        {
            throw new NotImplementedException();
        }
        static void ParseProperty(string text, ref int i, LinkedList<Token> tokens)
        {
            throw new NotImplementedException();
        }
        static void ParsePropertyLambda(string text, ref int i, LinkedList<Token> tokens)
        {
            throw new NotImplementedException();
        }
        static void ParseFunctionParameters(string text, ref int i, LinkedList<Token> tokens)
        {
            while (true)
            {
                //todo: out, ref, params, etc.
                tokens.AddLast(new Token(ReadType(text, ref i), TokenType.ParamType));
                tokens.AddLast(new Token(ReadName(text, ref i), TokenType.ParamName));
                char nextChar = readChar(text, ref i);
                if (nextChar == ')')
                {
                    break;
                }
                else if (nextChar != ',')
                {
                    //todo: default values
                    throw new TokenizingException(i);
                }
            }
            tokens.AddLast(new Token(")", TokenType.ParamsEnd));
        }
        static void ParseStatement(string text, ref int i, LinkedList<Token> tokens)
        {

        }
        static void ParseFunction(string text, ref int i, LinkedList<Token> tokens)
        {
            ParseFunctionParameters(text, ref i, tokens);
            ParseOpenBrace(text, ref i, tokens, TokenType.FunctionBegin);
            while(readChar(text, ref i) != '}')
            {
                i--;
                ParseStatement(text, ref i, tokens);
            }
            tokens.AddLast(new Token("}", TokenType.FunctionEnd));
            i++;
        }
        static void ParseOpenBrace(string text, ref int i, LinkedList<Token> tokens, TokenType tokenType)
        {
            char openBrace = readChar(text, ref i);
            if (openBrace != '{') throw new TokenizingException(i);
            tokens.AddLast(new Token("{", tokenType));
            i++;
        }
        static void ParseClass(string text, ref int i, LinkedList<Token> tokens)
        {
            ParseClassDeclaration(text, ref i, tokens);
            ParseOpenBrace(text, ref i, tokens, TokenType.ClassBegin);

            while (readChar(text, ref i) != '}')
            {
                i--;
                ParseClassItem(text, ref i, tokens);
            }
            tokens.AddLast(new Token("}", TokenType.ClassEnd));
            i++;
        }
        static void Main(string[] args)
        {
            var text = File.ReadAllText(@"..\..\..\Program.txt");
            LinkedList<Token> tokens = new LinkedList<Token>();
            int i = 0;
            ParseClass(text, ref i, tokens);
            ;
        }
    }
}
