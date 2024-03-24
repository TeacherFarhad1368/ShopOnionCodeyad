using Shared.Application;
using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Application.Contract.CityApplication
{
    public interface ICityApplication
    {
        OperationResult Create(CreateCityModel command);
        OperationResult Edit(EditCityModel command);
        bool ExistTitleForCreate(string title, int stateid);
        bool ExistTitleForEdit(string title , int id, int stateid);
        EditCityModel GetCityForEdit(int id);
        List<CityViewModel> GetAllForState(int stateId);
        bool ChangeStatus(int id, CityStatus status);
    }
}
