using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Application.Contract.StateQuery
{
    public interface IStateQuery
    {
        Task<List<StateQueryModel>> GetStatesWithCity();
        List<StateAdminQueryModel> GetStatesForAdmin();
		StateDetailQueryModel GetStateDetail(int id);
        string GetStateTitle(int id);
        List<StateForChooseQueryModel> GetStatesForChoose();
        List<CityForChooseQueryModel> GetCitiesForChoose(int stateId);
        bool IsStateCorrect(int stateId);
        bool IsCityCorrect(int stateId, int cityId);
    }
}
