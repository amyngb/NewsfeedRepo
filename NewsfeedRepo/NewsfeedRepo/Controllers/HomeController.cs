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

		[HttpGet]
		public ActionResult CreateComment()
		{
			return View();
		}

		[HttpGet]
		public ActionResult CreateRevisioin()
		{
			return View();
		}

		[HttpPost]
		public ActionResult CreateArticle(Article article)
		{
			if (Request.IsAuthenticated)
			{
				AddArticle(article);
			}

			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult CreateComment(ArticleComment comment)
		{
			AddComment(comment);
			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult CreateLike(ArticleLike like)
		{
			AddLike(like);
			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult CreateRevision(ArticleRevision revision)
		{
			if (Request.IsAuthenticated && User.Identity.Name == Articles[revision.ArticleId].Author)
			{
				MakeRevision(revision);
			}
			
			return RedirectToAction("Index");
		}

		public static List<Article> Articles = new List<Article> {
			new Article
			{
				Author = "amyngbedinghaus@gmail.com",
				Title = "Interesting article",
				DatePosted = DateTime.Now,
				Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vulputate euismod sodales. Cras eleifend diam in tristique mattis. Vestibulum id vulputate urna, non vulputate diam. Ut in elementum velit, ut luctus lacus. Quisque et massa lacus. Praesent dolor turpis, ultricies sit amet arcu et, tincidunt faucibus nisl. Nulla faucibus, turpis et cursus porttitor, ex ex fermentum leo, vel semper risus ligula a nisl. Ut a fringilla enim. Quisque congue sagittis mauris, vel molestie mauris eleifend a. Sed elementum tempus pretium. ",
				Comments = new List<ArticleComment>
				{
					new ArticleComment
					{
						ArticleId = 0,
						Comment = "Wow, that's so interesting!"
					}
				},
				Likes = new List<ArticleLike>()
			}
		};

		public void AddArticle(Article article)
		{
			if (String.IsNullOrEmpty(article.Body) || (String.IsNullOrEmpty(article.Title)))
			{
				throw new ArgumentNullException();
			}

			var articleToAdd = new Article();
			articleToAdd.Author = User.Identity.Name.ToString();
			articleToAdd.DatePosted = DateTime.Now;
			articleToAdd.Body = article.Body;
			articleToAdd.Title = article.Title;
			articleToAdd.Revised = false;
			if (article.Comments != null)
			{
				articleToAdd.Comments = article.Comments;
			}
			
			Articles.Add(articleToAdd);
		}

		public void AddComment(ArticleComment comment)
		{
			if (String.IsNullOrEmpty(comment.Comment))
			{
				throw new ArgumentNullException();
			}

			var index = comment.ArticleId;

			if (Articles[index].Comments == null)
			{
				Articles[index].Comments = new List<ArticleComment>();
			}

			Articles[index].Comments.Add(comment);
		}

		public void AddLike(ArticleLike like)
		{
			var index = like.ArticleId;
			if (Articles[index].Likes == null)
			{
				Articles[index].Likes = new List<ArticleLike>();
			}

			Articles[index].Likes.Add(like);
		}

		public void MakeRevision(ArticleRevision revision)
		{
			var index = revision.ArticleId;
			Articles[index].Revised = true;
			Articles[index].DateRevised = DateTime.Now;
			Articles[index].Body = revision.Revision;
		}

		public List<Article> GetArticles()
		{
			return Articles;
		}
	}
}