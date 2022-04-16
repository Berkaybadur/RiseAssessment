using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseAssessment.API.Client
{
    public class CreateDirectoryCommandRequest : IRequest<CreateDirectoryCommandResponse>
    {
        public string Company { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
