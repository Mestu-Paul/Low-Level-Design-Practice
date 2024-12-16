using Microsoft.AspNetCore.Mvc;
using ParkingLot.Commands;
using ParkingLot.Interfaces;
using ParkingLot.Models;

namespace ParkingLot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParkingLotController : ControllerBase
    {
        [HttpPost("CreateParkingLot")]
        public async Task<IActionResult> CreateParkingLot([FromBody]ParkingLotDto parkingLotDto, [FromServices] ICommandHandler<CreateParkingLotCommand,string>commandHandler)
        {
            var createParkingLotCommand = new CreateParkingLotCommand(parkingLotDto);
            var response = await commandHandler.HandleAsync(createParkingLotCommand);
            return Ok(response);
        }
    }
}
