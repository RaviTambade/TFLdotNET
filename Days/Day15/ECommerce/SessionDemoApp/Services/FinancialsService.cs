using Core.Models;
using Core.Repositories.Interfaces;
using Core.Services.Interfaces;
using System.Linq;

namespace Core.Services
{
    public class FinancialsService : IFinancialsService
    {
        private readonly IFruitRepository _fruitRepo;
        private readonly IFlowerRepository _flowerRepo;

        public FinancialsService(IFruitRepository fruitRepo,
                                 IFlowerRepository flowerRepo)
        {
            _fruitRepo = fruitRepo;
            _flowerRepo = flowerRepo;
        }

        public decimal GetTotalSold()
        {
            var flowerSold = _flowerRepo.GetAllSold().Sum(x => x.Profit);
            var fruitsSold = _fruitRepo.GetAllSold().Sum(x => x.Profit);

            return flowerSold + fruitsSold;
        }

        public FinancialStats GetStats()
        {
            FinancialStats stats = new FinancialStats();
            var flowerSold = _flowerRepo.GetAllSold();
            var fruitsSold = _fruitRepo.GetAllSold();

            //Calculate Average Stats
            stats.AverageFruitProfit = fruitsSold.Sum(x => x.Profit) / fruitsSold.Sum(x => x.Quantity);
            stats.AverageFlowerProfit = flowerSold.Sum(x => x.Profit) / flowerSold.Sum(x => x.Quantity);

            return stats;
        }
    }
}
