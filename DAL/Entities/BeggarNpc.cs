using System.ComponentModel.DataAnnotations;
using DAL.Enums;

namespace DAL.Entities
{
    public class BeggarNpc
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }

        [EnumDataType(typeof(BeggarsPractice))]
        public BeggarsPractice Practice { get; set; }
    }
}
