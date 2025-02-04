namespace MyWebService.Core.Interfaces
{
    public interface IChildOrganizationRepository
    {
        Task<IEnumerable<ChildOrganization>> GetAll();
        Task<ChildOrganization> GetById(int id);
        Task Add(ChildOrganization entity);
        Task Update(ChildOrganization entity);
        Task Delete(int id);
    }
}