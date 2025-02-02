namespace MyWebService.Core.Services
{
    public class OrganizationService 
    {
        private readonly IOrganizationRepository _repository;

        public OrganizationService(IOrganizationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Organization>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Organization> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task Add(Organization entity)
        {
            await _repository.Add(entity);
        }

        public async Task Update(Organization entity)
        {
            await _repository.Update(entity);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
    }
}