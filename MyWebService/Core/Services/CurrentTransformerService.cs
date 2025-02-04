namespace MyWebService.Core.Services
{
    public class CurrentTransformerService 
    {
        private readonly ICurrentTransformerRepository _repository;

        public CurrentTransformerService(ICurrentTransformerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CurrentTransformer>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<CurrentTransformer> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Add(CurrentTransformer entity)
        {
            await _repository.Add(entity);
        }

        public async Task Update(CurrentTransformer entity)
        {
            await _repository.Update(entity);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
        
        public async Task<IEnumerable<CurrentTransformer>> GetTransformersWithExpiredVerification(int objectId)
        {
            return await _repository.GetTransformersWithExpiredVerification(objectId);
        }
    }
}