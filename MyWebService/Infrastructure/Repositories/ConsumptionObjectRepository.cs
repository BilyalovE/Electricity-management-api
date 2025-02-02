using Microsoft.EntityFrameworkCore;

namespace MyWebService.Infrastructure.Repositories
{
    public class ConsumptionObjectRepository : IConsumptionObjectRepository
    {
        private readonly ElectricityDbContext _dbContext;

        public ConsumptionObjectRepository(ElectricityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<ConsumptionObject>> GetAll()
        {
            return await _dbContext.ConsumptionObjects.ToListAsync();
        }
        public async Task<ConsumptionObject> GetById(int id)
        {
            return await _dbContext.ConsumptionObjects.FirstOrDefaultAsync(o => o.Id == id);
        }
        
        public async Task Add(ConsumptionObject entity)
        {
            await _dbContext.ConsumptionObjects.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task Update(ConsumptionObject entity)
        {
            _dbContext.ConsumptionObjects.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task Delete(int id)
        {
            var entity = await _dbContext.ConsumptionObjects.FindAsync(id);
            if (entity != null)
            {
                _dbContext.ConsumptionObjects.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}