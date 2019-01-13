using Microsoft.AspNet.Identity;
using NewsfeedRepo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsfeedRepo.Controllers
{
	public class ArticleManager
	{
		public void AddArticle(Article article)
		{
			var articleToAdd = new Article();
			articleToAdd.Author = GetUser();
			articleToAdd.DatePosted = DateTime.Now;
			articleToAdd.Body = article.Body;
			articleToAdd.Title = article.Title;

			Articles.Add(article);
		}

		public List<Article> Articles = new List<Article>();

		private string GetUser()
		{
			return HttpContext.Current.User.Identity.Name.ToString();
		}
	}
}