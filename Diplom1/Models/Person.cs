using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom1.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [StringLength(50, ErrorMessage = "Имя не должно превышать 50 символов")]
        [MinLength(1)]
        public string Name { get; set; }

        [Display(Name = "Телефон")]
        [Required]
        [Index(IsUnique = true)]
        [Range(100000000, 999999999, ErrorMessage = "Укажите номер из девяти цифр")]
        public int Phone { get; set; }
    }
}
