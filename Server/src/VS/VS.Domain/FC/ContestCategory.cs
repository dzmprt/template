using System.ComponentModel.DataAnnotations;

namespace VS.Domain.FC
{
    public class ContestCategory 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Name { get; set; }

        public int ContestId { get; set; }
        public Contest Contest { get; set; }

        public List<Participant> Participants { get; set; } = new List<Participant>();
    }
}
