using System.ComponentModel.DataAnnotations;

namespace VS.Domain.FC
{
    public class Participant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public int? ImageId { get; set; }
        public Image Image { get; set; }

        public int ContestCategoryId { get; set; }
        public ContestCategory ContestCategory { get; set; }

        public List<Vote> Votes { get; set; } = new List<Vote>();

        public List<ParticipantImage> ParticipantImages { get; set; } = new List<ParticipantImage>();
    }
}
