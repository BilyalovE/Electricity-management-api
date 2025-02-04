namespace MyWebService.Core.Services
{
    public class VoltageTransformerService 
    {
        private readonly IVoltageTransformerRepository _repository;

        public VoltageTransformerService(IVoltageTransformerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<VoltageTransformer>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<VoltageTransformer> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Add(VoltageTransformer entity)
        {
            await _repository.Add(entity);
        }

        public async Task Update(VoltageTransformer entity)
        {
            await _repository.Update(entity);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
        
        public async Task<IEnumerable<VoltageTransformer>> GetTransformersWithExpiredVerification(int objectId)
        {
            return await _repository.GetTransformersWithExpiredVerification(objectId);
        }
    }
}