namespace CRMAnalyticsApi.Models;

public class CRMAnalytics
{
    public int TotalCustomers { get; set; }

    public int ActiveLeads { get; set; }

    public int OpenSupportTickets { get; set; }

    public decimal TotalSales { get; set; }

    public int TodayFollowUps { get; set; }

    public string TopRegion { get; set; } = string.Empty;
}