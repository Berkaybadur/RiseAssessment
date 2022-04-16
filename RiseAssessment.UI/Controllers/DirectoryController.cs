using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RiseAssessment.API.Client;
using RiseAssessment.API.Client.Refit.Dependency;
using RiseAssessment.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RiseAssessment.UI.Controllers
{
    public class DirectoryController : Controller
    {
        private readonly IMapper _mapper;
        public DirectoryController(IMapper mapper)
        {
            _mapper = mapper;
        }
        //Using only list view and Mapper
        public IActionResult Index()
        {
            var response = RefitApiServiceDependency.DirectoryApi.List(new ListDirectoryQueryRequest
            {
            });
            if (response.Exception == null)
            {
                var result = _mapper.Map<List<ListDirectoryQueryResponseViewModel>>(response.Result);
                return View(result);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}
