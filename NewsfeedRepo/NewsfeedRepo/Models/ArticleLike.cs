﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsfeedRepo.Models
{
	public class ArticleLike
	{
		public bool Like { get; set; }
		public int ArticleId { get; set; }
	}
}