using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom1.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Worker> Workers { get; set; }

        public Job()
        {
            Workers = new List<Worker>();
        }
    }
}
