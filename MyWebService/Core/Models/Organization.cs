using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace MyWebService.Core.Models
{
    public class Organization
    {
        public int Id { get; set; }
     
        public string Name { get; set; }
        public string Address { get; set; }
        
        [JsonIgnore] public List<ChildOrganization> ChildOrganizations { get; set; } = new();
    }
    
    public class OrganizationCreateOrUpdateDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
    }
}
