using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Contract.SitePageApplication.Query
{
	public interface ISitePageQuery
	{
		List<SitePageAdminQueryModel> GetAllForAdmin();
	}
}
