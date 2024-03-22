using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Contract.ImageSiteApplication.Query
{
	public interface IImageSiteQuery
	{
		ImageAdminPaging GetAllForAdmin(int pageId,int take,string filter);
	}
}
