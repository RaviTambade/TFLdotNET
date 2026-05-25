using CRMAnalyticsApi.Models;

namespace CRMAnalyticsApi.Repositories;

public class CRMRepository
{
    public async Task<CRMAnalytics> GetAnalyticsAsync()
    {
        // Simulate heavy DB query
        await Task.Delay(5000);

        return new CRMAnalytics
        {
            TotalCustomers = 1500,
            ActiveLeads = 320,
            OpenSupportTickets = 47,
            TotalSales = 1250000,
            TodayFollowUps = 89,
            TopRegion = "Pune"
        };
    }
}