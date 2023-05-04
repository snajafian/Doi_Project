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
        public async Task<IActionResult> GetConsortium(string consortiumId,long page=1)
        {
            var result = Tuple.Create(false, "ورودی ها را کنترل کنید", "");
            try
            {
                var ResultViewModel = new ResultViewModel();
                consortiumId=consortiumId.Trim();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"https://api.datacite.org/providers?consortium-id={consortiumId}&page[number]={page}"))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        ResultViewModel = JsonConvert.DeserializeObject<ResultViewModel>(apiResponse);
                    }
                }
                return PartialView("_Providers", ResultViewModel);
            }
            catch (Exception e)
            {
                return Json(new { success = result.Item1, message = "An unknown error has occurred", error = e });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetClient(string clientId)
        {
            var result = Tuple.Create(false, "ورودی ها را کنترل کنید", "");
            try
            {
                
                var ClientResultViewModel = new ClientResultViewModel();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"https://api.datacite.org/dois?client_id={clientId}"))
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();
                        ClientResultViewModel = JsonConvert.DeserializeObject<ClientResultViewModel>(apiResponse);
                    }
                }
                //var count = ClientResultViewModel.Data.Count();
                var count = ClientResultViewModel.Meta.Total;
                return Json(new { success = true, message = "Count of dois is "+count, error = "", count = count });
            }
            catch (Exception e)
            {
                return Json(new { success = result.Item1, message = "An unknown error has occurred", error = e });
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
