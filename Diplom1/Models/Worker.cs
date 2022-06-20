using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom1.Models
{
    public class Worker
    {
        public int Id { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Введите имя")]
        [StringLength(50, ErrorMessage = "Имя не должно превышать 50 символов")]
        public string Name { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Введите фамилию")]
        [StringLength(50, ErrorMessage = "Фамилия не должна превышать 50 символов")]
        public string Surname { get; set; }

        [Display(Name = "Рабочие дни")]
        [Required(ErrorMessage = "Укажите рабочие дни")]
        public virtual ICollection<Day> Days { get; set; }

        [Display(Name = "Выполняемые работы")]
        [Required(ErrorMessage = "Укажите виды работ")]
        public virtual ICollection<Job> Jobs { get; set; }

        [Display(Name = "Привязанный Аккаунт")]
        public string AccountID { get; set; }

        [Display(Name = "Статус")]
        public string Status { get; set; }

        public Worker()
        {
            Days = new List<Day>();
            Jobs = new List<Job>();
        }
    }
}
