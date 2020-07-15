﻿using CodeGenerator.ClassItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.ClassItems
{
    class EnsureWhitespaceAfterItem : InitialStatementItemInfo
    {
        string Expression;
        public EnsureWhitespaceAfterItem(string[] arguments)
        {
            if (arguments.Length != 1) throw new NotImplementedException();
            Expression = arguments[0];
        }
        public override string[] GetCreationStatements() => new string[] { $"tokens.EnsureWhitespaceAfter({Expression}); "};

    }
}
