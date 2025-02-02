namespace MyWebService.Core.Services
{
    public class CalculationMeterService 
    {
        private readonly ICalculationMeterRepository _repository;

        public CalculationMeterService(ICalculationMeterRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CalculationMeter>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<CalculationMeter> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Add(CalculationMeter entity)
        {
            await _repository.Add(entity);
        }

        public async Task Update(CalculationMeter entity)
        {
            await _repository.Update(entity);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
        
        public async Task<IEnumerable<CalculationMeter>> GetByYear(int year)
        {
            return await _repository.GetByYear(year);
        }

    }
}