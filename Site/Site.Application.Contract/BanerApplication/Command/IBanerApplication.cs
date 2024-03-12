using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Contract.BanerApplication.Command
{
    public interface IBanerApplication
    {
        OperationResult Create(CreateBaner command);
        OperationResult Edit(EditBaner command);
        bool ActivationChange(int id);
        EditBaner GetForEdit(int id);
    }
}
