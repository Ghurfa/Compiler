using System;
using System.Collections.Generic;
using System.Text;
using SymbolsTable.Nodes;
using SymbolsTable.TypeInfos;
using Parser.SyntaxTreeItems;

namespace SymbolsTable
{
    public partial class SymbolsTable
    {
        private ClassNode currentClass;
        protected Scope currentScope;
        protected Dictionary<Method, FunctionScope> methodScopes;
        protected Dictionary<Constructor, FunctionScope> constructorScopes;

        public IEnumerable<ClassNode> IterateWithStack => IterateThroughNamespace(globalNode, true, true);
        public IEnumerable<ClassNode> IterateWithoutStack => IterateThroughNamespace(globalNode, false, false);
        protected IEnumerable<ClassNode> IterateFirstPass => IterateThroughNamespace(globalNode, false, true);

        private IEnumerable<ClassNode> IterateThroughNamespace(NamespaceNode node, bool initFields, bool modifyStack)
        {
            if (modifyStack) EnterNamespace(node);

            foreach (SymbolNode child in node.Children)
            {
                if (child is BuiltInClassNode _) continue;
                else if (child is ClassNode classChild)
                {
                    EnterClass(classChild);
                    yield return classChild;
                    ExitClass();
                }
                else if (child is NamespaceNode namespaceChild)
                {
                    foreach (ClassNode classNode in IterateThroughNamespace(namespaceChild, initFields, modifyStack))
                    {
                        yield return classNode;
                    }
                }
                else throw new InvalidOperationException();
            }

            if (modifyStack) ExitNamespace();
        }

        private void EnterNamespace(NamespaceNode namespaceNode)
        {
            var newDict = new Dictionary<string, ClassNode>();
            foreach (SymbolNode child in namespaceNode.Children)
            {
                if (child is ClassNode classNode)
                {
                    newDict.Add(classNode.Name, classNode);
                }
            }
            namespaceStack.Push(newDict);
        }

        private void ExitNamespace() => namespaceStack.Pop();

        private void EnterClass(ClassNode classNode)
        {
            currentClass = classNode;
        }

        private void ExitClass() { }

        public void EnterFunction(Method method)
        {
            currentScope = methodScopes[method];
            currentScope.Begin();
        }

        public void EnterFunction(Constructor constructor)
        {
            currentScope = constructorScopes[constructor];
            currentScope.Begin();
        }

        public void ExitFunction() => ExitScope();

        public void EnterNextScope()
        {
            if (currentScope.TryGetNextChild(out Scope child))
            {
                currentScope = child;
                currentScope.Begin();
            }
            else throw new InvalidOperationException();
        }

        public void ExitScope() => currentScope = currentScope.ParentScope;
    }
}
