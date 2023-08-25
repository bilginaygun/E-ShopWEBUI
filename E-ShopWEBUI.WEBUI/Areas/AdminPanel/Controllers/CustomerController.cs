using E_ShopWEBUI.Core.DTO;
using E_ShopWEBUI.Core.Result;
using E_ShopWEBUI.Helper.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace E_ShopWEBUI.WEBUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CustomerController : Controller
    {
        [HttpGet("/Admin/Kullanicilar")]
        public async Task<IActionResult> Index()
        {
            var url = "http://localhost:5183/Customers";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedCustomer.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<List<CustomerDTO>>>(restResponse.Content);

            var customers = responseObject.Data;

            return View(customers);
        }


        [HttpGet("/Admin/Customer/{CustomerGUID}")]
        public async Task<IActionResult> GetCustomer(Guid CustomerGUID)
        {
            var url = "http://localhost:5183/Customer/" + CustomerGUID;
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + SessionManager.LoggedCustomer.Token);
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<CustomerDTO>>(restResponse.Content);

            var category = responseObject.Data;

            if (restResponse.StatusCode == HttpStatusCode.OK)
            {
                return Json(new { success = true, data = responseObject.Data });
            }
            else if (restResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                return Json(new { success = false, HataBilgisi = responseObject.HataBilgisi });
            }
            else
            {
                return Json(new { success = false, HataBilgisi = responseObject.HataBilgisi });
            }
        }
    }
}
