namespace MyWebService.Core.Services
{
    public class MeasurementPointService 
    {
        private readonly IMeasurementPointRepository _repository;

        public MeasurementPointService(IMeasurementPointRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MeasurementPoint>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<MeasurementPoint> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Add(MeasurementPoint entity)
        {
            await _repository.Add(entity);
        }

        public async Task Update(MeasurementPoint entity)
        {
            await _repository.Update(entity);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
        
        public async Task<MeasurementPoint?> AddWithDevices(MeasurementPointAddDto dto)
        {
            var electricityMeter = await _repository.GetElectricityMeterById(dto.ElectricityMeterId);
            var currentTransformer = await _repository.GetCurrentTransformerById(dto.CurrentTransformerId);
            var voltageTransformer = await _repository.GetVoltageTransformerById(dto.VoltageTransformerId);

            if (electricityMeter == null || currentTransformer == null || voltageTransformer == null)
            {
                return null; // Вернем null, если какие-то устройства не найдены
            }

            var measurementPoint = new MeasurementPoint
            {
                Name = dto.Name,
                ElectricityMeterEntity = electricityMeter,
                CurrentTransformerEntity = currentTransformer,
                VoltageTransformerEntity = voltageTransformer
            };

            await _repository.Add(measurementPoint);
            return measurementPoint;
        }
    }
}