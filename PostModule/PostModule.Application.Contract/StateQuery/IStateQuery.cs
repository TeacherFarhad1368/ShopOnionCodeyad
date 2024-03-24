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
        List<StateQueryModel> GetStatesWithCity();
        List<StateAdminQueryModel> GetStatesForAdmin();
		StateDetailQueryModel GetStateDetail(int id);
	}
}
