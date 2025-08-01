using Baykasoglu.API.Data;
using Baykasoglu.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Baykasoglu.API.Repositories
{
    public class SQLProjectRepository : IProjectRepository
    {
        private readonly MyDbContext dbContext;

        public SQLProjectRepository(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<List<Projects>> GetAllAsync()
        {
          
           return await dbContext.Projects.ToListAsync();
            
            
            
            
            //  throw new NotImplementedException();
        }
    }
}
