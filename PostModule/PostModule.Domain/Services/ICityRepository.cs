using Shared.Domain;
using PostModule.Application.Contract.CityApplication;
using PostModule.Domain.CityEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Domain.Services
{
    public interface ICityRepository : IRepository<int, City>
    {
        List<CityViewModel> GetAllForState(int stateId);
        EditCityModel GetCityForEdit(int id);
    }
}
