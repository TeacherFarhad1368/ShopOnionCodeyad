using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Contract.UI.Site;

public interface ISiteUiQuery
{
    SitePageUiQueryModel GetSitePageQueryModel(string slug);
    ContactUsUiQueryModel GetContactUsModelForUi();
}
