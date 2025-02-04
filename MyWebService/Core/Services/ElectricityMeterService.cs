namespace MyWebService.Core.Services
{
    public class ElectricityMeterService 
    {
        private readonly IElectricityMeterRepository _repository;

        public ElectricityMeterService(IElectricityMeterRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ElectricityMeter>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<ElectricityMeter> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Add(ElectricityMeter entity)
        {
            await _repository.Add(entity);
        }

        public async Task Update(ElectricityMeter entity)
        {
            await _repository.Update(entity);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
        public async Task<IEnumerable<ElectricityMeter>> GetMetersWithExpiredVerification(int objectId)
        {
            return await _repository.GetMetersWithExpiredVerification(objectId);
        }

    }
}