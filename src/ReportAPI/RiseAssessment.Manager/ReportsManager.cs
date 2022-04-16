using MediatR;
using RiseAssesment.Infrastructure.CQRS.Commands.Request;
using RiseAssesment.Infrastructure.CQRS.Commands.Response;
using RiseAssesment.Infrastructure.CQRS.Common;
using RiseAssesment.Infrastructure.CQRS.Queries.Request;
using RiseAssesment.Infrastructure.CQRS.Queries.Response;
using RiseAssessment.Manager.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseAssessment.Manager
{
    public class ReportsManager : IReportsManager
    {
        private readonly IMediator _mediator;
        public ReportsManager(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<CreateReportsCommandResponse> CreateReportsAsync(CreateReportsCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
            //TODO : Producer Apisine İstek 
        }

        public async Task<IEnumerable<ListReportsQueryResponse>> GetAllReportsAsync(ListReportsQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<ReportsQueryResponse> GetReportsAsync(GetReportsQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<EmptyResponse> UpdateReportsAsync(UpdateReportsCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }
    }
}
