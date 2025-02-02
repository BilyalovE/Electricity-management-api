using Microsoft.EntityFrameworkCore;

namespace MyWebService.Infrastructure.Repositories
{
    public class VoltageTransformerRepository : IVoltageTransformerRepository
    {
        private readonly ElectricityDbContext _dbContext;

        public VoltageTransformerRepository(ElectricityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<VoltageTransformer>> GetAll()
        {
            return await _dbContext.VoltageTransformers.ToListAsync();
        }

        public async Task<VoltageTransformer> GetById(int id)
        {
            return await _dbContext.VoltageTransformers.FirstOrDefaultAsync(o => o.Id == id);
        }
        
        public async Task Add(VoltageTransformer entity)
        {
            await _dbContext.VoltageTransformers.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task Update(VoltageTransformer entity)
        {
            _dbContext.VoltageTransformers.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task Delete(int id)
        {
            var entity = await _dbContext.VoltageTransformers.FindAsync(id);
            if (entity != null)
            {
                _dbContext.VoltageTransformers.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
        
        // Получение трансформаторов указанного объекта потребления с просроченной поверкой (прошел год с даты поверки)
        public async Task<IEnumerable<VoltageTransformer>> GetTransformersWithExpiredVerification(int objectId)
        {                                                   
            var oneYearAgo = DateTime.UtcNow.AddYears(-1);

            return await _dbContext.VoltageTransformers
                .Where(m => m.MeasurementPointEntity != null &&
                            m.MeasurementPointEntity.ConsumptionObjectId == objectId && // Связь через точку измерения
                            m.VerificationDate <= oneYearAgo)
                .ToListAsync();
        }
    }
}