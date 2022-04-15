using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseAssessment.API.Client
{
    public class ListContactQueryRequest : IRequest<IEnumerable<ListContactQueryResponse>>
    {
        public string DirectoryId { get; set; }
    }
}
