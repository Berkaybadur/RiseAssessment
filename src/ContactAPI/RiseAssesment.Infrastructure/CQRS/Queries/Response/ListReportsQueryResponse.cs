using RiseAssesment.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseAssesment.Infrastructure.CQRS.Queries.Response
{
    public class ListReportsQueryResponse
    {
        public string Id { get; set; }
        public DateTime CreateDate { get; set; }
        public ReportStatus ReportStatus { get; set; }
    }
}
