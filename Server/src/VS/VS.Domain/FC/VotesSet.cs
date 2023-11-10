using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VS.Domain.FC
{
    [Table("VotesSet")]
    public class VotesSet
    {
        [Key]
        public int Id { get; set; }

        // TODO: ticket id int
        public string TicketKey { get; set; }

        public List<Vote> Votes { get; set; }
        
        [JsonIgnore]
        public string ClientIp { get; set; }
        
        [JsonIgnore]
        public string ClientUserAgent { get; set; }
    }
}
