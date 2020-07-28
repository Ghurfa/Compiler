using Compiler.SyntaxTreeItems;
using System;
using System.Collections.Generic;
using System.Text;
using TypeChecker.SymbolNodes;
using TypeChecker.TypeInfos;

namespace TypeChecker
{
    public partial class SymbolsTable
    {
        private ClassNode current;

        public IEnumerable<ClassNode> Iterate => IterateThroughNamespace(globalNode, true, true);
        public IEnumerable<ClassNode> IterateWithoutStack => IterateThroughNamespace(globalNode, false, false);
        private IEnumerable<ClassNode> IterateIgnoreFields => IterateThroughNamespace(globalNode, false, true);

        private IEnumerable<ClassNode> IterateThroughNamespace(NamespaceNode node, bool initClassStack, bool modifyStack)
        {
            if(modifyStack) EnterNamespace(node);

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
                    foreach (ClassNode classNode in IterateThroughNamespace(namespaceChild, initClassStack, modifyStack))
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

        private void EnterClass(ClassNode classNode, bool initClassStack)
        {
            if (initClassStack)
            {
                classStack = new Stack<ScopeInfo>();
                var newDict = new Dictionary<string, LocalInfo>();
                foreach (KeyValuePair<string, FieldNode> field in classNode.Fields)
                {
                    newDict.Add(field.Key, new LocalInfo(field.Value.Type, 0, !field.Value.Modifiers.IsStatic));
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

        public void EnterMethod(ParameterListDeclaration parameters)
        {
            EnterScope(1);
            foreach (ParameterDeclaration param in parameters.Parameters)
            {
                AddLocal(param.Identifier.Text, ValueTypeInfo.Get(this, param.Type), -1);
            }
        }

        public void ExitMethod() => ExitScope();

        public void EnterScope(int indexInParent)
        {
            classStack.Push(new ScopeInfo(indexInParent));
        }

        public void ExitScope() => classStack.Pop();
    }
}
