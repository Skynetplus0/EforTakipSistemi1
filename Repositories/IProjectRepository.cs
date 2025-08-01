using Baykasoglu.API.Models.Domain;

namespace Baykasoglu.API.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Projects>>GetAllAsync();




    }
}

