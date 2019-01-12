using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewsfeedRepo.Controllers;
using System;
using System.Web.Mvc;

namespace NewsfeedRepo.Tests.Controllers
{
	[TestClass]
	public class HomeControllerTest
	{
		HomeController _controller;
		Models.Article _article;
		
		[TestInitialize]
		public void TestInitialize()
		{
			_controller = new HomeController();
			_article = new Models.Article { Author = "testUser", Body = "testBody", DatePosted = DateTime.Now, DateRevised = DateTime.Now, Title = "testTitle" };


		}

		[TestMethod]
		public void Index()
		{
			var result = _controller.Index() as ViewResult;
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void CreateArticleGet()
		{
			var result = _controller.CreateArticle() as ViewResult;
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void CreateArticlePost()
		{
			var result = _controller.CreateArticle(_article) as ViewResult;
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void IsUserSignedIn()
		{
			
		}
	}
}
