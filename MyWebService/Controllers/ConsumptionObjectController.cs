using Microsoft.AspNetCore.Mvc;
using MyWebService.Core.Services;

namespace MyWebService.WebApi.Controllers
{
    [ApiController]
    [Route("api/consumption-objects")]
    public class ConsumptionObjectController : ControllerBase
    {
        private readonly ConsumptionObjectService _service;

        public ConsumptionObjectController(ConsumptionObjectService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var consumptionObjects = await _service.GetAll();
            return Ok(consumptionObjects);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var consumptionObject = await _service.GetById(id);
            if (consumptionObject == null)
            {
                return NotFound();
            }
            return Ok(consumptionObject); 
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(ConsumptionObjectCreateOrUpdateDto consumptionObjectDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var consumptionObject = new ConsumptionObject
            {
                Name = consumptionObjectDto.Name,
                Address = consumptionObjectDto.Address,
                ChildOrganizationId = consumptionObjectDto.ChildOrganizationId,
            };

            await _service.Add(consumptionObject);
            return CreatedAtAction(nameof(GetById), new { id = consumptionObject.Id }, consumptionObject);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ConsumptionObjectCreateOrUpdateDto consumptionObjectDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            var existingConsumptionObject = await _service.GetById(id);
            if (existingConsumptionObject == null)
            {
                return NotFound();
            }
        
            existingConsumptionObject.Name = consumptionObjectDto.Name;
            existingConsumptionObject.Address = consumptionObjectDto.Address;
            existingConsumptionObject.ChildOrganizationId = consumptionObjectDto.ChildOrganizationId;
        
            await _service.Update(existingConsumptionObject);
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