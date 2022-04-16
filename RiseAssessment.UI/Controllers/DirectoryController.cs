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
    public class DirectoryController : Controller
    {
        private readonly IMapper _mapper;
        public DirectoryController(IMapper mapper)
        {
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var response = RefitApiServiceDependency.DirectoryApi.List(new ListDirectoryQueryRequest
            {
            });
            if (response.Exception == null)
            {
                var result = response.Result;
                return View(result);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        public IActionResult Add()
        {

            return View();
        }
    }
}
