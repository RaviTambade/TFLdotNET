using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
 
namespace Core.Models
{
    public class FinancialStats
    {
        [DisplayName("Avg. Fruit Profit: ")]
        [DataType(DataType.Currency)]
        public decimal AverageFruitProfit { get; set; }

        [DisplayName("Avg. flower Item Profit: ")]
        [DataType(DataType.Currency)]
        public decimal AverageFlowerProfit { get; set; }
    }
}
