using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RiseAssessment.API.Client;
using RiseAssessment.API.Client.Refit.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiseAssessment.UI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IMapper _mapper;
        public ContactController(IMapper mapper)
        {
            _mapper = mapper;
        }
        //Using only list view and Mapper
        public IActionResult Index()
        {
            var response = RefitApiServiceDependency.ContactApi.List(new ListContactQueryRequest
            {
            });
            if (response.Exception == null)
            {
                var result = _mapper.Map<List<ListContactQueryResponseViewModel>>(response.Result);
                return View(result);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
