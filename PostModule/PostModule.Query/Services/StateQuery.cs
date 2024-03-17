using Microsoft.EntityFrameworkCore;
using PostModule.Application.Contract.StateQuery;
using PostModule.Infrastracture.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Query.Services
{
    internal class StateQuery : IStateQuery
    {
        private readonly Post_Context _post_Context;
        public StateQuery(Post_Context post_Context)
        {
            _post_Context = post_Context;
        }

        public List<StateQueryModel> GetStatesWithCity() =>
            _post_Context.States.Include(s => s.Cities).Select(s => new StateQueryModel
            {
                Name = s.Title,
                Cities = s.Cities.Select(c=> new CityQueryModel
                {
                    CityCode = c.Id,
                    Name = c.Title
                }).ToList()
            }).ToList();
    }
}
