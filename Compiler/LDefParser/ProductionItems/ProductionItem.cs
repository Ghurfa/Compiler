using LDefParser.ProductionItems;
using System;

namespace LDefParser
{
    abstract class ProductionItem
    {
        public string Name { get; protected set; }
        public string Type { get; protected set; }

        public string LowerCaseName => Name.Substring(0, 1).ToLower() + Name.Substring(1);

        protected ProductionItem(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public static bool TryReadProductionItem(StringBuffer sb, out ProductionItem item)
        {
            sb.Save();
            ProductionItem baseItem;

            if (sb.TryReadWord(out string word))
            {
                if (sb.TryRead(':'))
                {
                    if (sb.TryReadWord(out string name))
                    {
                        baseItem = new NormalProductionItem(name, word);
                    }
                    else throw new InvalidOperationException();
                }
                else baseItem = new NormalProductionItem(word, word);
            }
            else if (sb.TryReadToken(out string tokenName))
            {
                if (sb.TryRead(':'))
                {
                    if (sb.TryReadWord(out string name))
                    {
                        baseItem = new TokenProductionItem(name, tokenName + "Token");
                    }
                    else throw new InvalidOperationException();
                }
                else baseItem = new TokenProductionItem(tokenName, tokenName + "Token");
            }
            else
            {
                sb.Restore();
                item = default;
                return false;
            }

            if (sb.TryRead('+'))
            {
                if (sb.TryRead(':') && sb.TryReadWord(out string name))
                {
                    item = new RepeatedProductionItem(baseItem, name);
                }
                else throw new InvalidOperationException();

                sb.Pop();
                return true;
            }
            else
            {
                sb.Pop();
                item = baseItem;
                return true;
            }
        }
    }
}
