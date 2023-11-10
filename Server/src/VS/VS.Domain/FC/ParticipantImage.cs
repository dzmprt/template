namespace VS.Domain.FC
{
    public class ParticipantImage
    {
        public ParticipantImage()
        {
        }

        public int ParticipantId { get; set; }
        public Participant Participant { get; set; }


        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}
