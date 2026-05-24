using Core.Models;
using Core.Repositories.Interfaces;
using System.Collections.Generic;

namespace Core.Repositories
{
    public class FlowerRepository : IFlowerRepository
    {
        public List<Flower> GetAll()
        {
            //In a real project, this is where you would call your database/datastore for this info
            List<Flower> items = new List<Flower>()
            {
                new Flower()
                {
                    ID = 14,
                    Name = "Summer Breeze Flower Box",
                    SalePrice = 4.99M,
                    UnitPrice = 1.69M,
                    Quantity = 43
                },
                new Flower()
                {
                    ID = 3,
                    Name = "Yellow Mellow Sunshine Bouquet",
                    SalePrice = 4.89M,
                    UnitPrice = 1.13M,
                    Quantity = 319
                },
                new Flower()
                {
                    ID = 18,
                    Name = "Sunshine Floral Ecstasy",
                    SalePrice = 5.69M,
                    UnitPrice = 0.47M,
                    Quantity = 319
                },
                new Flower()
                {
                    ID = 19,
                    Name = "Red Rose Beautiful Bunch",
                    SalePrice = 6.19M,
                    UnitPrice = 0.59M,
                    Quantity = 252
                },
                new Flower()
                {
                    ID = 1,
                    Name = "Dreamy Hues",
                    SalePrice = 5.59M,
                    UnitPrice = 1.12M,
                    Quantity = 217
                }
            };
            return items;
        }
        public Flower GetById(int id)
        {
            List<Flower> allFlowers=GetAll();
            var found = allFlowers.Find(x => x.ID == id);
            Flower theFlower=found as Flower;
            return theFlower;
        }
        public List<Flower> GetAllSold()
        {
            //In a real project, this is where you would call your database/datastore for this info
            List<Flower> items = new List<Flower>()
            {
                new Flower()
                {
                    ID = 14,
                    Name = "Summer Breeze Flower Box",
                    SalePrice = 4.99M,
                    UnitPrice = 1.69M,
                    Quantity = 43
                },
                new Flower()
                {
                    ID = 3,
                    Name = "Yellow Mellow Sunshine Bouquet",
                    SalePrice = 4.89M,
                    UnitPrice = 1.13M,
                    Quantity = 319
                },
                new Flower()
                {
                    ID = 18,
                    Name = "Sunshine Floral Ecstasy",
                    SalePrice = 5.69M,
                    UnitPrice = 0.47M,
                    Quantity = 319
                },
                new Flower()
                {
                    ID = 19,
                    Name = "Red Rose Beautiful Bunch",
                    SalePrice = 6.19M,
                    UnitPrice = 0.59M,
                    Quantity = 252
                },
                new Flower()
                {
                    ID = 1,
                    Name = "Dreamy Hues",
                    SalePrice = 5.59M,
                    UnitPrice = 1.12M,
                    Quantity = 217
                }
            };
            return items;
        }
    }
}
