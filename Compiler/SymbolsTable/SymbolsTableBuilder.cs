using Parser;
using System;
using System.Collections.Generic;
using System.Text;
using SymbolsTable.Nodes;
using SymbolsTable.TypeInfos;
using Parser.SyntaxTreeItems;
using Parser.SyntaxTreeItems.ClassItemDeclarations;

namespace SymbolsTable
{
    public class SymbolsTableBuilder : SymbolsTable
    {
        protected Dictionary<string, ClassNode> defaultCachedClasses;
        private ClassNode objectNode;

        public SymbolsTableBuilder()
        {
            defaultCachedClasses = new Dictionary<string, ClassNode>();
        }

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
                            classNode.AddField(new SimpleField(this, sFieldDecl));
                            break;
                        case MethodDeclaration methodDecl:
                            {
                                var newMethod = new Method(this, methodDecl);
                                classNode.AddMethod(newMethod);
                                methodScopes.Add(newMethod, new FunctionScope(this, newMethod.Declaration.ParameterList, newMethod.Modifiers.IsStatic));
                            }
                            break;
                        case ConstructorDeclaration ctorDecl:
                            {
                                var newCtor = new Constructor(this, ctorDecl);
                                classNode.AddConstructor(newCtor);
                                constructorScopes.Add(newCtor, new FunctionScope(this, newCtor.Declaration.ParameterList, newCtor.Modifiers.IsStatic));
                            }
                            break;
                        default: throw new NotImplementedException();
                    }
                }
            }
        }

        public void EnterNewScope(int statementIndex)
        {
            currentScope.Children.Add(new Scope(statementIndex, currentScope));
            EnterNextScope();
        }

        public override Result GetClass(string name, out ClassNode classNode)
        {
            if (base.GetClass(name, out classNode) == Result.Success)
                return Result.Success;
            else
            {
                NamespaceNode parent = (NamespaceNode)currentClass.Parent;
                while (parent != null)
                {
                    if (parent.TryGetChild(name, out SymbolNode child))
                    {
                        if (child is ClassNode ret)
                        {
                            currentClass.CachedClasses.Add(name, ret);
                            classNode = ret;
                            return Result.Success;
                        }
                        else if (child is NamespaceNode)
                        {
                            classNode = null;
                            return Result.NamespaceUsedAsType;
                        }
                        else throw new InvalidOperationException();
                    }
                    parent = (NamespaceNode)parent.Parent;
                }

                if (systemNamespace.TryGetChild(name, out SymbolNode systemChild))
                {
                    if (systemChild is ClassNode ret)
                    {
                        currentClass.CachedClasses.Add(name, ret);
                        classNode = ret;
                        return Result.Success;
                    }
                    else if (systemChild is NamespaceNode)
                    {
                        classNode = null;
                        return Result.NamespaceUsedAsType;
                    }
                    else throw new InvalidOperationException();
                }
                else
                {
                    classNode = null;
                    return Result.ClassNotFound;
                }
            }
        }

        private void InitializeTree()
        {
            globalNode = new GlobalNode();
            systemNamespace = new LibraryNamespaceNode(this, globalNode);
            globalNode.AddChild(systemNamespace);

            GetSystemClass("Object", out objectNode);

            ValueTypeInfo.Initialize(this);

            methodScopes = new Dictionary<Method, FunctionScope>();
            constructorScopes = new Dictionary<Constructor, FunctionScope>();
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
