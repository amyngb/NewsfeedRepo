using NewsfeedRepo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NewsfeedRepo.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public ActionResult CreateArticle()
		{
			var article = new Article();
			return View(article);
		}

		[HttpPost]
		public ActionResult CreateArticle(Article article)
		{
			var articleToAdd = new Article();
			articleToAdd.Author	= User.Identity.Name;
			articleToAdd.DatePosted = DateTime.Now;
			articleToAdd.Body = article.Body;
			articleToAdd.Title = article.Title;
			
			AddArticle(articleToAdd);
			return Redirect("/");
		}

		private void AddArticle(Article article)
		{
			List<Article> articleList;
			if ((List<Article>)Session["articles"] == null)
			{
				articleList = new List<Article>();
			}
			else
			{
				articleList = (List<Article>)Session["articles"];
			}
			
			articleList.Add(article);
			Session["articles"] = articleList;
		}
	}
}