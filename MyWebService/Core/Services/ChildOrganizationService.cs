namespace MyWebService.Core.Services
{
    public class ChildOrganizationService 
    {
        private readonly IChildOrganizationRepository _repository;

        public ChildOrganizationService(IChildOrganizationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ChildOrganization>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<ChildOrganization> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Add(ChildOrganization entity)
        {
            await _repository.Add(entity);
        }

        public async Task Update(ChildOrganization entity)
        {
            await _repository.Update(entity);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
    }
}