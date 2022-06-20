using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom1.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Display(Name = "Автомобиль")]
        [Required(ErrorMessage = "Введите название автомобиля")]
        [StringLength(50, ErrorMessage = "Название не должно превышать 50 символов")]
        public string Car { get; set; }

        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public int? PersonId { get; set; }

        [Display(Name = "Клиент")]
        public Person Person { get; set; }
        public int? JobId { get; set; }

        [Display(Name = "Вид работ")]
        public Job Job { get; set; }
        public int? WorkerId { get; set; }

        [Display(Name = "Работник")]
        public Worker Worker { get; set; }

        [Display(Name = "Комментарий")]
        [StringLength(500, ErrorMessage = "Комментарий не должен превышать 500 символов")]
        public string Comment { get; set; }

        [Display(Name = "Статус")]
        public string Status { get; set; }
    }
}
