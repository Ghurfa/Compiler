using Compiler;
using Compiler.SyntaxTreeItems;
using Compiler.SyntaxTreeItems.ClassItemDeclarations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using TypeChecker.Exceptions;
using TypeChecker.SymbolNodes;
using TypeChecker.TypeInfos;

namespace TypeChecker
{
    class SymbolsTree
    {
        GlobalNode globalNode;

        public SymbolsTree()
        {
            globalNode = new GlobalNode();
        }

        public NamespaceSymbolNode AddNamespace(NamespaceDeclaration namespaceDecl)
        {
            QualifiedIdentifierPart[] parts = namespaceDecl.Name.Parts;

            int i;
            SymbolNode currentNode = globalNode;
            for (i = 0; i < parts.Length; i++)
            {
                if (currentNode.TryGetChild(parts[i].Identifier.Text, out SymbolNode child))
                {
                    currentNode = child;
                }
                else
                {
                    var newChild = new NamespaceSymbolNode(parts[i].Identifier.Text, currentNode);
                    currentNode.AddChild(newChild);
                    currentNode = newChild;
                    break;
                }
            }
            for (; i < parts.Length; i++)
            {
                var newChild = new NamespaceSymbolNode(parts[i].Identifier.Text, currentNode);
                currentNode.AddChild(newChild);
                currentNode = newChild;
            }

            return (NamespaceSymbolNode)currentNode;
        }

        public ClassNode AddClassNode(ClassDeclaration classDecl, SymbolNode parent)
        {
            if (parent.TryGetChild(classDecl.Name.Text, out SymbolNode classNode))
            {
                throw new DuplicateClassException();
            }
            else
            {
                var newChild = new ClassNode(classDecl.Name.Text, parent, new Modifiers(classDecl.Modifiers));
                parent.AddChild(newChild);
                return newChild;
            }
        }

        public FieldNode AddSimpleFieldNode(SimpleFieldDeclaration sFieldDecl, ClassNode parent)
        {
            string name = sFieldDecl.Name.Text;
            if (name == parent.Name) throw new MemberWithClassNameException();

            if (parent.TryGetChild(name, out SymbolNode _))
            {
                throw new DuplicateFieldException();
            }
            else
            {
                Modifiers modifiers = new Modifiers(sFieldDecl.Modifiers);
                var newChild = new FieldNode(name, parent, new ValueTypeInfo(sFieldDecl.Type.ToString()), modifiers);
                parent.AddChild(newChild);
                return newChild;
            }
        }

        public InferredFieldNode AddInferredFieldNode(InferredFieldDeclaration iFieldDecl, ClassNode parent)
        {
            string name = iFieldDecl.Name.Text;
            if (name == parent.Name) throw new MemberWithClassNameException();

            if (parent.TryGetChild(name, out SymbolNode _))
            {
                throw new DuplicateFieldException();
            }
            else
            {
                Modifiers modifiers = new Modifiers(iFieldDecl.Modifiers);
                var newChild = new InferredFieldNode(name, parent, modifiers);
                parent.AddChild(newChild);
                return newChild;
            }
        }

        public MethodNode AddMethodNode(MethodDeclaration methodDecl, ClassNode parent)
        {
            string name = methodDecl.Name.Text;
            if (name == parent.Name) throw new MemberWithClassNameException();

            ValueTypeInfo retType = new ValueTypeInfo(methodDecl.ReturnType.ToString());
            ValueTypeInfo[] paramTypes = new ValueTypeInfo[methodDecl.ParameterList.Parameters.Length];
            for (int i = 0; i < paramTypes.Length; i++)
            {
                paramTypes[i] = new ValueTypeInfo(methodDecl.ParameterList.Parameters[i].Type.ToString());
            }

            FunctionTypeInfo type = new FunctionTypeInfo(retType, paramTypes);
            MethodNode newNode = new MethodNode(name, parent, type, new Modifiers(methodDecl.Modifiers));

            parent.AddMethodChild(newNode);
            return newNode;
        }

        public ConstructorNode AddConstructorNode(ConstructorDeclaration ctorDecl, ClassNode parent)
        {
            ValueTypeInfo[] paramTypes = new ValueTypeInfo[ctorDecl.ParameterList.Parameters.Length];
            for (int i = 0; i < paramTypes.Length; i++)
            {
                paramTypes[i] = new ValueTypeInfo(ctorDecl.ParameterList.Parameters[i].Type.ToString());
            }
            ConstructorNode newNode = new ConstructorNode(parent, paramTypes, new Modifiers(ctorDecl.Modifiers));

            parent.AddConstructorChild(newNode);
            return newNode;
        }
    }
}
