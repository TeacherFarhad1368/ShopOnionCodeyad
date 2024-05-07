using PostModule.Domain.Services;
using Query.Contract.UserPanel.Address;
using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.UserAgg.Repository;

namespace Query.Services.UserPanel
{
    internal class UserAddressUserPanelQuery : IUserAddressUserPanelQuery
    {
        private readonly IUserAdressRepository _userAdressRepository;
        private readonly IStateRepository _stateRepository;
        private readonly ICityRepository _cityRepository;

        public UserAddressUserPanelQuery(IUserAdressRepository userAdressRepository, IStateRepository stateRepository, ICityRepository cityRepository)
        {
            _userAdressRepository = userAdressRepository;
            _stateRepository = stateRepository;
            _cityRepository = cityRepository;
        }

        public List<UserAddressForPanelQueryModel> GetAddresseForUserPanel(int userId)
        {
            var addresses = _userAdressRepository.GetAllByQuery(a => a.UserId == userId).OrderByDescending(a => a.Id)
                .Select(a => new UserAddressForPanelQueryModel
                {
                    AddressDetail = a.AddressDetail,
                    CityId = a.CityId,
                    CityName = "",
                    CreationDate = a.CreateDate.ToPersainDate(),
                    FullName = a.FullName,
                    IranCode = a.IranCode,
                    Phone = a.Phone,
                    PostalCode = a.PostalCode,
                    StateId = a.StateId,
                    StateName = ""
                }).ToList();

            addresses.ForEach(x =>
            {
                var city = _cityRepository.GetById(x.CityId);
                var state = _stateRepository.GetById(x.StateId);
                x.CityName = city.Title;
                x.StateName = state.Title;
            });
            return addresses;

        }
    }
}
