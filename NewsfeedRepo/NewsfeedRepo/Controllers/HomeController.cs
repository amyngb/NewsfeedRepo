using NewsfeedRepo.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NewsfeedRepo.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet]
		public ActionResult Index()
		{
			var articleList = GetArticles();
			return View(articleList);
		}

		[HttpPost]
		public ActionResult CreateArticle(Article article)
		{
			AddArticle(article);
			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult CreateComment(ArticleComment comment)
		{
			AddComment(comment);
			return RedirectToAction("Index");
		}

		public static List<Article> Articles = new List<Article>();

		public void AddComment(ArticleComment comment)
		{
			throw new NotImplementedException();
		}

		public void AddArticle(Article article)
		{
			var articleToAdd = new Article();
			articleToAdd.Author = User.Identity.Name.ToString();
			articleToAdd.DatePosted = DateTime.Now;
			articleToAdd.Body = article.Body;
			articleToAdd.Title = article.Title;

			Articles.Add(article);
		}

		public List<Article> GetArticles()
		{
			return Articles;
		}
	}
}