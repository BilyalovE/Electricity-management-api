using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace MyWebService.Core.Models
{
    public class SupplyPoint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double MaxPower { get; set; }
        public int ConsumptionObjectId { get; set; }
        [JsonIgnore] public ConsumptionObject? ConsumptionObjectEntity { get; set; }
        [JsonIgnore] public List<CalculationMeter> CalculationMeters { get; set; } = new();
    }
    
    public class SupplyPointCreateOrUpdateDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "MaxPower is required")]
        public double MaxPower { get; set; }
        
        [Required(ErrorMessage = "ConsumptionObjectId is required")]
        public int ConsumptionObjectId { get; set; }
    }
}