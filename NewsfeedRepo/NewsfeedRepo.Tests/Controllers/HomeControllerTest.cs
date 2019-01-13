using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NewsfeedRepo.Controllers;
using NewsfeedRepo.Models;
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
		private Article _article0;
		private Article _article1;
		private ArticleComment _comment0;
		private ArticleComment _comment1;

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

			_comment0 = new ArticleComment { ArticleId = 0, Comment = "testComment0" };
			_comment1 = new ArticleComment { ArticleId = 0, Comment = "testComment1" };
			_article0 = new Article
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
			};
			_article1 = new Article {
				Author = "testUser",
				Body = "testBody",
				DatePosted = DateTime.Now,
				Title = "testTitle",
				Comments = new List<ArticleComment> { _comment0, _comment1},
				Likes = new List<ArticleLike> { new ArticleLike { ArticleId = 1 } }
			};
		}

		[TestMethod]
		public void GivenAnArticleToAdd_AddArticle_AddsArticleToArticleList()
		{
			var expected = new List<Article>() { _article0, _article1 };
			
			_controller.AddArticle(_article1);

			var actual = _controller.GetArticles();
			Assert.AreEqual(expected[0].Author, actual[0].Author);
			Assert.AreEqual(expected[0].Body, actual[0].Body);
			Assert.AreEqual(expected[0].Comments[0].Comment, actual[0].Comments[0].Comment);
		}

		[TestMethod]
		public void GivenACommentToAdd_AddComment_AddsCommentToArticleInArticleList()
		{
			var articles = new List<Article>() { _article0, _article1 };
			var expected = articles[1].Comments[0];

			_controller.AddComment(_comment1);

			var actualArticles = _controller.GetArticles();
			var actual = actualArticles[1].Comments[0];
			Assert.AreEqual(expected.Comment, actual.Comment);
		}

		[TestMethod]
		public void GivenALikeIsClicked_AddLike_AddsLikeToArticleInArticleList()
		{
			var like = new ArticleLike { ArticleId = 1 };
			var articles = new List<Article>() { _article0, _article1 };
			var expected = articles[1].Likes.Count;
			
			_controller.AddLike(like);

			var actualArticles = _controller.GetArticles();
			var actual = actualArticles[1].Likes.Count;
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void GivenARevisionIsSubmitted_MakeRevision_MakesRevisionToArticleInArticleList()
		{
			var revision = new ArticleRevision { ArticleId = 0, Revision = "testRevision" };
			
			_controller.MakeRevision(revision);

			var actualArticles = _controller.GetArticles();

			Assert.AreEqual(true, actualArticles[0].Revised);
			Assert.AreEqual(revision.Revision, actualArticles[0].Body);
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
