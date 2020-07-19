using System;
using System.Collections.Generic;
using System.Text;

namespace LDefParser.ProductionDefinitions
{
    class SingleDefinition : ProductionDefinition
    {
        public ProductionItem[] Items { get; set; }

        public static bool TryReadSingleDefinition(StringBuffer sb, out SingleDefinition singleDefinition)
        {
            sb.Save();
            if(sb.TryRead('='))
            {
                List<ProductionItem> items = new List<ProductionItem>();
                while(ProductionItem.TryReadProductionItem(sb, out var item))
                {
                    items.Add(item);
                }
                if (items.Count == 0) throw new InvalidOperationException();
                singleDefinition = new SingleDefinition() { Items = items.ToArray() };
                sb.Pop();
                return true;
            }
            else
            {
                singleDefinition = default;
                sb.Restore();
                return false;
            }
        }
    }
}
