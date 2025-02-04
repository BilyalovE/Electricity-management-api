namespace MyWebService.Core.Interfaces
{
    public interface IElectricityMeterRepository
    {
        Task<IEnumerable<ElectricityMeter>> GetAll();
        Task<ElectricityMeter> GetById(int id);
        Task Add(ElectricityMeter entity);
        Task Update(ElectricityMeter entity);
        Task Delete(int id);
        
        // Метод для получения всех счетчиков с просроченным сроком поверки на объекте потребления
        Task<IEnumerable<ElectricityMeter>> GetMetersWithExpiredVerification(int objectId);
    }
}