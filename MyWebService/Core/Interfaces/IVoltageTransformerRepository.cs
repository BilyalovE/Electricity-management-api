namespace MyWebService.Core.Interfaces
{
    public interface IVoltageTransformerRepository
    {
        Task<IEnumerable<VoltageTransformer>> GetAll();
        Task<VoltageTransformer> GetById(int id);
        Task Add(VoltageTransformer entity);
        Task Update(VoltageTransformer entity);
        Task Delete(int id);
        
        // Метод для получения всех трансформаторов напряжения с просроченным сроком поверки на объекте потребления
        Task<IEnumerable<VoltageTransformer>> GetTransformersWithExpiredVerification(int objectId);
    }
}