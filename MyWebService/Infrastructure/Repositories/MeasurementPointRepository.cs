using Microsoft.EntityFrameworkCore;

namespace MyWebService.Infrastructure.Repositories
{
    public class MeasurementPointRepository : IMeasurementPointRepository
    {
        private readonly ElectricityDbContext _dbContext;

        public MeasurementPointRepository(ElectricityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<MeasurementPoint>> GetAll()
        {
            return await _dbContext.MeasurementPoints.ToListAsync();
        }
        
        public async Task<MeasurementPoint> GetById(int id)
        {
            return await _dbContext.MeasurementPoints.FirstOrDefaultAsync(o => o.Id == id);
        }
        
        public async Task Add(MeasurementPoint entity)
        {
            await _dbContext.MeasurementPoints.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task Update(MeasurementPoint entity)
        {
            _dbContext.MeasurementPoints.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task Delete(int id)
        {
            var entity = await _dbContext.MeasurementPoints.FindAsync(id);
            if (entity != null)
            {
                _dbContext.MeasurementPoints.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
        
        // Методы для поиска счетчика и трансформаторов
        public async Task<ElectricityMeter?> GetElectricityMeterById(int id)
        {
            return await _dbContext.ElectricityMeters.FindAsync(id);
        }

        public async Task<CurrentTransformer?> GetCurrentTransformerById(int id)
        {
            return await _dbContext.CurrentTransformers.FindAsync(id);
        }

        public async Task<VoltageTransformer?> GetVoltageTransformerById(int id)
        {
            return await _dbContext.VoltageTransformers.FindAsync(id);
        }
    }
}