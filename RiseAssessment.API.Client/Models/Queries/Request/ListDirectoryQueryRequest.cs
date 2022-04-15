using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseAssessment.API.Client
{
    public class ListDirectoryQueryRequest : IRequest<IEnumerable<ListDirectoryQueryResponse>>
    {
        public string Name { get; set; }
    }
}
