using Cloudcustomers.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Cloudcustomers.API.Controllers
{
    [ApiController]
    [Route("drones")]
    public class DroneController : ControllerBase
    {
        private readonly IDronesService _dronesService;
        public DroneController(IDronesService dronesService)
        {

            _dronesService = dronesService;
        }
        private static readonly List<DroneDto> drones = new()
        {
            new DroneDto(Guid.NewGuid(), "Tromsø",69.651648,18.9558186,DateTimeOffset.UtcNow),
            new DroneDto(Guid.NewGuid(), "Bergen",60.3943055,5.3259192,DateTimeOffset.UtcNow),
            new DroneDto(Guid.NewGuid(), "Oslo",59.913330,10.7389701,DateTimeOffset.UtcNow)
        };
        ///drones
        [HttpGet]
        public async Task<IActionResult> Get()
        //public Task<IActionResult> Get()
        {
            //Ta vekk kommentartegn på linjen under og en vil hente fra ekstern site.
            //var drones = await _dronesService.getAllDrones();
            

            if (drones.Any())
            {
                return Ok(drones);
            }
            return NotFound();

        }
        
        [HttpGet("{id}")]
        public ActionResult<DroneDto> GetById(Guid id)
        {
            var drone = drones.Where(drone => drone.Id == id).SingleOrDefault();
            if (drone == null){
                return NotFound();
            }
            return drone;
        }
        [HttpPost]
        public ActionResult<DroneDto> Post(CreateDroneDto createDroneDto)
        {
            var drone =new DroneDto(Guid.NewGuid(), createDroneDto.Name, createDroneDto.Latitude, createDroneDto.Longitude, DateTimeOffset.UtcNow);
            drones.Add(drone);
            return CreatedAtAction(nameof(GetById), new { id = drone.Id }, drone);
        }
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UpdateDroneDto updateDroneDto)
        {
            var existingDrone=drones.Where(drones => drones.Id == id).SingleOrDefault();
            if (existingDrone == null) {
                return NotFound();
            }
            var updatedDrone = existingDrone with
            {
                Name = updateDroneDto.Name,
                Latitude = updateDroneDto.Latitude,
                Longitude = updateDroneDto.Longitude
            };
            var index =drones.FindIndex(existingDrone => existingDrone.Id == id); 
            drones[index]=updatedDrone;
            return NoContent();
        }
        //Delete/id
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var index = drones.FindIndex(existingDrone => existingDrone.Id == id);
            if (index <0)
            {
                return NotFound();
            }
            drones.RemoveAt(index);
            return NoContent();
        }
    }
}
