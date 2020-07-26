using System;
using System.Collections.Generic;
using System.Text;
using TypeChecker.SymbolNodes;

namespace TypeChecker
{
    partial class SymbolsTable
    {
        public IEnumerable<ClassNode> Iterate => IterateThroughNamespace(globalNode);

        private ClassNode current;

        private IEnumerable<ClassNode> IterateThroughNamespace(NamespaceNode node)
        {
            foreach(SymbolNode child in node.Children)
            {
                if (child is ClassNode classChild)
                {
                    EnterClass(classChild);
                    current = classChild;
                    yield return classChild;
                    ExitClass();
                }
                else if (child is NamespaceNode namespaceChild)
                {
                    EnterNamespace(namespaceChild);
                    foreach (ClassNode classNode in IterateThroughNamespace(namespaceChild))
                    {
                        EnterClass(classNode);
                        current = classNode;
                        yield return classNode;
                        ExitClass();
                    }
                    ExitNamespace();
                }
                else throw new InvalidOperationException();
            }
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
            classStack = new Stack<ScopeInfo>();
            var newDict = new Dictionary<string, (TypeInfos.TypeInfo, int)>();
            foreach (KeyValuePair<string, FieldNode> field in classNode.Fields)
            {
                newDict.Add(field.Key, (field.Value.Type, 0));
            }
            classStack.Push(new ScopeInfo(newDict));
        }

        private void ExitClass()
        {
            if (classStack.Count != 1) throw new InvalidOperationException();
        }
    }
}
