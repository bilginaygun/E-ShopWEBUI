using E_ShopWEBUI.Core.DTO;
using E_ShopWEBUI.Core.Result;
using E_ShopWEBUI.Helper.SessionHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

using System.Net;
using System.Security.AccessControl;

namespace ShoppingWEBUI.WebUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IHttpContextAccessor contextAccessor)
        {
            _httpContextAccessor = contextAccessor;
        }

        [HttpGet("/AdminAccount/Login")]
        public IActionResult Index()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            return View();
        }

        [HttpPost("/Account/AdminLogin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin(LoginDTO loginDTO)
        {
            var url = "http://localhost:5183/Login";
            var client = new RestClient(url);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var body = JsonConvert.SerializeObject(loginDTO);
            request.AddBody(body, "application/json");
            RestResponse restResponse = await client.ExecuteAsync(request);

            var responseObject = JsonConvert.DeserializeObject<ApiResult<LoginDTO>>(restResponse.Content);

            if (restResponse.StatusCode == HttpStatusCode.NotFound && responseObject?.Data == null)
            {
                ViewBag.LoginError = "Buraya Bir Data Geliyor";
                ViewData["LoginError"] = "Kullanıcı Adı Veya Şifre Yanlış";
                TempData["LoginError"] = "Buraya Başka Bir Data Geliyor";
                return View("Index");
            }
            else if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                ViewData["LoginError"] = "Hata Oluştu";
                return View("Index");
            }

       


            SessionManager.LoggedCustomer = responseObject.Data;

            return RedirectToAction("Index", "Home");
        }

    }
}
