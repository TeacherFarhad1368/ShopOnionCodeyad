using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Query.Contract.UI.PostPackage;

public interface IPackageUiQuery
{
    PackageUiPageQueryModel GetPackagesForUi();
}
