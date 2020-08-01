﻿using Parser.SyntaxTreeItems;
using SymbolsTable.TypeInfos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SymbolsTable
{
    public partial class SymbolsTable
    {
        protected class Scope
        {
            public int IndexInParent { get; private set; }
            public Dictionary<string, BodyLocal> Locals { get; private set; }
            public Dictionary<PrimaryExpression, Field> ReferencedFields { get; private set; }
            public Dictionary<NewObjectExpression, Constructor> ReferencedConstructors{ get; private set; }
            public Dictionary<MethodCallExpression, Method> ReferencedMethods{ get; private set; }
            public Scope ParentScope { get; private set; }
            public List<Scope> Children { get; private set; }

            public Scope(int index, Scope parentScope)
            {
                IndexInParent = index;
                Locals = new Dictionary<string, BodyLocal>();
                ReferencedFields = new Dictionary<PrimaryExpression, Field>();
                ReferencedConstructors = new Dictionary<NewObjectExpression, Constructor>();
                ReferencedMethods = new Dictionary<MethodCallExpression, Method>();
                ParentScope = parentScope;
                Children = new List<Scope>();
            }

            private int currentChild;
            public void Begin() => currentChild = 0;
            public bool TryGetNextChild(out Scope child)
            {
                if (Children.Count == currentChild)
                {
                    child = null;
                    return false;
                }
                else
                {
                    child = Children[currentChild];
                    currentChild++;
                    return true;
                }
            }
        }
        protected class FunctionScope : Scope
        {
            public Dictionary<string, ParamLocal> Parameters { get; set; }
            public FunctionScope(SymbolsTable table, ParameterListDeclaration paramList, bool isStatic)
                : base(0, null)
            {
                Parameters = new Dictionary<string, ParamLocal>();
                int offset = isStatic ? 0 : 1;
                for (int i = 0; i < paramList.Parameters.Length; i++)
                {
                    var param = paramList.Parameters[i];
                    var paramInfo = new ParamLocal(ValueTypeInfo.Get(table, param.Type), i + offset);
                    Parameters.Add(param.Identifier.Text, paramInfo);
                }
            }
        }
    }
}