using System.Net.Http.Json;
using BlazorAppWebAssembly.Entities;

namespace BlazorAppWebAssembly.Services
{
    public class CustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Customer>> GetEmployeesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Customer>>("api/customers");
        }
    }
}
