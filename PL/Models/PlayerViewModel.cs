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
        public string Name { get; set; }

        public int HighScore { get; set; }

        public List<PlayerViewModel> Players { get; set; }
    }
}