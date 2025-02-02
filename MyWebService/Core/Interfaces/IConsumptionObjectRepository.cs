namespace MyWebService.Core.Interfaces
{
    public interface IConsumptionObjectRepository
    {
        Task<IEnumerable<ConsumptionObject>> GetAll();
        Task<ConsumptionObject> GetById(int id);
        Task Add(ConsumptionObject entity);
        Task Update(ConsumptionObject entity);
        Task Delete(int id);
    }
}