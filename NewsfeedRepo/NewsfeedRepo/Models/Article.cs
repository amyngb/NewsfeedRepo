using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NewsfeedRepo.Models
{
	public class Article
	{
		[Required]
		public string Title { get; set; }
		[Required]
		public string Author { get; set; }
		public string Body { get; set; }
		public DateTime DatePosted { get; set; }
		public DateTime DateRevised { get; set; }
		public List<ArticleComment> Comments { get; set; }
		public List<ArticleLike> Likes { get; set; }
		public bool Revised { get; set; }
	}
}