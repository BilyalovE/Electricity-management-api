using Microsoft.AspNetCore.Mvc;
using MyWebService.Core.Services;

namespace MyWebService.WebApi.Controllers
{
    [ApiController]
    [Route("api/voltage-transformers")]
    public partial class VoltageTransformerController : ControllerBase
    {
        private readonly VoltageTransformerService _service;

        public VoltageTransformerController(VoltageTransformerService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var voltageTransformers = await _service.GetAll();
            return Ok(voltageTransformers); 
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var voltageTransformer = await _service.GetById(id);
            if (voltageTransformer == null)
            {
                return NotFound();
            }
            return Ok(voltageTransformer);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(VoltageTransformerCreateOrUpdateDto voltageTransformerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var voltageTransformer = new VoltageTransformer
            {
                Number = voltageTransformerDto.Number,
                Type = voltageTransformerDto.Type,
                VerificationDate = voltageTransformerDto.VerificationDate,
                TransformationRatio = voltageTransformerDto.TransformationRatio,
                MeasurementPointId = voltageTransformerDto.MeasurementPointId,
            };

            await _service.Add(voltageTransformer);
            return CreatedAtAction(nameof(GetById), new { id = voltageTransformer.Id }, voltageTransformer);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, VoltageTransformerCreateOrUpdateDto voltageTransformerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            var existingVoltageTransformer = await _service.GetById(id);
            if (existingVoltageTransformer == null)
            {
                return NotFound();
            }
        
            existingVoltageTransformer.Number = voltageTransformerDto.Number;
            existingVoltageTransformer.Type = voltageTransformerDto.Type;
            existingVoltageTransformer.VerificationDate = voltageTransformerDto.VerificationDate;
            existingVoltageTransformer.TransformationRatio = voltageTransformerDto.TransformationRatio;
            existingVoltageTransformer.MeasurementPointId = voltageTransformerDto.MeasurementPointId;
        
            await _service.Update(existingVoltageTransformer);
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