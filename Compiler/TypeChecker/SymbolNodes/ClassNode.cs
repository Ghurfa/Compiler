using Compiler.SyntaxTreeItems.ClassItemDeclarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypeChecker.Exceptions;
using TypeChecker.TypeInfos;

namespace TypeChecker.SymbolNodes
{
    class ClassNode : SymbolNode
    {
        public Modifiers Modifiers { get; set; }
        public Dictionary<string, FieldNode> Fields { get; set; }
        public List<FieldNode> SimpleDefaultedFields { get; set; }
        public List<MethodNode> Methods { get; set; }
        public List<ConstructorNode> Constructors { get; set; }
        private SymbolsTable table;

        public ClassNode(string name, SymbolNode parent, Modifiers modifiers)
            : base(name, parent)
        {
            Modifiers = modifiers;
            Fields = new Dictionary<string, FieldNode>();
            SimpleDefaultedFields = new List<FieldNode>();
            Methods = new List<MethodNode>();
            Constructors = new List<ConstructorNode>();
        }

        public override void AddChild(SymbolNode child)
        {
            switch (child)
            {
                case FieldNode fieldNode: AddFieldChild(fieldNode); break;
                case MethodNode methodNode: AddMethodChild(methodNode); break;
                case ConstructorNode ctorNode: AddConstructorChild(ctorNode); break;
                default: throw new NotImplementedException();
            }
        }

        public void AddFieldChild(FieldNode node)
        {
            if(Fields.ContainsKey(node.Name)) throw new DuplicateMemberException();
            else
            {
                foreach(MethodNode method in Methods)
                {
                    if (method.Name == node.Name) throw new DuplicateMemberException();
                }
            }

            Fields.Add(node.Name, node);
            Children.Add(node);
            if(node.Declaration is SimpleFieldDeclaration sFieldDecl && sFieldDecl.DefaultValue != null)
            {
                SimpleDefaultedFields.Add(node);
            }
        }

        public void AddMethodChild(MethodNode node)
        {
            foreach (ClassItemNode child in Children)
            {
                if(TryGetChild(node.Name, out SymbolNode other))
                {
                    if (other is MethodNode method && method.Name == node.Name)
                    {
                        if (method.Type.ReturnType != node.Type.ReturnType) throw new DifferentTypeMethodsException();
                        if (Enumerable.SequenceEqual(method.Type.Parameters, node.Type.Parameters)) throw new DuplicateMethodException();
                    }
                    else throw new DuplicateMemberException();
                }
            }
            Methods.Add(node);
            Children.Add(node);
        }

        public void AddConstructorChild(ConstructorNode node)
        {
            foreach (ClassItemNode child in Children)
            {
                if (child is ConstructorNode ctor)
                {
                    if (Enumerable.SequenceEqual(node.ParamTypes, ctor.ParamTypes)) throw new DuplicateConstructorException();
                }
            }
            Constructors.Add(node);
            Children.Add(node);
        }

        public SymbolsTable GetSymbolsTable()
        {
            if(table == null)
            {
                Dictionary<string, TypeInfo> fieldsDict = new Dictionary<string, TypeInfo>();
                foreach (KeyValuePair<string, FieldNode> field in Fields)
                {
                    fieldsDict.Add(field.Key, field.Value.Type);
                }
                table = new SymbolsTable(fieldsDict, this);
            }
            return table;
        }
    }
}
