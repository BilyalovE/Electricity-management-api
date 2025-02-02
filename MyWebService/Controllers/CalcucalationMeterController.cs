using Microsoft.AspNetCore.Mvc;
using MyWebService.Core.Services;

namespace MyWebService.WebApi.Controllers
{
    [ApiController]
    [Route("api/calculation-meters")]
    public partial class CalculationMeterController : ControllerBase
    {
        private readonly CalculationMeterService _service;

        public CalculationMeterController(CalculationMeterService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var calculationMeters = await _service.GetAll();
            return Ok(calculationMeters);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var calculationMeter = await _service.GetById(id);
            if (calculationMeter == null)
            {
                return NotFound();
            }

            return Ok(calculationMeter);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CalculationMeterCreateOrUpdateDto calculationMeterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var calculationMeter = new CalculationMeter
            {
                SupplyPointId = calculationMeterDto.SupplyPointId,
                MeasurementPointId = calculationMeterDto.MeasurementPointId,
                StartTime = calculationMeterDto.StartTime,
                EndTime = calculationMeterDto.EndTime
            };
            await _service.Add(calculationMeter);
            return CreatedAtAction(nameof(GetById), new { id = calculationMeter.Id }, calculationMeter);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CalculationMeterCreateOrUpdateDto calculationMeterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            var existingCalculationMeter = await _service.GetById(id);
            if (existingCalculationMeter == null)
            {
                return NotFound();
            }

            existingCalculationMeter.SupplyPointId = calculationMeterDto.SupplyPointId;
            existingCalculationMeter.MeasurementPointId = calculationMeterDto.MeasurementPointId;
            existingCalculationMeter.StartTime = calculationMeterDto.StartTime;
            existingCalculationMeter.EndTime = calculationMeterDto.EndTime;
            
            await _service.Update(existingCalculationMeter);
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