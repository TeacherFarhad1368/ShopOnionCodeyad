using Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PostModule.Application.Contract.CityApplication;
using PostModule.Domain.CityEntity;
using PostModule.Domain.Services;

namespace PostModule.Infrastracture.EF.Repositories
{
    internal class CityRepository : Repository<int, City>, ICityRepository
    {
        private readonly Post_Context _context;
        public CityRepository(Post_Context context) : base(context)
        {
            _context = context;
        }

        public List<CityViewModel> GetAllForState(int stateId)
        {
            return GetAllByQuery(c => c.StateId == stateId).Select(c => new CityViewModel
            {
                CreateDate=c.CreateDate.ToString(),
                Id=c.Id,
                Status=c.Status,
                Title=c.Title
            }).ToList();
        }

        public EditCityModel GetCityForEdit(int id)
        {
            var city = GetById(id);
            return new()
            {
                Id=city.Id,
                Status=city.Status,
                Title = city.Title
            };
        }
    }
}
