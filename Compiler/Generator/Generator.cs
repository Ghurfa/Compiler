using Compiler;
using Compiler.SyntaxTreeItems;
using Compiler.SyntaxTreeItems.ClassItemDeclarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TypeChecker;
using TypeChecker.SymbolNodes;
using TypeChecker.TypeInfos;

namespace Generator
{
    public static class Generator
    {
        public static void Generate(string fileName, SymbolsTable table)
        {
            var assemblyName = new AssemblyName("Assembly");
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Save);
            var module = assemblyBuilder.DefineDynamicModule("Module", fileName);

            var types = new TypeCollection();

            foreach (ClassNode classNode in table.IterateWithoutStack)
            {
                types.DefineType(module, classNode);
            }

            foreach ((ClassNode node, ClassBuildingInfo info) in types)
            {
                foreach (FieldNode field in node.Fields.Values) DefineField(types, info, field);
                foreach (ConstructorNode ctor in node.Constructors) DefineConstructor(types, info, ctor);
                foreach (MethodNode method in node.Methods) DefineMethod(types, info, method);
            }

            foreach ((ClassNode node, ClassBuildingInfo info) in types)
            {
                foreach (ConstructorNode ctor in node.Constructors)
                {

                }
                foreach (MethodBuilder builder in info.Methods.Values)
                {

                }
            }
        }

        private static void DefineField(TypeCollection types, ClassBuildingInfo info, FieldNode node)
        {
            FieldBuilder builder = info.Builder.DefineField(node.Name, types[node.Type], GetFieldAttributes(node.Modifiers));
            info.Fields.Add(node, builder);
            if (node is InferredFieldNode iField)
            {
                info.DefaultedFields.Add((builder, ((InferredFieldDeclaration)iField.Declaration).DefaultValue));
            }
            else
            {
                SimpleFieldDeclaration decl = (SimpleFieldDeclaration)node.Declaration;
                if (decl.DefaultValue != null)
                {
                    info.DefaultedFields.Add((builder, decl.DefaultValue));
                }
            }
        }

        private static void DefineConstructor(TypeCollection types, ClassBuildingInfo info, ConstructorNode node)
        {
            var builder = info.Builder.DefineConstructor(GetMethodAttributes(node.Modifiers),
                                                         CallingConventions.HasThis,
                                                         GetTypes(types, node.ParamTypes));
            info.Constructors.Add(node, builder);
        }

        private static void DefineMethod(TypeCollection types, ClassBuildingInfo info, MethodNode node)
        {
            System.Type retType = node.Type.ReturnType is VoidTypeInfo ? typeof(void) : types[(ValueTypeInfo)node.Type.ReturnType];
            System.Type[] paramTypes = GetTypes(types, node.Type.Parameters);
            MethodBuilder builder = info.Builder.DefineMethod(node.Name, GetMethodAttributes(node.Modifiers), retType, paramTypes);
            info.Methods.Add(node, builder);
        }

        private static void DefineFunctionBody(TypeCollection types, ILGenerator generator, MethodBodyDeclaration body)
        {
        
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

        private static TypeBuilder[] GetTypes(TypeCollection collection, ValueTypeInfo[] types)
        {
            TypeBuilder[] ret = new TypeBuilder[types.Length];
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = collection[types[i]];
            }
            return ret;
        }
    }
}

