using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Application.Contract.CityApplication
{
    public interface ICityApplication
    {
        bool Create(CreateCityModel command);
        bool Edit(EditCityModel command);
        bool ExistTitleForCreate(string title, int stateid);
        bool ExistTitleForEdit(string title , int id, int stateid);
        EditCityModel GetCityForEdit(int id);
        List<CityViewModel> GetAllForState(int stateId);
    }
}
