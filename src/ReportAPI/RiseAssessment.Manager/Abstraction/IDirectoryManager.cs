using RiseAssesment.Infrastructure.CQRS.Commands.Request;
using RiseAssesment.Infrastructure.CQRS.Commands.Response;
using RiseAssesment.Infrastructure.CQRS.Common;
using RiseAssesment.Infrastructure.CQRS.Queries.Request;
using RiseAssesment.Infrastructure.CQRS.Queries.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseAssessment.Manager.Abstraction
{
    public interface IDirectoryManager
    {
        Task<IEnumerable<ListDirectoryQueryResponse>> GetAllDirectoryAsync(ListDirectoryQueryRequest requestModel);
        Task<DirectoryQueryResponse> GetDirectoryAsync(GetDirectoryQueryRequest requestModel);
        Task<CreateDirectoryCommandResponse> CreateDirectoryAsync(CreateDirectoryCommandRequest requestModel);
        Task<EmptyResponse?> UpdateDirectoryAsync(UpdateDirectoryCommandRequest requestModel);
        Task<EmptyResponse?> DeleteDirectoryAsync(DeleteDirectoryCommandRequest requestModel);
    }
}
