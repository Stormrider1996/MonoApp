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
    public class VehicleModelController : ControllerBase
    {
        private readonly IVehicleModelService _vehicleModelService;

        public VehicleModelController(IVehicleModelService vehicleModelService)
        {
            _vehicleModelService = vehicleModelService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleModelDto>>> GetAllVehicleModelsAsync()
        {
            try
            {
                var results = await _vehicleModelService.GetAllVehicleModelsAsync();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleModelDto>> GetVehicleModelByIdAsync(Guid id)
        {
            try
            {
                var result = await _vehicleModelService.GetVehicleModelByIdAsync(id);
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

        [HttpGet("make/{id}")]
        public async Task<ActionResult<IEnumerable<VehicleModelDto>>> GetVehicleModelsByMakeIdAsync(Guid id)
        {
            try
            {
                var results = await _vehicleModelService.GetVehicleModelsByMakeIdAsync(id);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<VehicleMakeDto>>> GetPagedVehicleMakes([FromQuery] QueryParameters queryParams)
        {
            try
            {
                var pagedResult = await _vehicleModelService.GetPagedVehicleModelsAsync(queryParams);
                return Ok(pagedResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<VehicleModelDto>> CreateVehicleModelAsync([FromBody] CreateVehicleModelDto vehicleModelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _vehicleModelService.CreateVehicleModelAsync(vehicleModelDto);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VehicleModelDto>> UpdateVehicleModelAsync([FromBody] UpdateVehicleModelDto vehicleModelDto, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _vehicleModelService.UpdateVehicleModelAsync(vehicleModelDto, id);
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
        public async Task<IActionResult> DeleteVehicleModelAsync(Guid id)
        {
            try
            {
                await _vehicleModelService.DeleteVehicleModelAsync(id);
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
