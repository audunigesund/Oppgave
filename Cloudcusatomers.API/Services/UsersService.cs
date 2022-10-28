using Cloudcustomers.API.Dtos;
using Cloudcustomers.API.Models;
using System;


public interface IUsersService{
    public Task<List<User>> getAllUsers();
}

public class UsersService:IUsersService { 
    private readonly HttpClient _httpClient;

    public UsersService(HttpClient httpClient){
        _httpClient = httpClient;
    }
    public async Task<List<User>> getAllUsers()
    {
        var usersResponse = await _httpClient.GetAsync("https://example.com");
        return new List<User> { };
    }
   public async Task<List<DroneDto>> getAllDrones()
    {
        var usersResponse = await _httpClient.GetAsync("https://localhost:7250/drones");
        return new List<DroneDto> { };
    }

    /*Task<List<User>> IUsersService.getAllUsers(){
        throw new NotImplementedException();
    }*/

    /*public Task getAllUsers()
    {
        throw new NotImplementedException();
    }*/
}
