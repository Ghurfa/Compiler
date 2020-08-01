using Parser;
using System;
using System.Collections.Generic;
using System.Text;
using SymbolsTable.Nodes;
using SymbolsTable.TypeInfos;
using Parser.SyntaxTreeItems;
using Parser.SyntaxTreeItems.ClassItemDeclarations;
using System.Linq;

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


        public Result AddLocal(string name, ValueTypeInfo type, int index)
        {
            Scope ancestor = currentScope;
            while (!(ancestor is FunctionScope))
            {
                if (ancestor.Locals.ContainsKey(name))
                {
                    if (ancestor == currentScope) return Result.LocalAlreadyDefinedInScope;
                    else return Result.LocalDefinedInEnclosingScope;
                }
                ancestor = ancestor.ParentScope;
            }

            if (((FunctionScope)ancestor).Parameters.ContainsKey(name)) return Result.LocalDefinedInEnclosingScope;

            currentScope.Locals.Add(name, new BodyLocal(type, index + 1));

            return Result.Success;
        }

        public Result GetLocal(string name, int statementIndex, out Local local)
        {
            Scope ancestor = currentScope;
            while (!(ancestor is FunctionScope))
            {
                if (ancestor.Locals.TryGetValue(name, out BodyLocal bodyLocal))
                {
                    if (bodyLocal.Index > statementIndex && statementIndex >= 0)
                    {
                        local = null;
                        return Result.UsingLocalBeforeDeclaration;
                    }
                    local = bodyLocal;
                    return Result.Success;
                }
                statementIndex = ancestor.IndexInParent;
                ancestor = ancestor.ParentScope;
            }

            if (ancestor.Locals.TryGetValue(name, out BodyLocal funcBodyLocal))
            {
                if (funcBodyLocal.Index > statementIndex && statementIndex >= 0)
                {
                    local = null;
                    return Result.UsingLocalBeforeDeclaration;
                }
                local = funcBodyLocal;
                return Result.Success;
            }
            else if (((FunctionScope)ancestor).Parameters.TryGetValue(name, out ParamLocal paramLocal))
            {
                local = paramLocal;
                return Result.Success;
            }

            local = null;
            return Result.LocalNotFound;
        }

        public Result GetField(string name, bool isStatic, PrimaryExpression expr, out Field field)
            => GetField(currentClass, name, isStatic, expr, out field);


        public Result GetField(string baseIdentifier, string fieldName, int statementIndex, PrimaryExpression expr, out Field field)
        {
            if (GetLocal(baseIdentifier, statementIndex, out Local baseLocal) == Result.Success)
            {
                return GetField(baseLocal.Type.Class, fieldName, false, expr, out field);
            }
            else
            {
                Result getClassResult = GetClass(baseIdentifier, out ClassNode classNode);
                if (getClassResult != Result.Success)
                {
                    field = null;
                    return getClassResult;
                }

                return GetField(classNode, fieldName, true, expr, out field);
            }
        }

        public Result GetField(ClassNode classNode, string fieldName, bool isStatic, PrimaryExpression expr, out Field field)
        {
            if (classNode.Fields.TryGetValue(fieldName, out field))
            {
                currentScope.ReferencedFields[expr] = field;
                if (!isStatic && field.Modifiers.IsStatic) return Result.InvalidStaticReference;
                else if (isStatic && !field.Modifiers.IsStatic) return Result.InvalidInstanceReference;
                else return Result.Success;
            }
            else
            {
                field = null;
                return Result.NoSuchMember;
            }
        }

        public Result GetMethod(string methodName, IEnumerable<ValueTypeInfo> paramTypes, MethodCallExpression methodCall, out Method method)
            => GetMethod(currentClass, methodName, paramTypes, methodCall, out method);

        public Result GetMethod(ClassNode classNode, string methodName, IEnumerable<ValueTypeInfo> paramTypes, MethodCallExpression methodCall, out Method method)
        {
            ClassNode current = classNode;
            bool foundMethodWithName = false;
            while (current != null)
            {
                foreach (Method other in current.Methods)
                {
                    if (other.Name == methodName)
                    {
                        if (Enumerable.SequenceEqual(other.Type.Parameters, paramTypes))
                        {
                            method = other;
                            currentScope.ReferencedMethods[methodCall] = method;
                            return Result.Success;
                        }
                        foundMethodWithName = true;
                    }
                }
                current = current.ParentClass;
            }
            method = null;
            if (foundMethodWithName) return Result.NoSuchOverload;
            else return Result.NoSuchMember;
        }

        public Result GetConstructor(ClassNode classNode, IEnumerable<ValueTypeInfo> paramTypes, NewObjectExpression newObj, out Constructor constructor)
        {
            ClassNode current = classNode;
            while (current != null)
            {
                foreach (Constructor other in current.Constructors)
                {
                    if (Enumerable.SequenceEqual(other.ParamTypes, paramTypes))
                    {
                        constructor = other;
                        currentScope.ReferencedConstructors[newObj] = constructor;
                        return Result.Success;
                    }
                }
                current = current.ParentClass;
            }
            constructor = null;
            return Result.NoSuchConstructor;
        }

    }
}
