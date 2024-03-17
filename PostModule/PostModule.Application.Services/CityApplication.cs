using PostModule.Application.Contract.CityApplication;
using PostModule.Domain.CityEntity;
using PostModule.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Application.Services
{
    internal class CityApplication : ICityApplication
    {
        private readonly ICityRepository _cityRepository;
        public CityApplication(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        public bool Create(CreateCityModel command)
        {
            City city = new(command.StateId, command.Title, command.Status);
            return _cityRepository.Create(city);
        }

        public bool Edit(EditCityModel command)
        {
            var city = _cityRepository.GetById(command.Id);
            city.Edit(command.Title,command.Status);
            return _cityRepository.Save();
        }

        public bool ExistTitleForCreate(string title , int stateid) =>
            _cityRepository.ExistBy(c => c.Title == title && c.StateId == stateid);

        public bool ExistTitleForEdit(string title, int id, int stateid) =>
            _cityRepository.ExistBy(c => c.Title == title && c.StateId == stateid && c.Id != id);

        public List<CityViewModel> GetAllForState(int stateId) => 
            _cityRepository.GetAllForState(stateId);

        public EditCityModel GetCityForEdit(int id) =>
            _cityRepository.GetCityForEdit(id);
    }
}
