using Compiler.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Text;
using TypeChecker.SymbolNodes;

namespace TypeChecker.TypeInfos
{
    public class ValueTypeInfo : TypeInfo
    {
        private static Dictionary<ClassNode, ValueTypeInfo> types = new Dictionary<ClassNode, ValueTypeInfo>();
        public static Dictionary<string, ValueTypeInfo> PrimitiveTypes = new Dictionary<string, ValueTypeInfo>();

        public static ValueTypeInfo Get(ClassNode classNode)
        {
            if (types.TryGetValue(classNode, out ValueTypeInfo existing)) return existing;
            else
            {
                var ret = new ValueTypeInfo(classNode);
                types.Add(classNode, ret);
                return ret;
            }
        }

        public static ValueTypeInfo Get(SymbolsTable table, string name) => Get(table.GetClass(name));

        public static ValueTypeInfo Get(SymbolsTable table, Compiler.SyntaxTreeItems.Type type)
        {
            switch (type)
            {
                case ArrayType arrType: return new ArrayTypeInfo(table, Get(table, arrType.BaseType));
                case PrimitiveType primType: return PrimitiveTypes[primType.TypeKeyword.Text];
                case TupleType tupleType:
                    {
                        var subTypes = new ValueTypeInfo[tupleType.Items.Length];
                        for (int i = 0; i < subTypes.Length; i++)
                        {
                            subTypes[i] = Get(table, tupleType.Items[i].Type);
                        }
                        return TupleTypeInfo.Get(subTypes);
                    }
                default: throw new NotImplementedException();
            }
        }

        public static void Initialize(SymbolsTable table, string[] primitiveTypes)
        {
            void add(string name)
            {
                ClassNode node = table.GetBuiltInClass(name);
                ValueTypeInfo type = new ValueTypeInfo(node);
                PrimitiveTypes.Add(name, type);
                types.Add(node, type);
            }
            foreach (string type in primitiveTypes)
                add(type);
        }

        public ClassNode Class { get; set; }
        protected ValueTypeInfo() { }
        protected ValueTypeInfo(ClassNode type) { Class = type; }

        public override string ToString() => Class.Name;

        public override bool IsConvertibleTo(TypeInfo other)
        {
            if (other is ValueTypeInfo otherValType)
            {
                ValueTypeInfo ancestor = this;
                while (ancestor != null)
                {
                    if (ancestor == otherValType) return true;
                    ancestor = Get(ancestor.Class.ParentClass);
                }
                return false;
            }
            else return false;
        }
    }
}
