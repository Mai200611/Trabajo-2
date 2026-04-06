using Microsoft.AspNetCore.Mvc;

namespace AutoFleet.API.Controllers
{
    public class ConductorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
