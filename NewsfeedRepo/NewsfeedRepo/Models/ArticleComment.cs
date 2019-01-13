using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace NewsfeedRepo.Models
{
	public class ArticleComment
	{		
		public string Comment { get; set; }
		public int ArticleId { get; set; }
	}
}