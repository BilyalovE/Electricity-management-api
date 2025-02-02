using Microsoft.AspNetCore.Mvc;

namespace MyWebService.WebApi.Controllers
{
    public partial class CalculationMeterController
    {
        // Выбрать все расчетные приборы учета в 2018 году 
        [HttpGet("year/{year}")]
        public async Task<IActionResult> GetByYear(int year)
        {
            var calculationMeters = await _service.GetByYear(year);
            return Ok(calculationMeters);
        }
    }

    public partial class CurrentTransformerController
    {
        // Получение всех трансформаторов тока с просроченной поверкой по указанному объекту потребления
        [HttpGet("expired/{objectId}")]
        public async Task<IActionResult> GetTransformersWithExpiredVerification(int objectId)
        {
            var expiredTransformers = await _service.GetTransformersWithExpiredVerification(objectId);
            return Ok(expiredTransformers);
        }
    }

    public partial class MeasurementPointController
    {
        // Добавить новую точку измерения с указанием счетчика, трансформатора тока и трансформатора напряжения
        [HttpPost("add-with-devices")]
        public async Task<IActionResult> Create(MeasurementPointAddDto measurementPointDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var resService = await _service.AddWithDevices(measurementPointDto);
            if(resService == null)
                return NotFound();
            
            var measurementPoint = await _service.GetById(resService.Id);

            return CreatedAtAction(nameof(GetById), new { id = measurementPoint.Id }, measurementPoint);
        }
    }

    public partial class ElectricityMeterController
    {
        // Получение всех счетчиков с просроченной поверкой
        [HttpGet("expired/{objectId}")]
        public async Task<IActionResult> GetMetersWithExpiredVerification(int objectId)
        {
            var expiredMeters = await _service.GetMetersWithExpiredVerification(objectId);
            return Ok(expiredMeters);
        }
    }

    public partial class VoltageTransformerController
    {
        // Получение всех трансформаторов напряжения с просроченной поверкой по указанному объекту потребления
        [HttpGet("expired/{objectId}")]
        public async Task<IActionResult> GetTransformersWithExpiredVerification(int objectId)
        {
            var expiredTransformers = await _service.GetTransformersWithExpiredVerification(objectId);
            return Ok(expiredTransformers);
        }
    }
}