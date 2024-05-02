using Shared.Application;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Application.Contract.UserPostApplication.Command;

public interface IPackageApplication
{
    OperationResult Create(CreatePackage command);
    OperationResult Edit(EditPackage command);
    EditPackage GetForEdit(int id);
    bool ActivationChange(int id);

}
