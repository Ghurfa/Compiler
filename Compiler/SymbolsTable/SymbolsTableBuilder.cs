using Parser;
using System;
using System.Collections.Generic;
using System.Text;
using SymbolsTable.Nodes;
using SymbolsTable.TypeInfos;
using Parser.SyntaxTreeItems;
using Parser.SyntaxTreeItems.ClassItemDeclarations;
using Parser;

namespace SymbolsTable
{
    public class SymbolsTableBuilder : SymbolsTable
    {
        public void BuildTree(IEnumerable<NamespaceDeclaration> namespaces, out List<(InferredField, InferredFieldDeclaration)> inferredFields)
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

            inferredFields = new List<(InferredField, InferredFieldDeclaration)>();

            foreach (ClassNode classNode in IterateFirstPass)
            {
                foreach (ClassItemDeclaration classItem in classNode.Declaration.ClassItems)
                {
                    switch (classItem)
                    {
                        case InferredFieldDeclaration iFieldDecl:
                            {
                                var newNode = new InferredField(iFieldDecl);
                                inferredFields.Add((newNode, iFieldDecl));
                                classNode.AddField(newNode);
                            }
                            break;
                        case SimpleFieldDeclaration sFieldDecl:
                            {
                                var newNode = new SimpleField(this, sFieldDecl);
                                classNode.AddField(newNode);
                            }
                            break;
                        case MethodDeclaration methodDecl:
                            classNode.AddMethod(new Method(this, methodDecl));
                            break;
                        case ConstructorDeclaration ctorDecl:
                            classNode.AddConstructor(new Constructor(this, ctorDecl));
                            break;
                        default: throw new NotImplementedException();
                    }
                }
            }
        }

        public void EnterNewScope(int statementIndex)
        {
            currentScope.Children.Add(new Scope(statementIndex, currentScope));
            EnterScope();
        }

        private void InitializeTree()
        {
            void add(string name)
            {
                var newNode = new BuiltInClassNode(name, objectNode, globalNode);
                globalNode.AddChild(newNode);
                defaultCachedClasses.Add(name, newNode);
            }

            globalNode = new GlobalNode();
            objectNode = new BuiltInClassNode("object", null, globalNode);
            ArrayNode = new BuiltInClassNode("array", objectNode, globalNode);

            defaultCachedClasses.Clear();

            string[] primitiveTypes = new[] { "int", "bool", "string", "char", "float", "double" };

            foreach (string primType in primitiveTypes)
                add(primType);

            ValueTypeInfo.Initialize(this, primitiveTypes);

            ValueTypeInfo stringType = ValueTypeInfo.PrimitiveTypes["string"];
            objectNode.AddMethod(new Method("ToString", new FunctionTypeInfo(stringType, new ValueTypeInfo[] { }), Modifiers.Public));
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

        private Result AddClass(ClassDeclaration classDecl, SymbolNode parent)
        {
            string name = classDecl.Name.Text;
            if (parent.TryGetChild(name, out SymbolNode _))
                return Result.DuplicateClass;
            else
            {
                var newChild = new ClassNode(name, classDecl, objectNode, parent, new Modifiers(classDecl.Modifiers), defaultCachedClasses);
                parent.AddChild(newChild);
                return Result.Success;
            }
        }
    }
}
