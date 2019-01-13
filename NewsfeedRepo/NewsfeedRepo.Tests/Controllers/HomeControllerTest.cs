using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NewsfeedRepo.Controllers;
using NewsfeedRepo.Models;
//using NewsfeedRepo.Managers;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace NewsfeedRepo.Tests.Controllers
{
	[TestClass]
	public class HomeControllerTest
	{
		private HomeController _controller;
		private List<Article> _articleList;
		private Article _article;

		private Mock<HttpContextBase> moqContext;
		private Mock<ControllerContext> moqControllerContext;
		private Mock<HttpRequestBase> moqRequest;
		private Mock<IPrincipal> moqPrincipal;

		[TestInitialize]
		public void TestInitialize()
		{
			moqContext = new Mock<HttpContextBase>();
			moqRequest = new Mock<HttpRequestBase>();
			moqPrincipal = new Mock<IPrincipal>();
			moqControllerContext = new Mock<ControllerContext>();

			moqPrincipal.Setup(p => p.IsInRole("Administrator")).Returns(true);
			moqPrincipal.SetupGet(x => x.Identity.Name).Returns("testUser");
			moqControllerContext.SetupGet(x => x.HttpContext.User).Returns(moqPrincipal.Object);

			_controller = new HomeController();
			_controller.ControllerContext = moqControllerContext.Object;
			
			_article = new Article { Author = "testUser", Body = "testBody", DatePosted = DateTime.Now, DateRevised = DateTime.Now, Title = "testTitle" };
		}

		[TestMethod]
		public void Index()
		{
			var result = _controller.Index() as ViewResult;
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void CreateArticlePost()
		{
			var result = _controller.CreateArticle(_article) as ViewResult;
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void GivenAnArticleToAdd_AddArticle_AddsArticleToArticleList()
		{
			var expected = new List<Article>() { _article };

			_controller.AddArticle(_article);

			Assert.AreEqual(expected, _articleList);
		}
	}
}
