using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RiseAssessment.API.Client;
using RiseAssessment.API.Client.Refit.Dependency;
using RiseAssessment.UI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RiseAssessment.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var listcontact = RefitApiServiceDependency.ContactApi.List(new ListContactQueryRequest
            {
            });
            var contact = listcontact.Result;

            var listdirectory = RefitApiServiceDependency.DirectoryApi.List(new ListDirectoryQueryRequest
            {
            });
            var directory = listdirectory.Result;
            var list = new HomeIndexViewModel
            { ContactCount=contact.Count(),
             DirectoryCount=directory.Count()};
            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
