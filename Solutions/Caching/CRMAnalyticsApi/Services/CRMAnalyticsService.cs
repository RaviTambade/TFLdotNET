using System.Text.Json;
using CRMAnalyticsApi.Models;
using CRMAnalyticsApi.Repositories;
using Microsoft.Extensions.Caching.Distributed;

namespace CRMAnalyticsApi.Services;

public class CRMAnalyticsService : ICRMAnalyticsService
{
    private readonly IDistributedCache _cache;

    private readonly CRMRepository _repository;

    private readonly ILogger<CRMAnalyticsService> _logger;

    private const string CACHE_KEY = "CRM_ANALYTICS";

    public CRMAnalyticsService(
        IDistributedCache cache,
        CRMRepository repository,
        ILogger<CRMAnalyticsService> logger)
    {
        _cache = cache;
        _repository = repository;
        _logger = logger;
    }

    public async Task<CRMAnalytics> GetDashboardAnalyticsAsync()
    {
        // Check Redis Cache
        var cachedData =
            await _cache.GetStringAsync(CACHE_KEY);

        if (!string.IsNullOrEmpty(cachedData))
        {
            _logger.LogInformation(
                "Analytics loaded from Redis Cache");

            return JsonSerializer.Deserialize<CRMAnalytics>(
                cachedData)!;
        }

        _logger.LogInformation(
            "Analytics loaded from Database");

        // Simulate database fetch
        var analytics =
            await _repository.GetAnalyticsAsync();

        // Serialize
        var serializedData =
            JsonSerializer.Serialize(analytics);

        // Cache options
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow =
                TimeSpan.FromMinutes(10)
        };

        // Store in Redis
        await _cache.SetStringAsync(
            CACHE_KEY,
            serializedData,
            options);

        return analytics;
    }
}