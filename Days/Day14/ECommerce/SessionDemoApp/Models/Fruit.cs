using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Core.Models
{
    public class Fruit
    {
        public int ID { get; set; }
        [DisplayName("Movie Name")]
        public string MovieName { get; set; }
        [DisplayName("Sale Price")]
        [DataType(DataType.Currency)]
        public decimal SalePrice { get; set; }
        [DisplayName("Studio Percentage")]
        public decimal StudioCutPercentage { get; set; }
        [DisplayName("Quantity")]
        public int Quantity { get; set; }
        [DisplayName("Total Profit")]
        [DataType(DataType.Currency)]
        public decimal Profit
        {
            get
            {
                return (Quantity * SalePrice) - (StudioCutPercentage * (Quantity * SalePrice));
            }
        }
        [DisplayName("Profit Per Item")]
        [DataType(DataType.Currency)]
        public decimal ProfitPerItem
        {
            get
            {
                return SalePrice - (StudioCutPercentage * SalePrice);
            }
        }
    }
}
