using Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PostModule.Application.Contract.CityApplication;
using PostModule.Domain.CityEntity;
using PostModule.Domain.Services;
using Shared.Domain.Enum;
using PostModule.Domain.StateEntity;

namespace PostModule.Infrastracture.EF.Repositories
{
    internal class CityRepository : Repository<int, City>, ICityRepository
    {
        private readonly Post_Context _context;
        public CityRepository(Post_Context context) : base(context)
        {
            _context = context;
        }

		public bool ChangeStatus(int id, CityStatus status)
		{
            var city = GetById(id);
            List<City> cities = new();
            if(status == CityStatus.تهران)
            {
                 cities = _context.Cities.Where(c => c.Status == CityStatus.تهران).ToList();
            }
            else if(status == CityStatus.مرکز_استان)
            {
                cities = _context.Cities.Where(c => c.Status == CityStatus.مرکز_استان && c.StateId == city.StateId).ToList();
			}
            city.ChangeStatus(status);
            
            if(cities.Count() > 0)
			foreach (var item in cities)
			{
				item.ChangeStatus(CityStatus.شهرستان_معمولی);
			}
            return Save();
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
                Title = city.Title,
                StateId = city.StateId, 
            };
        }
    }
}
