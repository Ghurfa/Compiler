using SymbolsTable.Nodes;
using SymbolsTable.TypeInfos;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SymbolsTable.ItemInfos
{
    public class LibraryField : Field
    {
        public FieldInfo FieldInfo { get; private set; }
        private SymbolsTable table;

        private bool typeLoaded = false;
        public override ValueTypeInfo Type 
        { 
            get
            {
                if (!typeLoaded) LoadType();
                return base.Type;
            }
            set => base.Type = value; 
        }

        public LibraryField(SymbolsTable table, FieldInfo field)
            : base(field.Name, getModifiers(field))
        {
            this.table = table;
            FieldInfo = field;
        }

        private void LoadType()
        {
            table.GetLibraryClass(FieldInfo.FieldType, out LibraryClassNode typeNode);
            base.Type = ValueTypeInfo.Get(typeNode);
            typeLoaded = true;
        }
        
        private static Modifiers getModifiers(FieldInfo field)
        {
            AccessModifier accessModifier;
            if (field.IsPublic) accessModifier = AccessModifier.PublicModifier;
            else if (field.IsPrivate) accessModifier = AccessModifier.PrivateModifier;
            else accessModifier = AccessModifier.ProtectedModifier;
            return new Modifiers(accessModifier, field.IsStatic);
        }
    }
}
