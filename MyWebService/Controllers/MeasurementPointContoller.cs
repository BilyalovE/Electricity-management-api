using Microsoft.AspNetCore.Mvc;
using MyWebService.Core.Services;

namespace MyWebService.WebApi.Controllers
{
    [ApiController]
    [Route("api/measurement-points")]
    public partial class MeasurementPointController : ControllerBase
    {
        private readonly MeasurementPointService _service;

        public MeasurementPointController(MeasurementPointService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var measurementPoints = await _service.GetAll();
            return Ok(measurementPoints);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var measurementPoint = await _service.GetById(id);
            if (measurementPoint == null)
            {
                return NotFound();
            }
            return Ok(measurementPoint);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(MeasurementPointCreateOrUpdateDto measurementPointDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var measurementPoint = new MeasurementPoint
            {
                Name = measurementPointDto.Name,
                ConsumptionObjectId = measurementPointDto.ConsumptionObjectId
            };

            await _service.Add(measurementPoint);
            return CreatedAtAction(nameof(GetById), new { id = measurementPoint.Id }, measurementPoint);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MeasurementPointCreateOrUpdateDto measurementPointDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            var existingMeasurementPoint = await _service.GetById(id);
            if (existingMeasurementPoint == null)
            {
                return NotFound();
            }
        
            existingMeasurementPoint.Name = measurementPointDto.Name;
            existingMeasurementPoint.ConsumptionObjectId = measurementPointDto.ConsumptionObjectId;
        
            await _service.Update(existingMeasurementPoint);
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