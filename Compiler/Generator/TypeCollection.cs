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
using TypeChecker;
using TypeChecker.SymbolNodes;
using TypeChecker.TypeInfos;

namespace Generator
{
    class TypeCollection : IEnumerable<(ClassNode node, ClassBuildingInfo info)>
    {
        private Dictionary<ClassNode, ClassBuildingInfo> types;

        public TypeBuilder this[ClassNode node] => types[node].Builder;
        public TypeBuilder this[ValueTypeInfo valType] => types[valType.Class].Builder;

        public TypeCollection()
        {
            types = new Dictionary<ClassNode, ClassBuildingInfo>();
        }

        public void DefineType(ModuleBuilder module, ClassNode node)
        {
            if (node is BuiltInClassNode || types.ContainsKey(node)) return;

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
                ClassBuildingInfo parentTypeInfo;
                if (types.TryGetValue(node.ParentClass, out parentTypeInfo))
                {
                    DefineType(module, node.ParentClass);
                    parentTypeInfo = types[node.ParentClass];
                }
                type = module.DefineType(node.FullName, attr, parentTypeInfo.Builder);
            }
            else
            {
                type = module.DefineType(node.FullName, attr);
            }

            var newInfo = new ClassBuildingInfo(type);
            newInfo.ReferencedClasses = new ReadOnlyDictionary<string, ClassNode>(node.CachedClasses);
            types.Add(node, newInfo);
        }

        public IEnumerator<(ClassNode node, ClassBuildingInfo info)> GetEnumerator()
        {
            foreach(KeyValuePair<ClassNode, ClassBuildingInfo> entry in types)
            {
                yield return (entry.Key, entry.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
