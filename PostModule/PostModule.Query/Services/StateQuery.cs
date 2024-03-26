using Microsoft.EntityFrameworkCore;
using PostModule.Application.Contract.StateQuery;
using PostModule.Infrastracture.EF;
using Shared.Application;
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

		public StateDetailQueryModel GetStateDetail(int id)
		{
            var state = _post_Context.States.Find(id);
            StateDetailQueryModel model = new()
            {
                Name = state.Title,
                Id =state.Id,
                CloseStates = state.CloseStates,
                States = new(),
                Cities = new(),
            };
            model.States = _post_Context.States.Select(s => new StateForAddStateClosesQueryModel
            {
                Id = s.Id,
                title = s.Title
            }).ToList();
            model.Cities = _post_Context.Cities.Where(c => c.StateId == state.Id)
                .Select(c => new CityAdminQueryModel
                {
                    CreationDate = c.CreateDate.ToPersainDate(),
                    Id = c.Id,
                    Status = c.Status,
                    Title = c.Title
                }).ToList();
            return model;   
		}

		public List<StateAdminQueryModel> GetStatesForAdmin() =>
			_post_Context.States.Include(s => s.Cities).Select(s => new StateAdminQueryModel
			{
				Id = s.Id,
                Title = s.Title,
                CreateDate = s.CreateDate.ToPersainDate(),
                CityCount = s.Cities.Count()
			}).ToList();

		public List<StateQueryModel> GetStatesWithCity() =>
            _post_Context.States.Include(s => s.Cities).Select(s => new StateQueryModel
            {
                Name = s.Title,
                Cities = s.Cities.Select(c=> new CityQueryModel
                {
                    CityCode = c.Id,
                    Name = c.Title,
                }).ToList()
            }).ToList();
    }
}
