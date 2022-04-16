using MediatR;
using RiseAssesment.Infrastructure.CQRS.Commands.Request;
using RiseAssesment.Infrastructure.CQRS.Commands.Response;
using RiseAssesment.Infrastructure.CQRS.Common;
using RiseAssesment.Infrastructure.CQRS.Queries;
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
    public class ContactManager : IContactManager
    {
        private readonly IMediator _mediator;
        public ContactManager(IMediator mediator)
        {
            _mediator = mediator; 
        }
        public async Task<CreateContactCommandResponse> CreateContactAsync(CreateContactCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<EmptyResponse> DeleteContactAsync(DeleteContactCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<IEnumerable<ListContactQueryResponse>> GetAllContactAsync(ListContactQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<ContactQueryResponse> GetContactAsync(GetContactQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<EmptyResponse> UpdateContactAsync(UpdateContactCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }
    }
}
