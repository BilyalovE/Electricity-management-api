namespace MyWebService.Core.Interfaces
{
    public interface IMeasurementPointRepository
    {
        Task<IEnumerable<MeasurementPoint>> GetAll();
        Task<MeasurementPoint> GetById(int id);
        Task Add(MeasurementPoint entity);
        Task Update(MeasurementPoint entity);
        Task Delete(int id);
        
        // Новые методы для поиска счетчика и трансформаторов
        Task<ElectricityMeter?> GetElectricityMeterById(int id);
        Task<CurrentTransformer?> GetCurrentTransformerById(int id);
        Task<VoltageTransformer?> GetVoltageTransformerById(int id);
    }
}



