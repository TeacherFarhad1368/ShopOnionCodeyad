using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Application.Contract.StateApplication
{
    public interface IStateApplication
    {
        OperationResult Create(CreateStateModel command);
        OperationResult Edit(EditStateModel command);
        List<StateViewModel> GetAll();
        EditStateModel GetStateForEdit(int id);
        bool ExistTitleForCreate(string title);
        bool ExistTitleForEdit(string title , int id);
		bool ChangeStateClose(int id, List<int> stateCloses);
	}
}
