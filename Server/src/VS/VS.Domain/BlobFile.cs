using System.ComponentModel.DataAnnotations;

namespace VS.Domain
{
    public class BlobFile
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Extension { get; set; }

        [Required]
        public byte[] Data { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
