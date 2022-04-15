using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseAssessment.API.Client.Refit.Interfaces
{
    public interface IDirectoryApi
    {
        [Get("/Directory")]
        Task<IEnumerable<ListDirectoryQueryResponse>> List([FromBody] ListDirectoryQueryRequest requestModel);

        [Get("/Directory/{id}")]
        Task<DirectoryQueryResponse> Get(string id);

        [Post("/Directory")]
        Task<CreateDirectoryCommandResponse> Post([FromBody] CreateDirectoryCommandRequest requestModel);

        [Put("/Directory")]
        Task<EmptyResponse?> Put([FromBody] UpdateDirectoryCommandRequest requestModel);

        [Delete("/Directory/{id}")]
        Task<EmptyResponse?> Delete([FromRoute] string id);
    }
}
