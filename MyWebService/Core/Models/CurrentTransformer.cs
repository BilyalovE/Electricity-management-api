using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace MyWebService.Core.Models
{
    public class CurrentTransformer
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public DateTime VerificationDate { get; set; }
        public double TransformationRatio { get; set; }
        public int? MeasurementPointId { get; set; }
        [JsonIgnore] public MeasurementPoint? MeasurementPointEntity { get; set; }
    }
    
    public class CurrentTransformerCreateOrUpdateDto
    {
        [Required(ErrorMessage = "Number is required")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }
        
        [Required(ErrorMessage = "DateTime is required")]
        public DateTime VerificationDate { get; set; }
        
        [Required(ErrorMessage = "TransformationRatio is required")]
        public double TransformationRatio { get; set; }
        
        [Required(ErrorMessage = "MeasurementPointId is required")]
        public int MeasurementPointId { get; set; }
    }
}