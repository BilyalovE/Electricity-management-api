using Microsoft.EntityFrameworkCore;

namespace MyWebService.Infrastructure.Repositories
{
    public class SupplyPointRepository : ISupplyPointRepository
    {
        private readonly ElectricityDbContext _dbContext;

        public SupplyPointRepository(ElectricityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<SupplyPoint>> GetAll()
        {
            return await _dbContext.SupplyPoints.ToListAsync();
        }
        
        public async Task<SupplyPoint> GetById(int id)
        {
            return await _dbContext.SupplyPoints.FirstOrDefaultAsync(o => o.Id == id);
        }
        
        public async Task Add(SupplyPoint entity)
        {
            await _dbContext.SupplyPoints.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task Update(SupplyPoint entity)
        {
            _dbContext.SupplyPoints.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task Delete(int id)
        {
            var entity = await _dbContext.SupplyPoints.FindAsync(id);
            if (entity != null)
            {
                _dbContext.SupplyPoints.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}