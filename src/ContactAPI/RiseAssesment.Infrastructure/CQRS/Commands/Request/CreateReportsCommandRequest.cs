using MediatR;
using RiseAssesment.Infrastructure.CQRS.Commands.Response;
using RiseAssesment.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseAssesment.Infrastructure.CQRS.Commands.Request
{
    public class CreateReportsCommandRequest : IRequest<CreateReportsCommandResponse>
    {
        public DateTime CreateDate { get; set; }
        public ReportStatus ReportStatus { get; set; }

    }
}
