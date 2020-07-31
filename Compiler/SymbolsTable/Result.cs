using System;
using System.Collections.Generic;
using System.Text;

namespace SymbolsTable
{
    public enum Result
    {
        Success,

        //Class declaration
        DuplicateClass,

        //Class resolution
        ClassNotFound,
        NamespaceUsedAsType,

        //Class member declaration
        InstanceMemberInStaticClass,
        DuplicateMember,
        DifferentTypeMethods,
        DuplicateMethod,
        DuplicateConstructor,

        //Class member resolution
        InvalidStaticReference,
        InvalidInstanceReference,
        NoSuchOverload,
        NoSuchMember,
        NoSuchConstructor,

        //Local declaration
        LocalAlreadyDefinedInScope,
        LocalDefinedInEnclosingScope,
        LocalShadowsField,

        //Local resolution
        UsingLocalBeforeDeclaration,
        LocalNotFound,
    }
}
