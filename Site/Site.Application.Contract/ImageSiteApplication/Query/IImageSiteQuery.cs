﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Contract.ImageSiteApplication.Query
{
	public interface IImageSiteQuery
	{
		List<ImageSiteAdminQueryModel> GetAllForAdmin();
	}
}
