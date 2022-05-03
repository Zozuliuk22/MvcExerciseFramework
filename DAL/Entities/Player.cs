using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Player
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        public int HighScore { get; set; }
    }
}
