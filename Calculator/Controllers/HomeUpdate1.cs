using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Calculator.Controllers
{
    public partial class HomeController : Controller
    {
        public async Task<IActionResult> Pa2()
        {
            var res = await .GetPayCategory();

            ViewData["CategoryId"] = new SelectList(res, "Id", "Name");

            return View();
        }
    }
}
