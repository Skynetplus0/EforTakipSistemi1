using System.Text.Json.Serialization;

namespace Baykasoglu.API.Models.Domain
{
    public class Worker
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime? DeletedDateTime { get; set; }

        [JsonIgnore]
        public ICollection<Projects> Projects { get; set; }=new List<Projects>();

    }
}
