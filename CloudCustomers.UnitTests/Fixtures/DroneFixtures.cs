using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cloudcustomers.API.Dtos;

namespace CloudCustomers.UnitTests.Fixtures
{
    public static class DroneFixture
    {
        public static List<DroneDto> GetTestDrones()
        {
            return new() {

                new DroneDto(Guid.NewGuid(), "Tromsø",69.651648,18.9558186,DateTimeOffset.UtcNow),
                new DroneDto(Guid.NewGuid(), "Bergen",60.3943055,5.3259192,DateTimeOffset.UtcNow),
                new DroneDto(Guid.NewGuid(), "Oslo",59.913330,10.7389701,DateTimeOffset.UtcNow)
            };
        }
    }
}