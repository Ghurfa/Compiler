using Compiler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TypeChecker.SymbolNodes;

namespace Generator
{
    class ClassBuildingInfo
    {
        public TypeBuilder Builder { get; private set; }
        public Dictionary<FieldNode, FieldBuilder> Fields { get; private set; }
        public List<(FieldBuilder field, Expression value)> DefaultedFields { get; private set; }
        public Dictionary<MethodNode, MethodBuilder> Methods { get; private set; }
        public Dictionary<ConstructorNode, ConstructorBuilder> Constructors { get; private set; }
        public ReadOnlyDictionary<string, ClassNode> ReferencedClasses { get; set; }
        public ClassBuildingInfo(TypeBuilder builder)
        {
            Builder = builder;
            Fields = new Dictionary<FieldNode, FieldBuilder>();
            DefaultedFields = new List<(FieldBuilder field, Expression value)>();
            Methods = new Dictionary<MethodNode, MethodBuilder>();
            Constructors = new Dictionary<ConstructorNode, ConstructorBuilder>();
        }
    }
}
