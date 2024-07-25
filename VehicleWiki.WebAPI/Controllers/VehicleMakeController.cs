using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleWiki.Common.NotFoundException;
using VehicleWiki.Dtos;
using VehicleWiki.Model.Common;
using VehicleWiki.Service.Common;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleMakeController : ControllerBase
    {
        private readonly IVehicleMakeService _vehicleMakeService;

        public VehicleMakeController(IVehicleMakeService vehicleMakeService)
        {
            _vehicleMakeService = vehicleMakeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleMakeDto>>> GetAllVehicleMakesAsync()
        {
            try
            {
                var results = await _vehicleMakeService.GetAllVehicleMakesAsync();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleMakeDto>> GetVehicleMakeByIdAsync(Guid id)
        {
            try
            {
                var result = await _vehicleMakeService.GetVehicleMakeByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException)
                {
                    return NotFound(ex.Message);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<VehicleMakeDto>>> GetPagedVehicleMakes([FromQuery] QueryParameters queryParams)
        {
            try
            {
                var pagedResult = await _vehicleMakeService.GetPagedVehicleMakesAsync(queryParams);
                return Ok(pagedResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<VehicleMakeDto>> CreateVehicleMakeAsync([FromBody] CreateVehicleMakeDto vehicleMakeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _vehicleMakeService.CreateVehicleMakeAsync(vehicleMakeDto);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VehicleMakeDto>> UpdateVehicleMakeAsync([FromBody] UpdateVehicleMakeDto vehicleMakeDto, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _vehicleMakeService.UpdateVehicleMakeAsync(vehicleMakeDto, id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException)
                {
                    return NotFound(ex.Message);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicleMakeAsync(Guid id)
        {
            try
            {
                await _vehicleMakeService.DeleteVehicleMakeAsync(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException)
                {
                    return NotFound(ex.Message);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
