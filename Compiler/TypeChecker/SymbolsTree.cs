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
            globalNode.AddChild(new ClassNode("int", globalNode, Modifiers.Public));
            globalNode.AddChild(new ClassNode("bool", globalNode, Modifiers.Public));
            globalNode.AddChild(new ClassNode("string", globalNode, Modifiers.Public));
            globalNode.AddChild(new ClassNode("char", globalNode, Modifiers.Public));
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

        public ClassNode AddClass(ClassDeclaration classDecl, SymbolNode parent)
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
