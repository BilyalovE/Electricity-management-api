namespace MyWebService.Core.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<Organization>> GetAll();
        Task<Organization> GetById(int id);
        Task Add(Organization entity);
        Task Update(Organization entity);
        Task Delete(int id);
    }
}