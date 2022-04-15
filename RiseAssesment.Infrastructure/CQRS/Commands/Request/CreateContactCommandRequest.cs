using MediatR;
using RiseAssesment.Infrastructure.CQRS.Commands.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseAssesment.Infrastructure.CQRS.Commands.Request
{
    public class CreateContactCommandRequest : IRequest<CreateContactCommandResponse>
    {
        [Required]
        public string Email { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
    }
}
