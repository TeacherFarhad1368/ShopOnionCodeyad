using PostModule.Application.Contract.CityApplication;
using PostModule.Domain.CityEntity;
using PostModule.Domain.Services;
using Shared.Application;
using Shared.Domain.Enum;
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

		public bool ChangeStatus(int id, CityStatus status)=>
            _cityRepository.ChangeStatus(id, status);   

		public OperationResult Create(CreateCityModel command)
        {
            if(_cityRepository.ExistBy(c=>c.Title == command.Title && c.StateId == command.StateId))
                return new(false,ValidationMessages.DuplicatedMessage,nameof(command.Title));
            City city = new(command.StateId, command.Title, CityStatus.شهرستان_معمولی);
            if (_cityRepository.Create(city))
            {
				return new(true);
			}
            return new(false, ValidationMessages.SystemErrorMessage, nameof(command.Title));
        }

        public OperationResult Edit(EditCityModel command)
        {
			if (_cityRepository.ExistBy(c => c.Title == command.Title && c.StateId == command.StateId && c.Id != command.Id))
				return new(false, ValidationMessages.DuplicatedMessage, nameof(command.Title));
			City city = _cityRepository.GetById(command.Id);
            city.Edit(command.Title, city.Status);
			if (_cityRepository.Save()) return new(true);
            return new(false, ValidationMessages.SystemErrorMessage, nameof(command.Title));
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
