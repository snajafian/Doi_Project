using Doi_Project.Models;
using Doi_Project.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Doi_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetConsortium(string consortiumId)
        {

            var result = new ResultViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://api.datacite.org/providers?consortium-id={consortiumId}"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ResultViewModel>(apiResponse);
                }
            }
            return PartialView("_Providers",result);

        }

        [HttpGet]
        public async Task<IActionResult> GetClient(string clientId)
        {

            var result = new ClientResultViewModel();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"https://api.datacite.org/dois?client_id={clientId}"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ClientResultViewModel>(apiResponse);
                }
            }
            return Ok(new { count = result.Data.Count() }); ;

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
