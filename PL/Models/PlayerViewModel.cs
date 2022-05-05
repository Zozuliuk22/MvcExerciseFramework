using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PL.Models
{
    public class PlayerViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter a name")]
        [Display(Name = "Please, enter your name:")]
        [MaxLength(40, ErrorMessage = "Name must be shorter")]
        [RegularExpression(@"^(?!.*\s\s)(?!.*\.\.)(?!.*,,)[A-Z][a-zA-Z .,]{2,30}$", 
                            ErrorMessage = "Enter a name without numbers and start with A-Z")]
        public string Name { get; set; }

        public List<PlayerViewModel> Players { get; set; }
    }
}