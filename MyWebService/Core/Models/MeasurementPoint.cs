using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace MyWebService.Core.Models
{
    public class MeasurementPoint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ConsumptionObjectId { get; set; }
        [JsonIgnore] public ConsumptionObject? ConsumptionObjectEntity { get; set; }
        [JsonIgnore] public ElectricityMeter? ElectricityMeterEntity { get; set; }
        [JsonIgnore] public CurrentTransformer? CurrentTransformerEntity { get; set; } 
        [JsonIgnore] public VoltageTransformer? VoltageTransformerEntity { get; set; }
        [JsonIgnore] public List<CalculationMeter> CalculationMeters { get; set; } = new();
    }
    
    public class MeasurementPointCreateOrUpdateDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "ConsumptionObjectId is required")]
        public int ConsumptionObjectId { get; set; }
    }
    
    public class MeasurementPointAddDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "ElectricityMeterId is required")]
        public int ElectricityMeterId { get; set; }

        [Required(ErrorMessage = "CurrentTransformerId is required")]
        public int CurrentTransformerId { get; set; }

        [Required(ErrorMessage = "VoltageTransformerId is required")]
        public int VoltageTransformerId { get; set; }
    }

}