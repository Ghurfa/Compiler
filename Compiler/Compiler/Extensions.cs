using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Compiler
{
    public static class Extensions
    {
        public static bool PopIfMatches(this LinkedList<Token> tokens, out Token tokenIfMatches, TokenType type)
        {
            if (tokens.Count == 0) throw new SyntaxTreeBuildingException();
            Token peek = tokens.First.Value;
            if (peek.Type == type)
            {
                tokens.RemoveFirst();
                tokenIfMatches = peek;
                return true;
            }
            tokenIfMatches = default;
            return false;
        }

        public static bool PopIfMatches(this LinkedList<Token> tokens, out Token tokenIfMatches, TokenType type, string text)
        {
            if (tokens.Count == 0) throw new SyntaxTreeBuildingException();
            Token peek = tokens.First.Value;
            if (peek.Type == type && peek.Text == text)
            {
                tokens.RemoveFirst();
                tokenIfMatches = peek;
                return true;
            }
            tokenIfMatches = default;
            return false;
        }
        public static bool PopIfMatches(this LinkedList<Token> tokens, out Token tokenIfMatches, TokenType type, string[] possibleText)
        {
            if (tokens.Count == 0) throw new SyntaxTreeBuildingException();
            Token peek = tokens.First.Value;
            if (peek.Type == type && possibleText.Contains(peek.Text))
            {
                tokens.RemoveFirst();
                tokenIfMatches = peek;
                return true;
            }
            tokenIfMatches = default;
            return false;
        }

        public static Token GetToken(this LinkedList<Token> tokens)
        {
            if (tokens.Count == 0) throw new SyntaxTreeBuildingException();
            var token = tokens.First.Value;
            tokens.RemoveFirst();
            return token;
        }
        public static Token GetToken(this LinkedList<Token> tokens, TokenType expectedType)
        {
            if (tokens.Count == 0) throw new SyntaxTreeBuildingException();
            var token = tokens.First.Value;
            if (token.Type != expectedType) throw new SyntaxTreeBuildingException(token);
            tokens.RemoveFirst();
            return token;
        }
        public static Token GetToken(this LinkedList<Token> tokens, TokenType expectedType, string expectedText)
        {
            if (tokens.Count == 0) throw new SyntaxTreeBuildingException();
            var token = tokens.First.Value;
            if (token.Type != expectedType) throw new SyntaxTreeBuildingException(token);
            if (token.Text != expectedText) throw new SyntaxTreeBuildingException(token);
            tokens.RemoveFirst();
            return token;
        }
        private static string[] modifiers = new string[] { "public", "private", "static" };
        public static Token[] GetModifiers(this LinkedList<Token> tokens)
        {
            return GetModifiers(tokens, modifiers);
        }
        public static Token[] GetModifiers(this LinkedList<Token> tokens, string[] validModifiers)
        {
            LinkedList<Token> modifiers = new LinkedList<Token>();
            Token token;
            while (tokens.PopIfMatches(out token, TokenType.Modifier, validModifiers))
            {
                modifiers.AddLast(token);
            }
            return modifiers.ToArray();
        }
    }
}
