using System.ComponentModel.DataAnnotations;
using UM.Domain;

namespace VS.Domain.FC
{
    public class Contest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Name { get; set; }

        public bool Started { get; set; }

        public bool Finished { get; set; }

        public DateTime DateCreation { get; set; }

        public DateTime? DateStarted { get; set; }

        public DateTime? DateFinished { get; set; }

        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }

        public List<ContestCategory> ContestCategories { get; set; } = new List<ContestCategory>();

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();

        public string? TestTicketKey { get; set; }

        public int MaximumNumberOfVotesInCategory { get; set; }
    }
}
