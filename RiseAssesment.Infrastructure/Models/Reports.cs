using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseAssesment.Infrastructure.Models
{
    public class Reports : BaseEntity
    {
        public ReportStatus ReportStatus { get; set; }
    }
}
