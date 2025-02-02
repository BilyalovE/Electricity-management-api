using Microsoft.EntityFrameworkCore;

namespace MyWebService.Infrastructure.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ElectricityDbContext _dbContext;

        public OrganizationRepository(ElectricityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<Organization>> GetAll()
        {
            return await _dbContext.Organizations.ToListAsync();
        }
        
        public async Task<Organization> GetById(int id)
        {
            return await _dbContext.Organizations.FirstOrDefaultAsync(o => o.Id == id);
        }
        
        public async Task Add(Organization entity)
        {
            await _dbContext.Organizations.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task Update(Organization entity)
        {
            _dbContext.Organizations.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task Delete(int id)
        {
            var entity = await _dbContext.Organizations.FindAsync(id);
            if (entity != null)
            {
                _dbContext.Organizations.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}