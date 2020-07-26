using System;
using System.Collections.Generic;
using System.Text;

namespace TypeChecker.SymbolNodes
{
    class ObjectClassNode : BuiltInClassNode
    {
        public ObjectClassNode(GlobalNode globalNode)
            :base("object", null, globalNode)
        {

        }
    }
}
