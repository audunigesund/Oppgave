using Cloudcustomers.API.Config;
using Cloudcustomers.API.Dtos;
using Microsoft.Extensions.Options;
using System;


public interface IDronesService{
    public Task<List<DroneDto>> getAllDrones();
}
public class DronesService : IDronesService
{
    private readonly HttpClient _httpClient;
    private readonly DronesApiOptions _apiConfig; 

    public DronesService(HttpClient httpClient, IOptions<DronesApiOptions> apiConfig)
    {
        _httpClient = httpClient;
        _apiConfig = apiConfig.Value;
    }
    public async Task<List<DroneDto>> getAllDrones()
    {
        var dronesResponse = await _httpClient.GetAsync(_apiConfig.Endpoint);
        if (dronesResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return new List<DroneDto>();
        }
        var responseContent=dronesResponse.Content;
        var allDrones= await responseContent.ReadFromJsonAsync<List<DroneDto>>();
  
        return allDrones.ToList();
  
       // var usersResponse = await _httpClient.GetAsync("https://localhost:7250/drones");
       // return new List<DroneDto> { };
    }
}

