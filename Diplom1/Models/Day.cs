using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom1.Models
{
    public class Day
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Worker> Workers { get; set; }

        public Day()
        {
            Workers = new List<Worker>();
        }
    }
}
