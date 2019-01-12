using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsfeedRepo.Models
{
	public class Comment
	{
		public string ArticleComment { get; set; }
		public Guid Id { get; set; }
		public Guid ArticleId { get; set; }
	}
}