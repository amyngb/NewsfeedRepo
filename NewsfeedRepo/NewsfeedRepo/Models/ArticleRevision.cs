using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsfeedRepo.Models
{
	public class ArticleRevision
	{
		public int ArticleId { get; set; }
		public string Revision { get; set; }
	}
}