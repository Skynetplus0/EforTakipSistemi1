namespace Baykasoglu.API.Models.DTO
{
    public class AddProjectRequestDTO
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public string? ProjectImageUrl { get; set; }

        public Guid WorkerId { get; set; }

    }
}
