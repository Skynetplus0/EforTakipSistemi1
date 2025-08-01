using Baykasoglu.API.Models.Domain;

namespace Baykasoglu.API.Repositories
{
    public class InMemoryProjectsRepository : IProjectRepository
    {
        public async Task<List<Projects>> GetAllAsync()
        {

            return new List<Projects>
            {
                new Projects()
                {
                    Id = Guid.NewGuid(),
                    Description = "Naber",
                    Name = "iyi",
                    ProjectImageUrl = "FWFWEFWEF"


                }

            };
        }
    }
}
