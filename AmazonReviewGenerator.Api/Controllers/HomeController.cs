using Microsoft.AspNetCore.Mvc;

namespace AmazonReviewGenerator.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
