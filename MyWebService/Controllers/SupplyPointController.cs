using Microsoft.AspNetCore.Mvc;
using MyWebService.Core.Services;

namespace MyWebService.WebApi.Controllers
{
    [ApiController]
    [Route("api/supply-points")]
    public class SupplyPointController : ControllerBase
    {
        private readonly SupplyPointService _service;

        public SupplyPointController(SupplyPointService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var supplyPoints = await _service.GetAll();
            return Ok(supplyPoints);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var supplyPoint = await _service.GetById(id);
            if (supplyPoint == null)
            {
                return NotFound();
            }
            return Ok(supplyPoint);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(SupplyPointCreateOrUpdateDto supplyPointDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var supplyPoint = new SupplyPoint
            {
                Name = supplyPointDto.Name,
                MaxPower = supplyPointDto.MaxPower,
                ConsumptionObjectId = supplyPointDto.ConsumptionObjectId,
            };

            await _service.Add(supplyPoint);
            return CreatedAtAction(nameof(GetById), new { id = supplyPoint.Id }, supplyPoint);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SupplyPointCreateOrUpdateDto supplyPointDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            var existingSupplyPoint = await _service.GetById(id);
            if (existingSupplyPoint == null)
            {
                return NotFound();
            }
        
            existingSupplyPoint.Name = supplyPointDto.Name;
            existingSupplyPoint.MaxPower = supplyPointDto.MaxPower;
            existingSupplyPoint.ConsumptionObjectId = supplyPointDto.ConsumptionObjectId;
        
            await _service.Update(existingSupplyPoint);
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}