using SymbolsTable.Nodes;
using SymbolsTable.TypeInfos;
using System.Reflection;

namespace SymbolsTable.ItemInfos
{
    public class LibraryConstructor : Constructor
    {
        public ConstructorInfo ConstructorInfo { get; private set; }
        private SymbolsTable table;

        private bool paramTypesLoaded = false;
        public override ValueTypeInfo[] ParamTypes 
        {
            get
            {
                if (!paramTypesLoaded) LoadParamTypes();
                return base.ParamTypes;
            }
        }

        public LibraryConstructor(SymbolsTable table, ConstructorInfo constructor)
            : base("$ctor", GetModifiers(constructor))
        {
            this.table = table;
            ConstructorInfo = constructor;
        }

        private void LoadParamTypes()
        {
            var parameters = ConstructorInfo.GetParameters();
            base.ParamTypes = new ValueTypeInfo[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                table.GetLibraryClass(parameters[i].ParameterType, out LibraryClassNode retNode);
                base.ParamTypes[i] = ValueTypeInfo.Get(retNode);
            }
            paramTypesLoaded = true;
        }

        private static Modifiers GetModifiers(ConstructorInfo ctor)
        {
            AccessModifier accessModifier;
            if (ctor.IsPublic) accessModifier = AccessModifier.PublicModifier;
            else if (ctor.IsPrivate) accessModifier = AccessModifier.PrivateModifier;
            else accessModifier = AccessModifier.ProtectedModifier;
            return new Modifiers(accessModifier, false);
        }
    }
}
