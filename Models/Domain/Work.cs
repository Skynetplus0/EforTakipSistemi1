namespace Baykasoglu.API.Models.Domain
{
    public class Work
    {

        public Guid Id { get; set; } 

        public string Name { get; set; }    

        public string Description { get; set; }

        public double LenghtInHours { get; set; }

        public Guid ProjectsId { get; set; }

        public Guid DifficultyId { get; set; }

        //Bağlılar navigation properties

        public Difficulty? Difficulty { get; set; }
        public Projects Projects { get; set; }

    }
}
