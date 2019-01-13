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
		private Article _article;
		private ArticleComment _comment1;
		private ArticleComment _comment2;

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

			_comment1 = new ArticleComment { ArticleId = 0, Comment = "testComment1" };
			_comment2 = new ArticleComment { ArticleId = 0, Comment = "testComment2" };
			_article = new Article {
				Author = "testUser",
				Body = "testBody",
				DatePosted = DateTime.Now,
				Title = "testTitle",
				Comments = new List<ArticleComment> { _comment1, _comment2} };
		}

		[TestMethod]
		public void GivenAnArticleToAdd_AddArticle_AddsArticleToArticleList()
		{
			var expected = new List<Article>() { _article };
			
			_controller.AddArticle(_article);

			var actual = _controller.GetArticles();

			Assert.AreEqual(expected[0].Author, actual[0].Author);
			Assert.AreEqual(expected[0].Body, actual[0].Body);
			CollectionAssert.AreEqual(expected[0].Comments, actual[0].Comments);
		}

		[TestMethod]
		public void GivenACommentToAdd_AddComment_AddsCommentToArticleInArticleList()
		{
			var articles = new List<Article>() { _article };
			var expected = articles[0].Comments[0];


			_controller.AddComment(_comment1);

			var actualArticles = _controller.GetArticles();
			var actual = actualArticles[0].Comments[0];


			Assert.AreEqual(expected.Comment, actual.Comment);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GivenEmptyStringArticleTitle_AddArticle_ThrowsException()
		{
			var emptyArticle = new Article();
			_controller.AddArticle(emptyArticle);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GivenEmptyStringArticleBody_AddArticle_ThrowsException()
		{
			var emptyArticle = new Article();
			_controller.AddArticle(emptyArticle);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GivenEmptyStringComment_AddComment_ThrowsException()
		{
			var emptyComment = new ArticleComment();
			_controller.AddComment(emptyComment);
		}
	}
}
