using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseAssesment.Infrastructure.Models
{
    public class Directory : BaseEntity
    {
        public string Company { get; set; }
        public string Name { get; set; }
        public int Surname { get; set; }
    }
}
