using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace MyWebService.Core.Models
{
    public class CalculationMeter
    {
        public int Id { get; set; }
        public int MeasurementPointId { get; set; }
        [JsonIgnore] public MeasurementPoint? MeasurementPointEntity { get; set; }
        public int SupplyPointId { get; set; }
        
        [JsonIgnore] public SupplyPoint? SupplyPointEntity { get; set; }
        
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
    
    public class CalculationMeterCreateOrUpdateDto
    {
        [Required(ErrorMessage = "MeasurementPointId is required")]
        public int MeasurementPointId { get; set; }

        [Required(ErrorMessage = "SupplyPointId is required")]
        public int SupplyPointId { get; set; }
        
        [Required(ErrorMessage = "StartTime is required")]
        public DateTime StartTime { get; set; }
        
        [Required(ErrorMessage = "EndTime is required")]
        public DateTime? EndTime { get; set; }
    }
}
