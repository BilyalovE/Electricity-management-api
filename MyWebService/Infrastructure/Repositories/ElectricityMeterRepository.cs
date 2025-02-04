using Microsoft.EntityFrameworkCore;

namespace MyWebService.Infrastructure.Repositories
{
    public class ElectricityMeterRepository : IElectricityMeterRepository
    {
        private readonly ElectricityDbContext _dbContext;

        public ElectricityMeterRepository(ElectricityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<ElectricityMeter>> GetAll()
        {
            return await _dbContext.ElectricityMeters.ToListAsync();
        }

        public async Task<ElectricityMeter> GetById(int id)
        {
            return await _dbContext.ElectricityMeters.FirstOrDefaultAsync(o => o.Id == id);
        }
        
        public async Task Add(ElectricityMeter entity)
        {
            await _dbContext.ElectricityMeters.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task Update(ElectricityMeter entity)
        {
            _dbContext.ElectricityMeters.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task Delete(int id)
        {
            var entity = await _dbContext.ElectricityMeters.FindAsync(id);
            if (entity != null)
            {
                _dbContext.ElectricityMeters.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
        
        // Получение счетчиков указанного объекта потребления с просроченной поверкой (прошел год с даты поверки)
        public async Task<IEnumerable<ElectricityMeter>> GetMetersWithExpiredVerification(int objectId)
        {
            var oneYearAgo = DateTime.UtcNow.AddYears(-1);

            return await _dbContext.ElectricityMeters
                .Where(m => m.MeasurementPointEntity != null &&
                            m.MeasurementPointEntity.ConsumptionObjectId == objectId && // Связь через точку измерения
                            m.VerificationDate <= oneYearAgo)
                .ToListAsync();
        }

    }
}