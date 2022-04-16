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
    public interface IReportsManager
    {
        Task<IEnumerable<ListReportsQueryResponse>> GetAllReportsAsync(ListReportsQueryRequest requestModel);
        Task<ReportsQueryResponse> GetReportsAsync(GetReportsQueryRequest requestModel);
        Task<CreateReportsCommandResponse> CreateReportsAsync(CreateReportsCommandRequest requestModel);
        Task<EmptyResponse?> UpdateReportsAsync(UpdateReportsCommandRequest requestModel);

    }
}
