using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
	}
}