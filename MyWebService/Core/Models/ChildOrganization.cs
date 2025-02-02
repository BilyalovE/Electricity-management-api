using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace MyWebService.Core.Models
{
    public class ChildOrganization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int OrganizationId { get; set; }
        
        [JsonIgnore] public Organization? OrganizationEntity { get; set; }
        [JsonIgnore] public List<ConsumptionObject> ConsumptionObjects { get; set; } = new();
    }
    
    public class ChildOrganizationCreateOrUpdateDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        
        [Required(ErrorMessage = "OrganizationId is required")]
        public int OrganizationId { get; set; }
    }
}
