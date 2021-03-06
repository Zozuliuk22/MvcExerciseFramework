using System.ComponentModel.DataAnnotations;
using DAL.Enums;

namespace DAL.Entities
{
    public class FoolNpc
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [EnumDataType(typeof(FoolsPractice))]
        public FoolsPractice Practice { get; set; }
    }
}
