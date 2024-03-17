using Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PostModule.Application.Contract.StateApplication;
using PostModule.Domain.Services;
using PostModule.Domain.StateEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Infrastracture.EF.Repositories
{
    internal class StateRepository : Repository<int, State>, IStateRepository
    {
        private readonly Post_Context _context;
        public StateRepository(Post_Context context) : base(context)
        {
            _context = context;
        }

        public List<StateViewModel> GetAllStateViewModel()
        {
            return GetAllQuery().Select(s => new StateViewModel { 
                CreateDate=s.CreateDate.ToString(),
                Id=s.Id,
                Title=s.Title

            }).ToList();

        }

        public EditStateModel GetStateForEdit(int id)
        {
            var state = GetById(id);
            return new()
            {
                Id=state.Id,
                Title=state.Title
            };
        }
    }
}
