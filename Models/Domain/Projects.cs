namespace Baykasoglu.API.Models.Domain
{
    public class Projects
    {
        //Görev
        public Guid Id { get; set; } 

        public Guid? WorkerId { get; set; }
        public DateTime Date { get; set; }
        
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? ProjectImageUrl { get; set; }

        // ? işareti nullable yapar
        public DateTime? DeletedDateTime { get; set; }

        public int? Hours { get; set; }

        public Worker? Worker { get; set; }

        
    }
}
