using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom1.Models
{
    public class ServiceStationInitializer : DropCreateDatabaseIfModelChanges<ServiceStationContext>
    {
        protected override void Seed(ServiceStationContext context)
        {
            Job j1 = context.Jobs.Add(new Job { Id = 1, Name = "Aвтокондиционеры" });
            Job j2 = context.Jobs.Add(new Job { Id = 2, Name = "Диагностика тормозной системы" });
            Job j3 = context.Jobs.Add(new Job { Id = 3, Name = "Диагностика подвески" });
            Job j4 = context.Jobs.Add(new Job { Id = 4, Name = "Компьютерная диагностика" });
            Job j5 = context.Jobs.Add(new Job { Id = 5, Name = "Развал-схождение 3D" });
            Job j6 = context.Jobs.Add(new Job { Id = 6, Name = "Ремонт или замена ДВС" });
            Job j7 = context.Jobs.Add(new Job { Id = 7, Name = "Ремонт рулевого управления" });
            Job j8 = context.Jobs.Add(new Job { Id = 8, Name = "Ремонт тормозной системы" });
            Job j9 = context.Jobs.Add(new Job { Id = 9, Name = "Ремонт подвески" });
            Job j10 = context.Jobs.Add(new Job { Id = 10, Name = "Ремонт или замена КПП" });
            Job j11 = context.Jobs.Add(new Job { Id = 11, Name = "Замена масла" });
            Job j12 = context.Jobs.Add(new Job { Id = 12, Name = "Замена фильтров" });
            Job j13 = context.Jobs.Add(new Job { Id = 13, Name = "Шиномонтаж" });
            context.SaveChanges();
            Day d1 = context.Days.Add(new Day { Id = 1, Name = "Понедельник" });
            Day d2 = context.Days.Add(new Day { Id = 2, Name = "Вторник" });
            Day d3 = context.Days.Add(new Day { Id = 3, Name = "Среда" });
            Day d4 = context.Days.Add(new Day { Id = 4, Name = "Четверг" });
            Day d5 = context.Days.Add(new Day { Id = 5, Name = "Пятница" });
            Day d6 = context.Days.Add(new Day { Id = 6, Name = "Суббота" });
            Day d7 = context.Days.Add(new Day { Id = 7, Name = "Воскресенье" });
            context.SaveChanges();
            Worker w1 = context.Workers.Add(new Worker
            {
                Id = 1,
                Name = "Владислав",
                Surname = "Бурий",
                Status = "Работает",
                Jobs = new List<Job>() { j1, j2, j12 },
                Days = new List<Day>() { d1, d3, d5 }
            });
            context.SaveChanges();
            context.Persons.Add(new Person { Id = 1, Name = "Владислав", Phone = 291111111 });
            context.SaveChanges();
            context.Orders.Add(new Order { Id = 1, Car = "Audi A6", Date = new DateTime(2019, 12, 31, 0, 0, 0), Status = "Заявка", JobId = 2, WorkerId = 1, Comment = "", PersonId = 1 });
            context.SaveChanges();
        }
    }
}
