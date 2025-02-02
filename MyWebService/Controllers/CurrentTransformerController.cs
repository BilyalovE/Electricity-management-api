using Microsoft.AspNetCore.Mvc;
using MyWebService.Core.Services;

namespace MyWebService.WebApi.Controllers
{
    [ApiController]
    [Route("api/current-transformers")]
    public partial class CurrentTransformerController : ControllerBase
    {
        private readonly CurrentTransformerService _service;

        public CurrentTransformerController(CurrentTransformerService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var currentTransformers = await _service.GetAll();
            return Ok(currentTransformers); 
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var currentTransformer = await _service.GetById(id);
            if (currentTransformer == null)
            {
                return NotFound();
            }
            return Ok(currentTransformer);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CurrentTransformerCreateOrUpdateDto currentTransformerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentTransformer = new CurrentTransformer
            {
                Number = currentTransformerDto.Number,
                Type = currentTransformerDto.Type,
                VerificationDate = currentTransformerDto.VerificationDate,
                TransformationRatio = currentTransformerDto.TransformationRatio,
                MeasurementPointId = currentTransformerDto.MeasurementPointId,
            };

            await _service.Add(currentTransformer);
            return CreatedAtAction(nameof(GetById), new { id = currentTransformer.Id }, currentTransformer);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CurrentTransformerCreateOrUpdateDto currentTransformerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            var existingCurrentTransformer = await _service.GetById(id);
            if (existingCurrentTransformer == null)
            {
                return NotFound();
            }
        
            existingCurrentTransformer.Number = currentTransformerDto.Number;
            existingCurrentTransformer.Type = currentTransformerDto.Type;
            existingCurrentTransformer.VerificationDate = currentTransformerDto.VerificationDate;
            existingCurrentTransformer.TransformationRatio = currentTransformerDto.TransformationRatio;
            existingCurrentTransformer.MeasurementPointId = currentTransformerDto.MeasurementPointId;
        
            await _service.Update(existingCurrentTransformer);
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