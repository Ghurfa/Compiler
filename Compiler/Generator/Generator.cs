using Parser.SyntaxTreeItems.ClassItemDeclarations;
using SymbolsTable;
using SymbolsTable.Nodes;
using SymbolsTable.TypeInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public static class Generator
    {
        public static void Generate(string fileName, SymbolsTable.SymbolsTable table)
        {
            var assemblyName = new AssemblyName("Assembly");
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            var module = assemblyBuilder.DefineDynamicModule("Module", fileName);

            var maps = new Mappings();

            foreach (ClassNode classNode in table.IterateWithoutStack)
            {
                maps.DefineClass(module, classNode);
            }

            foreach (ClassNode node in table.IterateWithoutStack)
            {
                ClassBuildingInfo info = maps.GetClassBuildingInfo(node);
                foreach (Field field in node.Fields.Values) DefineField(maps, info, field);
                foreach (Constructor ctor in node.Constructors) DefineConstructor(maps, info, ctor);
                foreach (Method method in node.Methods) DefineMethod(maps, info, method);
            }

            MethodBuilder entryPoint = null;
            Emitter emitter = new Emitter(maps, table);

            foreach (ClassNode node in table.IterateWithoutStack)
            {
                ClassBuildingInfo info = maps.GetClassBuildingInfo(node);
                foreach (Constructor ctor in node.Constructors)
                {
                    ConstructorBuilder builder = maps[ctor];

                    emitter.SetGenerator(builder.GetILGenerator());
                    emitter.EmitConstructorStart(info);
                    emitter.EmitFunctionBody(ctor);
                }
                foreach (Method method in node.Methods)
                {
                    MethodBuilder builder = maps[method];
                    if (method.Name == "Main" && method.Modifiers.IsStatic && method.Type.Parameters.Length == 0)
                        entryPoint = builder;

                    emitter.SetGenerator(builder.GetILGenerator());
                    emitter.EmitFunctionBody(method);
                }
                info.Builder.CreateType();
            }

            assemblyBuilder.SetEntryPoint(entryPoint);
            assemblyBuilder.Save(fileName);
        }

        private static void DefineField(Mappings types, ClassBuildingInfo info, Field node)
        {
            FieldBuilder builder = info.Builder.DefineField(node.Name, types[node.Type.Class], GetFieldAttributes(node.Modifiers));
            types.MapField(node, builder);
            if (node is InferredField iField)
            {
                info.DefaultedFields.Add((builder, iField.Declaration.DefaultValue));
            }
            else
            {
                SimpleFieldDeclaration decl = ((SimpleField)node).Declaration;
                if (decl.DefaultValue != null)
                {
                    info.DefaultedFields.Add((builder, decl.DefaultValue));
                }
            }
        }

        private static void DefineConstructor(Mappings types, ClassBuildingInfo info, Constructor node)
        {
            var builder = info.Builder.DefineConstructor(GetMethodAttributes(node.Modifiers),
                                                         CallingConventions.HasThis,
                                                         GetTypes(types, node.ParamTypes));
            types.MapConstructor(node, builder);
        }

        private static void DefineMethod(Mappings types, ClassBuildingInfo info, SymbolsTable.Method node)
        {
            System.Type retType = node.Type.ReturnType is VoidTypeInfo ? typeof(void) : types[((ValueTypeInfo)node.Type.ReturnType).Class];
            System.Type[] paramTypes = GetTypes(types, node.Type.Parameters);
            MethodBuilder builder = info.Builder.DefineMethod(node.Name, GetMethodAttributes(node.Modifiers), retType, paramTypes);
            types.MapMethod(node, builder);
        }

        private static FieldAttributes GetFieldAttributes(Modifiers modifiers)
        {
            FieldAttributes attr = 0;
            switch (modifiers.AccessModifier)
            {
                case AccessModifier.PrivateModifier:
                case AccessModifier.ProtectedModifier:
                    attr |= FieldAttributes.Private;
                    break;
                case AccessModifier.PublicModifier:
                    attr |= FieldAttributes.Public;
                    break;
                default: throw new NotImplementedException();
            }

            if (modifiers.IsStatic)
            {
                attr |= FieldAttributes.Static;
            }

            return attr;
        }


        private static MethodAttributes GetMethodAttributes(Modifiers modifiers)
        {
            MethodAttributes attr = 0;
            switch (modifiers.AccessModifier)
            {
                case AccessModifier.PrivateModifier:
                case AccessModifier.ProtectedModifier:
                    attr |= MethodAttributes.Private;
                    break;
                case AccessModifier.PublicModifier:
                    attr |= MethodAttributes.Public;
                    break;
                default: throw new NotImplementedException();
            }

            if (modifiers.IsStatic)
            {
                attr |= MethodAttributes.Static;
            }

            return attr;
        }

        private static Type[] GetTypes(Mappings maps, ValueTypeInfo[] types)
        {
            Type[] ret = new Type[types.Length];
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = maps[types[i].Class];
            }
            return ret;
        }
    }
}

