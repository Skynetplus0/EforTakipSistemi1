using Baykasoglu.API.Models.Domain;

namespace Baykasoglu.API.Models.DTO
{
    public class ProjectsDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid WorkerId { get; set; }
        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string? ProjectImageUrl { get; set; }

        // ? işareti nullable yapar


        public Worker? Worker { get; set; }

       


    }
}
