using Microsoft.AspNetCore.Mvc;

namespace TurboRentingv2.Api.Controllers
{
    public class HomeController: Controller
    {

        public IActionResult Index()
        {
            return Content("API Working!");
        }
    }
}
