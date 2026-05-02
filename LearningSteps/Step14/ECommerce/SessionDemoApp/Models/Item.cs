using System;
using  Core.Models;
namespace DemoApp.Models
{
    [Serializable] 
    public class Item
    {
       public Flower theFlower{get;set;}
       public int Quantity{get;set;}
        public Item(){    }
    }
}
