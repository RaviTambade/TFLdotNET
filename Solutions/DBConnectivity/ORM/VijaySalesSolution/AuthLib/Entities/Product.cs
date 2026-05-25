using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLib.Entities
{
    public  class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double UnitPrice {  get; set; }
        public int StockAvaible {  get; set; }

    }
}
