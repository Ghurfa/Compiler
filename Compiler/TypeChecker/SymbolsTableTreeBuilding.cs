﻿using Compiler;
using Compiler.SyntaxTreeItems;
using Compiler.SyntaxTreeItems.ClassItemDeclarations;
using System;
using System.Collections.Generic;
using System.Text;
using TypeChecker.Exceptions;
using TypeChecker.SymbolNodes;

namespace TypeChecker
{
    partial class SymbolsTable
    {
        public void BuildTree(IEnumerable<NamespaceDeclaration> namespaces, out List<(InferredFieldNode, InferredFieldDeclaration)> inferredFields)
        {
            inferredFields = new List<(InferredFieldNode, InferredFieldDeclaration)>();

            foreach (NamespaceDeclaration namespaceDecl in namespaces)
            {
                //Built symbols tree
                SymbolNode namespaceNode = AddNamespace(namespaceDecl);
                foreach (ClassDeclaration classDecl in namespaceDecl.ClassDeclarations)
                {
                    ClassNode classNode = AddClass(classDecl, namespaceNode);
                    foreach (ClassItemDeclaration classItem in classDecl.ClassItems)
                    {
                        switch (classItem)
                        {
                            case InferredFieldDeclaration iFieldDecl:
                                {
                                    var newNode = new InferredFieldNode(iFieldDecl, classNode);
                                    inferredFields.Add((newNode, iFieldDecl));
                                    classNode.AddFieldChild(newNode);
                                }
                                break;
                            case SimpleFieldDeclaration sFieldDecl:
                                {
                                    var newNode = new FieldNode(sFieldDecl, classNode);
                                    classNode.AddFieldChild(newNode);
                                }
                                break;
                            case MethodDeclaration methodDecl:
                                classNode.AddMethodChild(new MethodNode(methodDecl, classNode));
                                break;
                            case ConstructorDeclaration ctorDecl:
                                classNode.AddConstructorChild(new ConstructorNode(ctorDecl, classNode));
                                break;
                            default: throw new NotImplementedException();
                        }
                    }
                }
            }
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
                var newChild = new ClassNode(name, parent, new Modifiers(classDecl.Modifiers));
                parent.AddChild(newChild);
                return newChild;
            }
        }
    }
}