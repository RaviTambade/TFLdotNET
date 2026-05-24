using Core.Models;

namespace Core.Services.Interfaces
{
    public interface IFinancialsService
    {
        decimal GetTotalSold();
        FinancialStats GetStats();
    }
}
