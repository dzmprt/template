using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VS.Domain.FC
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        public string Key { get; set; }

        public bool IsUsed { get; set; }

        public int ContestId { get; set; }

        [JsonIgnore]
        public Contest Contest { get; set; }

        public DateTime DateCreation { get; set; }

        public DateTime? DateUsed { get; set; }

        public bool CreatedByTestTiket { get; set; }
    }
}
