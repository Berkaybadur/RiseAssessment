using MediatR;
using RiseAssesment.Infrastructure.CQRS.Commands.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseAssesment.Infrastructure.CQRS.Commands.Request
{
    public class CreateDirectoryCommandRequest : IRequest<CreateDirectoryCommandResponse>
    {
        public string Company { get; set; }
        public string Name { get; set; }
        public int Surname { get; set; }
    }
}
