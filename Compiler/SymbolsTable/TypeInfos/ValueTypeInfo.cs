using System;
using System.Collections.Generic;
using System.Text;
using SymbolsTable.Nodes;
using Parser.SyntaxTreeItems;

namespace SymbolsTable.TypeInfos
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

        public static ValueTypeInfo Get(SymbolsTable table, Parser.SyntaxTreeItems.Type type)
        {
            switch (type)
            {
                case ArrayType arrType: return new ArrayTypeInfo(table, Get(table, arrType.BaseType), PrimitiveTypes["Array"].Class);
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
                case QualifiedIdentifierType qualifiedType:
                    {
                        var result = table.GetClass(qualifiedType.ToString(), out ClassNode classNode);
                        if (result != Result.Success) throw new InvalidOperationException();
                        return Get(classNode);
                    }
                default: throw new NotImplementedException();
            }
        }

        public static void Initialize(SymbolsTable table)
        {
            void add(string name, string systemName)
            {
                table.GetSystemClass(systemName, out ClassNode node);
                ValueTypeInfo type = new ValueTypeInfo(node);
                PrimitiveTypes.Add(name, type);
                types.Add(node, type);
            }
            add("bool", "Boolean");
            add("byte", "Byte");
            add("char", "Char");
            add("double", "Double");
            add("float", "Single");
            add("int", "Int32");
            add("object", "Object");
            add("string", "String");
            add("Array", "Array");
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
