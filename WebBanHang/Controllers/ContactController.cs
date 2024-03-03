using Microsoft.AspNetCore.Mvc;
using WebBanHang.Data;

namespace WebBanHang.Controllers
{
	public class ContactController : Controller
	{
		private readonly Vshop2024Context db;

		public ContactController(Vshop2024Context context)
		{
			db = context;
		}
		public IActionResult Index()
		{
			return View();
		}
	}
}
