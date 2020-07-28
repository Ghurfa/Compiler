using Compiler.SyntaxTreeItems;
using Compiler.SyntaxTreeItems.ClassItemDeclarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SymbolsTable.TypeInfos;

namespace SymbolsTable.Nodes
{
    public class ClassNode : SymbolNode
    {
        public string FullName;
        public Modifiers Modifiers { get; set; }
        public Dictionary<string, FieldInfo> Fields { get; set; }
        public List<SimpleFieldInfo> SimpleDefaultedFields { get; set; }
        public List<MethodInfo> Methods { get; set; }
        public List<ConstructorInfo> Constructors { get; set; }
        public Dictionary<string, ClassNode> CachedClasses { get; set; }
        public ClassDeclaration Declaration { get; set; }
        public ClassNode ParentClass { get; set; }

        public ClassNode(string name, ClassDeclaration classDecl, ClassNode parentClass, SymbolNode parent, Modifiers modifiers, Dictionary<string, ClassNode> defaultCached)
            : base(name, parent)
        {
            Modifiers = modifiers;
            Fields = new Dictionary<string, FieldInfo>();
            SimpleDefaultedFields = new List<SimpleFieldInfo>();
            Methods = new List<MethodInfo>();
            Constructors = new List<ConstructorInfo>();

            if (defaultCached == null) CachedClasses = null;
            else CachedClasses = new Dictionary<string, ClassNode>(defaultCached);
            Declaration = classDecl;
            ParentClass = parentClass;

            FullName = GetFullName();
        }

        public Result AddField(FieldInfo field)
        {
            if (Modifiers.IsStatic && !field.Modifiers.IsStatic) return Result.InstanceMemberInStaticClass;
            else if (Fields.ContainsKey(field.Name)) return Result.DuplicateMember;
            else
            {
                foreach (MethodInfo method in Methods)
                {
                    if (method.Name == field.Name) return Result.DuplicateMember;
                }
            }

            Fields.Add(field.Name, field);
            if (field is SimpleFieldInfo sFieldInfo && sFieldInfo.Declaration.DefaultValue != null)
            {
                SimpleDefaultedFields.Add(sFieldInfo);
            }
            return Result.Success;
        }

        public Result AddMethod(MethodInfo method)
        {
            if (Modifiers.IsStatic && !method.Modifiers.IsStatic) return Result.InstanceMemberInStaticClass;
            else if (Fields.ContainsKey(method.Name)) return Result.DuplicateMember;

            foreach (MethodInfo other in Methods)
            {
                if (method.Name == other.Name)
                {
                    if (method.Type.ReturnType != other.Type.ReturnType) return Result.DifferentTypeMethods;
                    if (Enumerable.SequenceEqual(method.Type.Parameters, other.Type.Parameters)) return Result.DuplicateMethod;
                }
            }
            Methods.Add(method);
            return Result.Success;
        }

        public Result AddConstructor(ConstructorInfo ctor)
        {
            foreach (ConstructorInfo other in Constructors)
            {
                if (Enumerable.SequenceEqual(ctor.ParamTypes, other.ParamTypes))
                    return Result.DuplicateConstructor;
            }
            Constructors.Add(ctor);
            return Result.Success;
        }

        private string GetFullName()
        {
            SymbolNode ancestor = Parent;
            string nameSoFar = Name;
            while(!(ancestor is GlobalNode))
            {
                nameSoFar = ancestor.Name + "." + nameSoFar;
                ancestor = ancestor.Parent;
            }
            return nameSoFar;
        }
    }
}
