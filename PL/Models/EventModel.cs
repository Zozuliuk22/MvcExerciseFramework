using System.Drawing;
using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class EventModel
    {
        public string Name { get; set; }

        public string WelcomeWord { get; set; }

        public string Color { get; set; }

        public Bitmap Image { get; set; }

        public bool IsEnteringFee { get; set; }

        [Required(ErrorMessage = "Enter a fee")]
        [Display(Name = "Please, enter a fee:")]
        [RegularExpression("^[0-9]*[,]?[0-9]+$", ErrorMessage = "Entered value is not valid.")]
        [Range(0, 100, ErrorMessage = "Entered value is not valid.")]
        public decimal EnteredFee { get; set; }

        public string PlayerScore { get; set; }

        public bool PlayerIsAlive { get; set; }

        public decimal PlayerCurrentBudget { get; set; }

        public bool IsPub { get; set; }

        public int PlayerCurrentBeers { get; set; }

        public string ResultMeetingMessage { get; set; }      
    }
}
