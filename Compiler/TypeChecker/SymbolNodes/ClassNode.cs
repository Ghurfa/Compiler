using Compiler.SyntaxTreeItems;
using Compiler.SyntaxTreeItems.ClassItemDeclarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypeChecker.Exceptions;
using TypeChecker.TypeInfos;

namespace TypeChecker.SymbolNodes
{
    public class ClassNode : SymbolNode
    {
        public string FullName;
        public Modifiers Modifiers { get; set; }
        public Dictionary<string, FieldNode> Fields { get; set; }
        public List<FieldNode> SimpleDefaultedFields { get; set; }
        public List<MethodNode> Methods { get; set; }
        public List<ConstructorNode> Constructors { get; set; }
        public Dictionary<string, ClassNode> CachedClasses { get; set; }
        public ClassDeclaration Declaration { get; set; }
        public ClassNode ParentClass { get; set; }

        public ClassNode(string name, ClassDeclaration classDecl, ClassNode parentClass, SymbolNode parent, Modifiers modifiers, Dictionary<string, ClassNode> defaultCached)
            : base(name, parent)
        {
            Modifiers = modifiers;
            Fields = new Dictionary<string, FieldNode>();
            SimpleDefaultedFields = new List<FieldNode>();
            Methods = new List<MethodNode>();
            Constructors = new List<ConstructorNode>();

            if (defaultCached == null) CachedClasses = null;
            else CachedClasses = new Dictionary<string, ClassNode>(defaultCached);
            Declaration = classDecl;
            ParentClass = parentClass;

            FullName = GetFullName();
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
            if (Modifiers.IsStatic && !node.Modifiers.IsStatic) throw new InstanceMemberInStaticClassException();
            else if (Fields.ContainsKey(node.Name)) throw new DuplicateMemberException();
            else
            {
                foreach (MethodNode method in Methods)
                {
                    if (method.Name == node.Name) throw new DuplicateMemberException();
                }
            }

            Fields.Add(node.Name, node);
            AddChild(node);
            if (node.Declaration is SimpleFieldDeclaration sFieldDecl && sFieldDecl.DefaultValue != null)
            {
                SimpleDefaultedFields.Add(node);
            }
        }

        public void AddMethodChild(MethodNode node)
        {
            if (Modifiers.IsStatic && !node.Modifiers.IsStatic) throw new InstanceMemberInStaticClassException();
            else if (Fields.ContainsKey(node.Name)) throw new DuplicateMemberException();

            foreach (MethodNode method in Methods)
            {
                if (method.Name == node.Name)
                {
                    if (method.Type.ReturnType != node.Type.ReturnType) throw new DifferentTypeMethodsException();
                    if (Enumerable.SequenceEqual(method.Type.Parameters, node.Type.Parameters)) throw new DuplicateMethodException();
                }
            }
            Methods.Add(node);
            AddChild(node);
        }

        public void AddConstructorChild(ConstructorNode node)
        {
            foreach (ConstructorNode ctor in Constructors)
            {
                if (Enumerable.SequenceEqual(node.ParamTypes, ctor.ParamTypes))
                    throw new DuplicateConstructorException();
            }
            Constructors.Add(node);
            AddChild(node);
        }

        private string GetFullName()
        {
            SymbolNode ancestor = Parent;
            string nameSoFar = Name;
            while(!(Parent is GlobalNode))
            {
                nameSoFar = ancestor.Name + "." + nameSoFar;
                ancestor = ancestor.Parent;
            }
            return nameSoFar;
        }
    }
}
