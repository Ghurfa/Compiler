using Compiler;
using Compiler.SyntaxTreeItems;
using Compiler.SyntaxTreeItems.ClassItemDeclarations;
using System;
using System.Collections.Generic;
using System.Text;
using TypeChecker.Exceptions;
using TypeChecker.SymbolNodes;
using TypeChecker.TypeInfos;

namespace TypeChecker
{
    partial class SymbolsTable
    {
        public void BuildTree(IEnumerable<NamespaceDeclaration> namespaces, out List<(InferredFieldNode, InferredFieldDeclaration)> inferredFields)
        {
            InitializeTree();

            foreach (NamespaceDeclaration namespaceDecl in namespaces)
            {
                SymbolNode namespaceNode = AddNamespace(namespaceDecl);
                foreach (ClassDeclaration classDecl in namespaceDecl.ClassDeclarations)
                {
                    AddClass(classDecl, namespaceNode);
                }
            }

            inferredFields = new List<(InferredFieldNode, InferredFieldDeclaration)>();

            foreach (ClassNode classNode in IterateIgnoreFields)
            {
                foreach (ClassItemDeclaration classItem in classNode.Declaration.ClassItems)
                {
                    switch (classItem)
                    {
                        case InferredFieldDeclaration iFieldDecl:
                            {
                                var newNode = new InferredFieldNode(this, iFieldDecl, classNode);
                                inferredFields.Add((newNode, iFieldDecl));
                                classNode.AddFieldChild(newNode);
                            }
                            break;
                        case SimpleFieldDeclaration sFieldDecl:
                            {
                                var newNode = new FieldNode(this, sFieldDecl, classNode);
                                classNode.AddFieldChild(newNode);
                            }
                            break;
                        case MethodDeclaration methodDecl:
                            classNode.AddMethodChild(new MethodNode(this, methodDecl, classNode));
                            break;
                        case ConstructorDeclaration ctorDecl:
                            classNode.AddConstructorChild(new ConstructorNode(this, ctorDecl, classNode));
                            break;
                        default: throw new NotImplementedException();
                    }
                }
            }
        }

        private void InitializeTree()
        {
            globalNode = new GlobalNode();
            objectNode = new ObjectClassNode(globalNode);

            defaultCachedClasses.Clear();
            void add(string name)
            {
                var newNode = new BuiltInClassNode(name, objectNode, globalNode);
                globalNode.AddChild(newNode);
                defaultCachedClasses.Add(name, newNode);
            }
            add("int");
            add("bool");
            add("string");
            add("char");

            ValueTypeInfo.Initialize(this);

            ValueTypeInfo stringType = ValueTypeInfo.PrimitiveTypes["string"];
            objectNode.AddMethodChild(new MethodNode("ToString", new FunctionTypeInfo(stringType, new ValueTypeInfo[] { }), objectNode, Modifiers.Public));
        }

        private NamespaceNode AddNamespace(NamespaceDeclaration namespaceDecl)
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
                    var newChild = new NamespaceNode(parts[i].Identifier.Text, currentNode);
                    currentNode.AddChild(newChild);
                    currentNode = newChild;
                    break;
                }
            }
            for (; i < parts.Length; i++)
            {
                var newChild = new NamespaceNode(parts[i].Identifier.Text, currentNode);
                currentNode.AddChild(newChild);
                currentNode = newChild;
            }

            return (NamespaceNode)currentNode;
        }

        private ClassNode AddClass(ClassDeclaration classDecl, SymbolNode parent)
        {
            string name = classDecl.Name.Text;
            if (parent.TryGetChild(name, out SymbolNode classNode))
            {
                throw new DuplicateClassException();
            }
            else
            {
                var newChild = new ClassNode(name, classDecl, objectNode, parent, new Modifiers(classDecl.Modifiers), defaultCachedClasses);
                parent.AddChild(newChild);
                return newChild;
            }
        }
    }
}
