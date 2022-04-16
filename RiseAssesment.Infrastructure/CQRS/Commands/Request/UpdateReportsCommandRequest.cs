using MediatR;
using RiseAssesment.Infrastructure.CQRS.Common;
using RiseAssesment.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseAssesment.Infrastructure.CQRS.Commands.Request
{
    public class UpdateReportsCommandRequest : IRequest<EmptyResponse>
    {
        public string Id { get; set; }
        public ReportStatus ReportStatus { get; set; }
    }
}
