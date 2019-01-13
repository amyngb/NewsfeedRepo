using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NewsfeedRepo.Controllers;
//using NewsfeedRepo.Managers;
using NewsfeedRepo.Models;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;

namespace NewsfeedRepo.Tests.Controllers
{
	[TestClass]
	public class ArticleManagerTest
	{
		private ArticleManager _articleManager;
		private List<Article> _articleList;
		private Article _article;

		private Mock<HttpContextBase> moqContext;
		private Mock<HttpRequestBase> moqRequest;
		private Mock<IPrincipal> moqUser;

		[TestInitialize]
		public void TestInitialize()
		{
			moqContext = new Mock<HttpContextBase>();
			moqRequest = new Mock<HttpRequestBase>();
			moqUser = new Mock<IPrincipal>();
			
			moqContext.Setup(x => x.User).Returns(moqUser.Object);
			moqContext.Setup(x => x.User.Identity.Name).Returns("testUser");

			_articleManager = new ArticleManager();
			_articleList = new List<Article>();
			_article = new Article { Author = "testUser", Body = "testBody", DatePosted = DateTime.Now, DateRevised = DateTime.Now, Title = "testTitle" };

		}

		[TestMethod]
		public void GivenAnArticleToAdd_AddArticle_AddsArticleToArticleList()
		{
			var expected = new List<Article>() { _article };

			_articleManager.AddArticle(_article);

			Assert.AreEqual(expected, _articleList);


		}
	}
}
