using Microsoft.AspNetCore.Mvc;
using MyWebService.Core.Services;

namespace MyWebService.WebApi.Controllers
{
    [ApiController]
    [Route("api/child-organizations")]
    public class ChildOrganizationController : ControllerBase
    {
        private readonly ChildOrganizationService _service;

        public ChildOrganizationController(ChildOrganizationService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var childOrganizations = await _service.GetAll();
            return Ok(childOrganizations); 
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var organization = await _service.GetById(id);
            if (organization == null)
            {
                return NotFound();
            }
            return Ok(organization); 
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(ChildOrganizationCreateOrUpdateDto childOrganizationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var childOrganization = new ChildOrganization
            {
                Name = childOrganizationDto.Name,
                Address = childOrganizationDto.Address,
                OrganizationId = childOrganizationDto.OrganizationId,
            };

            await _service.Add(childOrganization);
            return CreatedAtAction(nameof(GetById), new { id = childOrganization.Id }, childOrganization);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ChildOrganizationCreateOrUpdateDto childOrganizationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            var existingChildOrganization = await _service.GetById(id);
            if (existingChildOrganization == null)
            {
                return NotFound();
            }
        
            existingChildOrganization.Name = childOrganizationDto.Name;
            existingChildOrganization.Address = childOrganizationDto.Address;
            existingChildOrganization.OrganizationId = childOrganizationDto.OrganizationId;
        
            await _service.Update(existingChildOrganization);
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