using Microsoft.AspNetCore.Mvc;
using MyWebService.Core.Services;

namespace MyWebService.WebApi.Controllers
{
    [ApiController]
    [Route("api/electricity-meters")]
    public partial class ElectricityMeterController : ControllerBase
    {
        private readonly ElectricityMeterService _service;

        public ElectricityMeterController(ElectricityMeterService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var electricityMeters = await _service.GetAll();
            return Ok(electricityMeters); 
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var electricityMeter = await _service.GetById(id);
            if (electricityMeter == null)
            {
                return NotFound();
            }
            return Ok(electricityMeter);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(ElectricityMeterCreateOrUpdateDto electricityMeterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var electricityMeter = new ElectricityMeter
            {
                Number = electricityMeterDto.Number,
                Type = electricityMeterDto.Type,
                VerificationDate = electricityMeterDto.VerificationDate,
                MeasurementPointId = electricityMeterDto.MeasurementPointId,
            };

            await _service.Add(electricityMeter);
            return CreatedAtAction(nameof(GetById), new { id = electricityMeter.Id }, electricityMeter);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ElectricityMeterCreateOrUpdateDto electricityMeterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            var existingElectricityMeter = await _service.GetById(id);
            if (existingElectricityMeter == null)
            {
                return NotFound();
            }
        
            existingElectricityMeter.Number = electricityMeterDto.Number;
            existingElectricityMeter.Type = electricityMeterDto.Type;
            existingElectricityMeter.VerificationDate = electricityMeterDto.VerificationDate;
            existingElectricityMeter.MeasurementPointId = electricityMeterDto.MeasurementPointId;
        
            await _service.Update(existingElectricityMeter);
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