using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Contract.MenuApplication.Command
{
    public interface IMenuApplication
    {
        OperationResult Create(CreateMenu command);
        OperationResult Edit(EditMenu command);
        EditMenu GetForEdit(int id);
        bool ActivationChange(int id);
    }
}
