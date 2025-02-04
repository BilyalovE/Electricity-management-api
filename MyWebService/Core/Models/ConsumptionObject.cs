using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace MyWebService.Core.Models
{
    public class ConsumptionObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        
        public int ChildOrganizationId { get; set; }
        [JsonIgnore] public ChildOrganization? ChildOrganizationEntity { get; set; }

        [JsonIgnore] public List<MeasurementPoint> MeasurementPoints { get; set; } = new();

        [JsonIgnore] public List<SupplyPoint> SupplyPoints { get; set; } = new();
    }
    
    public class ConsumptionObjectCreateOrUpdateDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        
        [Required(ErrorMessage = "ChildOrganizationId is required")]
        public int ChildOrganizationId { get; set; }
    }
}