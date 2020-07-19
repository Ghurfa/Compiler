using System;
using System.Collections.Generic;
using System.Text;

namespace LDefParser.ProductionItems
{
    class RepeatedProductionItem : ProductionItem
    {
        public ProductionItem Inner { get; set; }
        public RepeatedProductionItem(ProductionItem inner, string name) : base(name, inner.Type + "[]")
        {
            Inner = inner;
        }
    }
}
