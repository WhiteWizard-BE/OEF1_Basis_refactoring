using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VivesBlog.Core;

namespace VivesBlog.Cyb.Ui.Mvc.Controllers
{
	public class HomeController(VivesBlogDbContext dbContext) : Controller
	{
		public IActionResult Index()
		{
			var articles = dbContext.Articles
				.Include(a => a.Author)
				.ToList();
			return View(articles);
		}

		public IActionResult Details(int id)
		{
			var article = dbContext.Articles
				.Include(a => a.Author)
				.SingleOrDefault(a => a.Id == id);

			return View(article);
		}
	}
}
