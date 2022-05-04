using System.Drawing;

namespace BLL.DTOs
{
    public class EventDto
    {
        public string Name { get; set; }

        public string WelcomeWord { get; set; }

        public string Color { get; set; }

        public Bitmap Image { get; set; }

        public bool IsEnteringFee { get; set; }

        public string PlayerScore { get; set; }

        public bool PlayerIsAlive { get; set; }

        public decimal PlayerCurrentBudget { get; set; }

        public bool IsPub { get; set; }

        public int PlayerCurrentBeers { get; set; }

        public string ResultMeetingMessage { get; set; }
    }
}
