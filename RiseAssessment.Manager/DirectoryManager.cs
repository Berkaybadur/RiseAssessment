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
    public class DirectoryManager : IDirectoryManager
    {
        private readonly IMediator _mediator;
        public DirectoryManager(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<CreateDirectoryCommandResponse> CreateDirectoryAsync(CreateDirectoryCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<EmptyResponse> DeleteDirectoryAsync(DeleteDirectoryCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<IEnumerable<ListDirectoryQueryResponse>> GetAllDirectoryAsync(ListDirectoryQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<DirectoryQueryResponse> GetDirectoryAsync(GetDirectoryQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<EmptyResponse> UpdateDirectoryAsync(UpdateDirectoryCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }
    }
}
