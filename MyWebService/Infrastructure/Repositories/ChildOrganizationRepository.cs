using Microsoft.EntityFrameworkCore;

namespace MyWebService.Infrastructure.Repositories
{
    public class ChildOrganizationRepository : IChildOrganizationRepository
    {
        private readonly ElectricityDbContext _dbContext;

        public ChildOrganizationRepository(ElectricityDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<ChildOrganization>> GetAll()
        {
            return await _dbContext.ChildOrganizations.ToListAsync();
        }
        public async Task<ChildOrganization> GetById(int id)
        {
            return await _dbContext.ChildOrganizations.FirstOrDefaultAsync(o => o.Id == id);
        }
        
        public async Task Add(ChildOrganization entity)
        {
            await _dbContext.ChildOrganizations.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task Update(ChildOrganization entity)
        {
            _dbContext.ChildOrganizations.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task Delete(int id)
        {
            var entity = await _dbContext.ChildOrganizations.FindAsync(id);
            if (entity != null)
            {
                _dbContext.ChildOrganizations.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}