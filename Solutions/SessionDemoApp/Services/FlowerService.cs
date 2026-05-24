using Core.Models;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;
using System.Collections.Generic;

namespace Core.Services
{
    public class FlowerService : IFlowerService 
    {
        private readonly IFlowerRepository _flowerRepo;
        public FlowerService(IFlowerRepository flowerRepo)
        {
            _flowerRepo = flowerRepo;
        }
        public List<Flower> GetAll() => _flowerRepo.GetAll();
        public Flower GetById(int id)=>_flowerRepo.GetById(id);
        public List<Flower> GetAllSold() => _flowerRepo.GetAllSold();
    }
}