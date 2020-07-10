﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.SyntaxTreeItems.Expressions
{
    public class ValueKeywordExpression : PrimaryExpression
    {
        public readonly Token ValueKeyword;
        public ValueKeywordExpression(LinkedList<Token> tokens, Token keyword)
        {
            ValueKeyword = keyword;
        }
    }
}