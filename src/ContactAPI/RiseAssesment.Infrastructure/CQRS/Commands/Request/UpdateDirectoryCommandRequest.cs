using MediatR;
using RiseAssesment.Infrastructure.CQRS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseAssesment.Infrastructure.CQRS.Commands.Request
{
    public class UpdateDirectoryCommandRequest : IRequest<EmptyResponse>
    {
        public string Id { get; set; }
        public string Company { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
