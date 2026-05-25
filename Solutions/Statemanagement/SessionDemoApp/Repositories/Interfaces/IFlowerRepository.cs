using Core.Models;
using System.Collections.Generic;

namespace Core.Repositories.Interfaces
{
    public interface IFlowerRepository
    {
        List<Flower> GetAll();
        Flower GetById(int id);
        List<Flower> GetAllSold();
    }
}
