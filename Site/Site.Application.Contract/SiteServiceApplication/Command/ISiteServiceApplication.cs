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
        OperationResult Create(CreateSiteService commmand);
        OperationResult Edit(EditSiteService commmand);
        bool ActivationChange(int id);
        EditSiteService GetForEdit(int id);
    }
}
