using E_ShopWEBUI.Core.ViewModel;
using E_ShopWEBUI.Helper.SessionHelper;
using Microsoft.AspNetCore.Mvc;

namespace E_ShopWEBUI.WEBUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("/Admin/Anasayfa")]
        public IActionResult Index()
        {

            CustomerViewModel customer = new()
            {
                AdSoyad = SessionManager.LoggedCustomer.AdSoyad,
                ID = Convert.ToInt32(SessionManager.LoggedCustomer.CustomerID),
                EPosta = SessionManager.LoggedCustomer.EPosta,
                Adres = SessionManager.LoggedCustomer.Adres,
            };


            return View(customer);

        }
    }
}
