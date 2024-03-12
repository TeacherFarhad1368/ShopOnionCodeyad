using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Contract.SiteSettingApplication.Command
{
    public interface ISiteSettingApplication
    {
        OperationResult Ubsert(UbsertSiteSetting command);
        UbsertSiteSetting GetForUbsert();
    }
}
