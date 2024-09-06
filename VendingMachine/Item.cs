using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    internal class Item
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockAmount { get; set; }

        public Item(string name, decimal price, int stockamount)
        {
            Name = name;
            Price = price;
            StockAmount = stockamount;
        }

        
        

    }
}
