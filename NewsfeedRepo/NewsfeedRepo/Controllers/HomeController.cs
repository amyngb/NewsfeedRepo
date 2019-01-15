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

			if (Articles[comment.ArticleId].Comments == null)
			{
				Articles[comment.ArticleId].Comments = new List<ArticleComment>();
			}

			Articles[comment.ArticleId].Comments.Add(comment);
		}

		public void AddLike(ArticleLike like)
		{
			if (Articles[like.ArticleId].Likes == null)
			{
				Articles[like.ArticleId].Likes = new List<ArticleLike>();
			}

			Articles[like.ArticleId].Likes.Add(like);
		}

		public void MakeRevision(ArticleRevision revision)
		{
			Articles[revision.ArticleId].Revised = true;
			Articles[revision.ArticleId].DateRevised = DateTime.Now;
			Articles[revision.ArticleId].Body = revision.Revision;
		}

		public List<Article> GetArticles()
		{
			return Articles;
		}
	}
}