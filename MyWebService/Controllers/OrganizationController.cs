using Microsoft.AspNetCore.Mvc;
using MyWebService.Core.Services;

namespace MyWebService.WebApi.Controllers
{
    [ApiController]
    [Route("api/organizations")]
    public class OrganizationController : ControllerBase
    {
        private readonly OrganizationService _service;

        public OrganizationController(OrganizationService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var organizations = await _service.GetAll();
            return Ok(organizations); 
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
        public async Task<IActionResult> Create(OrganizationCreateOrUpdateDto organizationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var organization = new Organization
            {
                Name = organizationDto.Name,
                Address = organizationDto.Address
            };

            await _service.Add(organization);
            return CreatedAtAction(nameof(GetById), new { id = organization.Id }, organization);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OrganizationCreateOrUpdateDto organizationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            var existingOrganization = await _service.GetById(id);
            if (existingOrganization == null)
            {
                return NotFound();
            }
        
            existingOrganization.Name = organizationDto.Name;
            existingOrganization.Address = organizationDto.Address;
        
            await _service.Update(existingOrganization);
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