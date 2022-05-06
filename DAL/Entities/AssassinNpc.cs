using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class AssassinNpc
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        public decimal MinReward { get; set; }

        [Required]
        public decimal MaxReward { get; set; }
    }
}
