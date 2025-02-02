namespace MyWebService.Core.Services
{
    public class ConsumptionObjectService 
    {
        private readonly IConsumptionObjectRepository _repository;

        public ConsumptionObjectService(IConsumptionObjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ConsumptionObject>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<ConsumptionObject> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Add(ConsumptionObject entity)
        {
            await _repository.Add(entity);
        }

        public async Task Update(ConsumptionObject entity)
        {
            await _repository.Update(entity);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
    }
}