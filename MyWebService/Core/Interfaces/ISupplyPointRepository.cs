namespace MyWebService.Core.Interfaces
{
    public interface ISupplyPointRepository
    {
        Task<IEnumerable<SupplyPoint>> GetAll();
        Task<SupplyPoint> GetById(int id);
        Task Add(SupplyPoint entity);
        Task Update(SupplyPoint entity);
        Task Delete(int id);
    }
}