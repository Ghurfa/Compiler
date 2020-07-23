using System;
using System.Collections.Generic;
using System.Text;
using TypeChecker.Exceptions;
using TypeChecker.TypeInfos;

namespace TypeChecker.SymbolNodes
{
    class ClassNode : SymbolNode
    {
        public Modifiers Modifiers { get; set; }

        public ClassNode(string name, SymbolNode parent, Modifiers modifiers)
            : base(name, parent)
        {
            Modifiers = modifiers;
        }

        public void AddMethodChild(MethodNode node)
        {
            foreach (ClassItemNode child in Children)
            {
                if (child is MethodNode method && method.Name == node.Name)
                {
                    if (method.Type.ReturnType != node.Type.ReturnType) throw new DifferentTypeMethodsException();
                    if (HasSameParams(method.Type.Parameters, node.Type.Parameters)) throw new DuplicateMethodException();
                }
            }
            Children.Add(node);
        }

        public void AddConstructorChild(ConstructorNode node)
        {
            foreach (ClassItemNode child in Children)
            {
                if (child is ConstructorNode ctor)
                {
                    if (HasSameParams(node.ParamTypes, ctor.ParamTypes)) throw new DuplicateConstructorException();
                }
            }
            Children.Add(node);
        }

        private bool HasSameParams(ValueTypeInfo[] left, ValueTypeInfo[] right)
        {
            if (left.Length != right.Length) return false;

            for(int i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i]) return false;
            }
            return true;
        }
    }
}
