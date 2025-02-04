namespace MyWebService.Core.Services
{
    public class SupplyPointService 
    {
        private readonly ISupplyPointRepository _repository;

        public SupplyPointService(ISupplyPointRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SupplyPoint>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<SupplyPoint> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Add(SupplyPoint entity)
        {
            await _repository.Add(entity);
        }

        public async Task Update(SupplyPoint entity)
        {
            await _repository.Update(entity);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
    }
}