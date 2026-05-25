using CRMAnalyticsApi.Models;

namespace CRMAnalyticsApi.Services;

public interface ICRMAnalyticsService
{
    Task<CRMAnalytics> GetDashboardAnalyticsAsync();
}