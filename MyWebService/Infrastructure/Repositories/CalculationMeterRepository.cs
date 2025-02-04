using Microsoft.EntityFrameworkCore;

namespace MyWebService.Infrastructure.Repositories
{
    public class CalculationMeterRepository : ICalculationMeterRepository
    {
        private readonly ElectricityDbContext _dbContext;

        public CalculationMeterRepository(ElectricityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<CalculationMeter>> GetAll()
        {
            return await _dbContext.CalculationMeters.ToListAsync();
        }
        
        public async Task<CalculationMeter> GetById(int id)
        {
            return await _dbContext.CalculationMeters.FirstOrDefaultAsync(o => o.Id == id);
        }
        
        public async Task Add(CalculationMeter entity)
        {
            await _dbContext.CalculationMeters.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task Update(CalculationMeter entity)
        {
            _dbContext.CalculationMeters.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task Delete(int id)
        {
            var entity = await _dbContext.CalculationMeters.FindAsync(id);
            if (entity != null)
            {
                _dbContext.CalculationMeters.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<CalculationMeter>> GetByYear(int year)
        {
            return await _dbContext.CalculationMeters
                .Where(m => m.StartTime.Year <= year && (m.EndTime == null || m.EndTime.Value.Year >= year))
                .ToListAsync();
        }

    }
}