using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VS.Domain.FC
{
    public class Vote
    {
        [Key]
        public int Id { get; set; }

        public int ParticipantId { get; set; }
        [JsonIgnore]
        public Participant Participant { get; set; }

        public int VotesSetId { get; set; }
        [JsonIgnore]
        public VotesSet VotesSet { get; set; }

        public int PrizeNumber { get; set; }
    }
}
