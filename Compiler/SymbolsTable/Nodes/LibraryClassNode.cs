using SymbolsTable.ItemInfos;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SymbolsTable.Nodes
{
    public class LibraryClassNode : ClassNode
    {
        public Type Type { get; private set; }

        private SymbolsTable table;

        private bool parentClassLoaded = false;
        public override ClassNode ParentClass 
        {
            get
            {
                if (!parentClassLoaded) LoadParentClass();
                return base.ParentClass;
            }
            set => base.ParentClass = value; 
        }

        private bool fieldsLoaded = false;
        public override Dictionary<string, Field> Fields
        {
            get
            {
                if (!fieldsLoaded) LoadFields();
                return base.Fields;
            }
            set => base.Fields = value;
        }

        private bool ctorsLoaded = false;
        public override List<Constructor> Constructors
        {
            get
            {
                if (!ctorsLoaded) LoadConstructors();
                return base.Constructors;
            }
            set => base.Constructors = value;
        }

        private bool methodsLoaded = false;
        public override List<Method> Methods
        {
            get
            {
                if (!methodsLoaded) LoadMethods();
                return base.Methods;
            }
            set => base.Methods = value;
        }

        public LibraryClassNode(SymbolsTable table, SymbolNode parent, Type type)
            : base(type.Name, parent, makeModifiers(type))
        {
            this.table = table;
            Type = type;
        }

        private static Modifiers makeModifiers(Type type)
        {
            return new Modifiers(AccessModifier.PublicModifier, type.IsAbstract && type.IsSealed);
        }

        private void LoadParentClass()
        {
            System.Type baseType = Type.BaseType;
            if (baseType != null)
            {
                table.GetLibraryClass(baseType, out LibraryClassNode parent);
                base.ParentClass = parent;
            }
            parentClassLoaded = true;
        }

        private void LoadFields()
        {
            base.Fields = new Dictionary<string, Field>();
            foreach (FieldInfo field in Type.GetFields())
            {
                base.Fields.Add(field.Name, new LibraryField(table, field));
            }
            fieldsLoaded = true;
        }

        private void LoadConstructors()
        {
            base.Constructors = new List<Constructor>();
            foreach(ConstructorInfo ctor in Type.GetConstructors())
            {
                base.Constructors.Add(new LibraryConstructor(table, ctor));
            }
            ctorsLoaded = true;
        }

        private void LoadMethods()
        {
            base.Methods = new List<Method>();
            foreach(MethodInfo method in Type.GetMethods())
            {
                base.Methods.Add(new LibraryMethod(table, method));
            }
            methodsLoaded = true;
        }
    }
}
