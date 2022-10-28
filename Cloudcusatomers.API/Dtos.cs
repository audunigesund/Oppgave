using System;
namespace Cloudcustomers.API.Dtos
{
    public record DroneDto(Guid Id, string Name, double Latitude, double Longitude, DateTimeOffset CreatedDate);
    public record CreateDroneDto(string Name, double Latitude, double Longitude);
    public record UpdateDroneDto(string Name, double Latitude, double Longitude);

}
