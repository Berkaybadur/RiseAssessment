using MediatR;
using RiseAssesment.Infrastructure.CQRS.Queries.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseAssesment.Infrastructure.CQRS.Queries.Request
{
    public class ListReportsQueryRequest : IRequest<IEnumerable<ListReportsQueryResponse>>
    {
        public DateTime CreateDate { get; set; }
    }
}
