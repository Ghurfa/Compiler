using System;
using System.Collections.Generic;
using System.Text;
using TypeChecker.SymbolNodes;

namespace TypeChecker
{
    partial class SymbolsTable
    {
        private ClassNode current;

        public IEnumerable<ClassNode> Iterate => IterateThroughNamespace(globalNode, true);
        private IEnumerable<ClassNode> IterateIgnoreFields => IterateThroughNamespace(globalNode, false);

        private IEnumerable<ClassNode> IterateThroughNamespace(NamespaceNode node, bool initClassStack)
        {
            EnterNamespace(node);
            foreach(SymbolNode child in node.Children)
            {
                if (child is BuiltInClassNode _) continue;
                else if (child is ClassNode classChild)
                {
                    EnterClass(classChild, initClassStack);
                    yield return classChild;
                    ExitClass(initClassStack);
                }
                else if (child is NamespaceNode namespaceChild)
                {
                    foreach (ClassNode classNode in IterateThroughNamespace(namespaceChild, initClassStack))
                    {
                        yield return classNode;
                    }
                }
                else throw new InvalidOperationException();
            }
            ExitNamespace();
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

        private void EnterClass(ClassNode classNode, bool initClassStack)
        {
            if (initClassStack)
            {
                classStack = new Stack<ScopeInfo>();
                var newDict = new Dictionary<string, (TypeInfos.TypeInfo, int)>();
                foreach (KeyValuePair<string, FieldNode> field in classNode.Fields)
                {
                    newDict.Add(field.Key, (field.Value.Type, 0));
                }
                classStack.Push(new ScopeInfo(newDict));
            }
            current = classNode;
        }

        private void ExitClass(bool shouldBeOne)
        {
            int expectedVal = shouldBeOne ? 1 : 0;
            if (classStack.Count != expectedVal) throw new InvalidOperationException();
        }
    }
}
