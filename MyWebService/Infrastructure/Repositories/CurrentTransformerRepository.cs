using Microsoft.EntityFrameworkCore;

namespace MyWebService.Infrastructure.Repositories
{
    public class CurrentTransformerRepository : ICurrentTransformerRepository
    {
        private readonly ElectricityDbContext _dbContext;

        public CurrentTransformerRepository(ElectricityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<CurrentTransformer>> GetAll()
        {
            return await _dbContext.CurrentTransformers.ToListAsync();
        }

        public async Task<CurrentTransformer> GetById(int id)
        {
            return await _dbContext.CurrentTransformers.FirstOrDefaultAsync(o => o.Id == id);
        }
        
        public async Task Add(CurrentTransformer entity)
        {
            await _dbContext.CurrentTransformers.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task Update(CurrentTransformer entity)
        {
            _dbContext.CurrentTransformers.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task Delete(int id)
        {
            var entity = await _dbContext.CurrentTransformers.FindAsync(id);
            if (entity != null)
            {
                _dbContext.CurrentTransformers.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
        
        // Получение трансформаторов указанного объекта потребления с просроченной поверкой (прошел год с даты поверки)
        public async Task<IEnumerable<CurrentTransformer>> GetTransformersWithExpiredVerification(int objectId)
        {                                                   
            var oneYearAgo = DateTime.UtcNow.AddYears(-1);

            return await _dbContext.CurrentTransformers
                .Where(m => m.MeasurementPointEntity != null &&
                            m.MeasurementPointEntity.ConsumptionObjectId == objectId && // Связь через точку измерения
                            m.VerificationDate <= oneYearAgo)
                .ToListAsync();
        }
    }
}