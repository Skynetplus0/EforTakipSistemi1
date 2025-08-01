using Baykasoglu.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Baykasoglu.API.Data
{
    public class MyDbContext: DbContext
    {
        //DbContextOptions dbContextOptions;
        public MyDbContext(DbContextOptions <MyDbContext>dbContextOptions)
            :base(dbContextOptions)
        {
                
        }

        public DbSet <Difficulty> Difficulties { get; set; }
        
        public DbSet <Projectmanager> projectmanagers { get; set; }

        public DbSet <Projects> Projects { get; set; }


        public DbSet <Work> works { get; set; }

        public DbSet <Worker> workers { get; set; }

    }
}
