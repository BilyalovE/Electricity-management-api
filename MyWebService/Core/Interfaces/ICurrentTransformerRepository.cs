namespace MyWebService.Core.Interfaces
{
    public interface ICurrentTransformerRepository
    {
        Task<IEnumerable<CurrentTransformer>> GetAll();
        Task<CurrentTransformer> GetById(int id);
        Task Add(CurrentTransformer entity);
        Task Update(CurrentTransformer entity);
        Task Delete(int id);
        
        // Метод для получения всех трансформаторов тока с просроченным сроком поверки на объекте потребления
        Task<IEnumerable<CurrentTransformer>> GetTransformersWithExpiredVerification(int objectId);
    }
}