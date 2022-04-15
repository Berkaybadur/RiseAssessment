using RiseAssesment.Infrastructure.CQRS.Commands.Request;
using RiseAssesment.Infrastructure.CQRS.Commands.Response;
using RiseAssesment.Infrastructure.CQRS.Common;
using RiseAssesment.Infrastructure.CQRS.Queries;
using RiseAssesment.Infrastructure.CQRS.Queries.Request;
using RiseAssesment.Infrastructure.CQRS.Queries.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseAssessment.Manager.Abstraction
{
    public interface IContactManager
    {
        Task<IEnumerable<ListContactQueryResponse>> GetAllContactAsync(ListContactQueryRequest requestModel);
        Task<ContactQueryResponse> GetContactAsync(GetContactQueryRequest requestModel);
        Task<CreateContactCommandResponse> CreateContactAsync(CreateContactCommandRequest requestModel);
        Task<EmptyResponse?> UpdateContactAsync(UpdateContactCommandRequest requestModel);
        Task<EmptyResponse?> DeleteContactAsync(DeleteContactCommandRequest requestModel);

    }
}
