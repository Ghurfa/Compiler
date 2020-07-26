using Compiler.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Text;
using TypeChecker.SymbolNodes;

namespace TypeChecker.TypeInfos
{
    class ValueTypeInfo : TypeInfo
    {
        private static Dictionary<ClassNode, ValueTypeInfo> types = new Dictionary<ClassNode, ValueTypeInfo>();
        public static Dictionary<string, ValueTypeInfo> PrimitiveTypes = new Dictionary<string, ValueTypeInfo>();

        public static ValueTypeInfo Get(SymbolsTable table, string name)
        {
            ClassNode classNode = table.GetClass(name);
            if (types.TryGetValue(classNode, out ValueTypeInfo existing)) return existing;
            else
            {
                var ret = new ValueTypeInfo(classNode);
                types.Add(classNode, ret);
                return ret;
            }
        }

        public static ValueTypeInfo Get(SymbolsTable table, Compiler.SyntaxTreeItems.Type type)
        {
            switch (type)
            {
                case PrimitiveType primType: return PrimitiveTypes[primType.TypeKeyword.Text];
                case TupleType tupleType:
                    {
                        var subTypes = new ValueTypeInfo[tupleType.Items.Length];
                        for(int i = 0; i < subTypes.Length; i++)
                        {
                            subTypes[i] = Get(table, tupleType.Items[i].Type);
                        }
                        return TupleTypeInfo.Get(subTypes);
                    }
                default: throw new NotImplementedException();
            }
        }

        public static void Initialize(SymbolsTable table)
        {
            void add(string name)
            {
                ClassNode node = table.GetBuiltInClass(name);
                ValueTypeInfo type = new ValueTypeInfo(node);
                PrimitiveTypes.Add(name, type);
                types.Add(node, type);
            }
            add("int");
            add("bool");
            add("string");
            add("char");
        }

        public ClassNode Type { get; set; }
        protected ValueTypeInfo() {}
        private ValueTypeInfo(ClassNode type) { Type = type; }

        public override string ToString() => Type.Name;
    }
}
