using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Contract.SiteServiceApplication.Command
{
    public interface ISiteServiceApplication
    {
        OperationResult Ubsert(UbsertSiteSetting command);
        UbsertSiteSetting GetForUbsert();
    }
}
