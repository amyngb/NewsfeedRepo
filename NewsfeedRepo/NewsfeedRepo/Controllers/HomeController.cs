using NewsfeedRepo.Models;
//using NewsfeedRepo.Managers;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NewsfeedRepo.Controllers
{
	public class HomeController : Controller
	{
		ArticleManager _articleManager = new ArticleManager();

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
			_articleManager.AddArticle(article);
			return Redirect("/");
		}

		[HttpGet]
		public ActionResult ViewArticles()
		{
			return View();
		}

		[HttpPost]
		public ActionResult ViewArticles(ArticleComment comment)
		{
			return View();
		}
	}
}