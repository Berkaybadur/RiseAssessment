using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiseAssessment.API.Client.Refit.Interfaces
{
    public interface IContactApi
    {
        [Get("/Contact")]
        Task<IEnumerable<ListContactQueryResponse>> List([FromBody] ListContactQueryRequest requestModel);

        [Get("/Contact/{id}")]
        Task<ContactQueryResponse> Get(string id);

        [Post("/Contact")]
        Task<CreateContactCommandResponse> Post([FromBody] CreateContactCommandRequest requestModel);

        [Put("/Contact")]
        Task<EmptyResponse?> Put([FromBody] UpdateContactCommandRequest requestModel);

        [Delete("/Contact/{id}")]
        Task<EmptyResponse?> Delete([FromRoute] string id);
    }
}
