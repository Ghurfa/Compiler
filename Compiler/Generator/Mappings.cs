using SymbolsTable;
using SymbolsTable.Nodes;
using SymbolsTable.TypeInfos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    class Mappings
    {
        private Dictionary<ClassNode, ClassBuildingInfo> Classes;
        private Dictionary<Field, FieldBuilder> Fields;
        private Dictionary<Constructor, ConstructorBuilder> Constructors;
        private Dictionary<Method, MethodBuilder> Methods;
        private Dictionary<Local, LocalBuilder> Locals;

        public Mappings()
        {
            Classes = new Dictionary<ClassNode, ClassBuildingInfo>();
            Fields = new Dictionary<Field, FieldBuilder>();
            Constructors = new Dictionary<Constructor, ConstructorBuilder>();
            Methods = new Dictionary<Method, MethodBuilder>();
            Locals = new Dictionary<Local, LocalBuilder>();
        }

        public Type this[ClassNode node]
        { 
            get
            {
                if (node is BuiltInClassNode builtIn)
                {
                    switch (builtIn.Name)
                    {
                        case "int": return typeof(int);
                        default: throw new NotImplementedException();
                    }
                }
                else return Classes[node].Builder;
            }
            private set { throw new InvalidOperationException(); }
        }
        public FieldBuilder this[Field field] => Fields[field];
        public ConstructorBuilder this[Constructor constructor] => Constructors[constructor];
        public MethodBuilder this[Method method] => Methods[method];
        public LocalBuilder this[Local local] => Locals[local];

        public void DefineClass(ModuleBuilder module, ClassNode node)
        {
            if (node is BuiltInClassNode || Classes.ContainsKey(node)) return;

            TypeAttributes attr = TypeAttributes.Class;
            switch (node.Modifiers.AccessModifier)
            {
                case AccessModifier.PrivateModifier:
                case AccessModifier.ProtectedModifier:
                    attr |= TypeAttributes.NotPublic;
                    break;
                case AccessModifier.PublicModifier:
                    attr |= TypeAttributes.Public;
                    break;
                default: throw new NotImplementedException();
            }

            TypeBuilder type;
            if (!(node.ParentClass is BuiltInClassNode))
            {
                if (!Classes.TryGetValue(node.ParentClass, out ClassBuildingInfo parentTypeInfo))
                {
                    DefineClass(module, node.ParentClass);
                    parentTypeInfo = Classes[node.ParentClass];
                }
                type = module.DefineType(node.FullName, attr, parentTypeInfo.Builder);
            }
            else
            {
                type = module.DefineType(node.FullName, attr);
            }

            var newInfo = new ClassBuildingInfo(type);
            newInfo.ReferencedClasses = new ReadOnlyDictionary<string, ClassNode>(node.CachedClasses);
            Classes.Add(node, newInfo);
        }

        public void MapField(Field field, FieldBuilder builder) => Fields.Add(field, builder);
        public void MapConstructor(Constructor constructor, ConstructorBuilder builder) => Constructors.Add(constructor, builder);
        public void MapMethod(Method method, MethodBuilder builder) => Methods.Add(method, builder);
        public void MapLocal(Local local, LocalBuilder builder) => Locals.Add(local, builder);

        public ClassBuildingInfo GetClassBuildingInfo(ClassNode node) => Classes[node];
    }
}
