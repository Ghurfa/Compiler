using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LDefParser
{
    public class StringBuffer
    {
        private string text;
        private int i;
        private Stack<int> saveStack;

        public StringBuffer(string text)
        {
            this.text = text;
            i = 0;
            saveStack = new Stack<int>();
        }

        public void Save() => saveStack.Push(i);
        public void Restore() => i = saveStack.Pop();
        public void Pop() => saveStack.Pop();

        private char[] nonPunctuationSymbols = new char[] { '+', '=', '<', '>', '|', '^', '~' };
        private bool IsSymbol(char character)
        {
            if (char.IsPunctuation(character))
            {
                return character != '@';
            }
            return nonPunctuationSymbols.Contains(character);
        }

        public bool TryReadWord(out string word)
        {
            while (char.IsWhiteSpace(text[i])) i++;

            int start = i;
            for (; i < text.Length; i++)
            {
                if (char.IsWhiteSpace(text[i]) || IsSymbol(text[i]))
                {
                    break;
                }
            }
            if (i == start)
            {
                word = null;
                i = start;
                return false;
            }
            else
            {
                word = text.Substring(start, i - start);
                return true;
            }
        }

        public bool TryRead(char character)
        {
            while (char.IsWhiteSpace(text[i])) i++;

            if (text[i] == character)
            {
                i++;
                return true;
            }
            else return false;
        }

        private bool TryReadEnclosedWord(out string word, char endCharacter)
        {
            int start = i;

            for (; i < text.Length; i++)
            {
                if (text[i] == endCharacter) break;
            }
            if (i == text.Length) throw new InvalidOperationException();

            word = text.Substring(start, i - start);
            i++;
            return true;
        }

        public bool TryReadToken(out string tokenName)
        {
            while (char.IsWhiteSpace(text[i])) i++;

            if (i >= text.Length)
            {
                tokenName = null;
                return false;
            }
            switch (Next())
            {
                case '[': tokenName = "OpenBracket"; break;
                case ']': tokenName = "CloseBracket"; break;
                case '(': tokenName = "OpenPeren"; break;
                case ')': tokenName = "ClosePeren"; break;
                case ',': tokenName = "Comma"; break;
                case '\'':
                    if (!TryReadEnclosedWord(out string tokenChar, '\'')) throw new InvalidOperationException();
                    if (tokenChar.Length != 1) throw new InvalidOperationException();

                    switch (tokenChar[0])
                    {
                        case '|': tokenName = "BitwiseOr"; break;
                        case '=': tokenName = "Assign"; break;
                        case '{': tokenName = "OpenCurly"; break;
                        case '}': tokenName = "CloseCurly"; break;
                        case '~': tokenName = "BitwiseNot"; break;
                        case '+': tokenName = "Plus"; break;
                        case '<': tokenName = "LessThan"; break;
                        case '>': tokenName = "GreaterThan"; break;
                        case ':': tokenName = "Colon"; break;
                        default: throw new NotImplementedException();
                    }
                    break;
                case '<':
                    if (!TryReadEnclosedWord(out string tokenPartialName, '>')) throw new InvalidOperationException();
                    tokenName = tokenPartialName;
                    break;
                case '|':
                case '=':
                case '{':
                case '}':
                case '~':
                case '+':
                case ':':
                    i--;
                    tokenName = null;
                    return false;
                default: throw new NotImplementedException();
            }
            return true;
        }

        public char Next() => text[i++];

        public bool ReachedEnd() => i >= text.Length;
    }
}
