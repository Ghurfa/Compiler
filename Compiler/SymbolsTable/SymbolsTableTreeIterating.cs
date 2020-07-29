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
        private ClassNode current;

        public IEnumerable<ClassNode> IterateClasses => IterateThroughNamespace(globalNode, true, true);
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
                    EnterClass(classChild, initFields);
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

        private void EnterClass(ClassNode classNode, bool initFields)
        {
            if (initFields)
            {
                scopeStack.Clear();
            }
            current = classNode;
        }

        private void ExitClass()
        {
            if (scopeStack.Count != 0) throw new InvalidOperationException();
        }

        public void EnterMethod(ParameterListDeclaration parameterListDecl)
        {
            EnterScope(0);
            parameters.Clear();
            for (int i = 0; i < parameterListDecl.Parameters.Length; i++)
            {
                var param = parameterListDecl.Parameters[i];
                var paramInfo = new ParamLocal(ValueTypeInfo.Get(this, param.Type), i);
                parameters.Add(param.Identifier.Text, paramInfo);
            }
        }

        public void ExitMethod() => ExitScope();

        public void EnterScope(int indexInParent)
        {
            scopeStack.Push(new ScopeInfo(indexInParent));
        }

        public void ExitScope() => scopeStack.Pop();
    }
}
