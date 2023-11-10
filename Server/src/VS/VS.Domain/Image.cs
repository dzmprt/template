using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using UM.Domain;
using VS.Domain.FC;

namespace VS.Domain
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        public string BlobFileId { get; set; }
        [JsonIgnore]
        public BlobFile BlobFile { get; set; }

        public string OwnerId { get; set; }
        [JsonIgnore]
        public ApplicationUser Owner { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<ParticipantImage> ParticipantImages { get; set; }

        public Image()
        {
        }
    }
}
