using CDN.Common;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Collections.Generic;
using CDN.UI.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace CDN.UI.Controllers
{
    public class FreeLancerController : Controller
    {
        private readonly string APIURL = "http://localhost:8888";
        private static HttpClient httpClient;

        public FreeLancerController(ILogger<FreeLancerController> logger)
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<FreeLancer>> Get()
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{APIURL}/api/FreeLancer");
            string responseJson = await response.Content.ReadAsStringAsync();
            List<FreeLancer> list = JsonSerializer.Deserialize<List<FreeLancer>>(responseJson);
            return list;
        }

        [HttpGet]
        public async Task<FreeLancer> GetDetails(int ID)
        {
            HttpResponseMessage response = await httpClient.GetAsync($"{APIURL}/api/FreeLancer/{ID}");
            string responseJson = await response.Content.ReadAsStringAsync();
            FreeLancer freelancer = JsonSerializer.Deserialize<FreeLancer>(responseJson);
            return freelancer;
        }

        [HttpPost]
        public async Task<object> Create(FreeLancer freeLancer)
        {
            try
            {
                string requestJson = JsonSerializer.Serialize(freeLancer);
                HttpResponseMessage response = await httpClient.PostAsync($"{APIURL}/api/FreeLancer/Create", new StringContent(requestJson, Encoding.UTF8, "application/json"));
                string responseJson = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    FreeLancer newFreeLancer = JsonSerializer.Deserialize<FreeLancer>(responseJson);
                    return newFreeLancer;
                }
                else
                {
                    return JsonSerializer.Deserialize<ObjectResult>(responseJson);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating freelancer.");
            }
        }

        [HttpPut]
        public async Task<object> Update(FreeLancer freeLancer)
        {
            try
            {
                string requestJson = JsonSerializer.Serialize(freeLancer);
                HttpResponseMessage response = await httpClient.PutAsync($"{APIURL}/api/FreeLancer/Update", new StringContent(requestJson, Encoding.UTF8, "application/json"));
                string responseJson = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                { 
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating freelancer.");
            }
        }

        [HttpDelete]
        public async Task<object> Delete(int ID)
        {
            try
            {
                HttpResponseMessage response = await httpClient.DeleteAsync($"{APIURL}/api/FreeLancer/Delete?id={ID}");
                string responseJson = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                { 
                    return true;
                }else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting freelancer.");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
