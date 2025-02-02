namespace MyWebService.Core.Interfaces
{
    public interface ICalculationMeterRepository
    {
        Task<IEnumerable<CalculationMeter>> GetAll();
        Task<CalculationMeter> GetById(int id);
        Task Add(CalculationMeter entity);
        Task Update(CalculationMeter entity);
        Task Delete(int id);
        
        // Метод для получения приборов учета по году
        Task<IEnumerable<CalculationMeter>> GetByYear(int year);
    }
}