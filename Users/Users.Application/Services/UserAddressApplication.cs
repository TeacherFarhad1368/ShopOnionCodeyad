﻿using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Contract.UserAddressApplication.Command;
using Users.Domain.UserAgg;
using Users.Domain.UserAgg.Repository;

namespace Users.Application.Services
{
    internal class UserAddressApplication : IUserAddressApplication
    {
        private readonly IUserAdressRepository _userAdressRepository;
        public UserAddressApplication(IUserAdressRepository userAdressRepository)
        {
            _userAdressRepository = userAdressRepository;
        }

        public OperationResult Create(CreateAddress command,int userId)
        {
            UserAddress address = new(command.StateId, command.CityId, command.AddressDetail, command.PostalCode, command.Phone,
                command.FullName, command.IranCode, userId);

            if (_userAdressRepository.Create(address))
                return new(true);

            return new(false,ValidationMessages.SystemErrorMessage, "StateId");  
        }

        public bool Delete(int id, int userId)
        {
            var address = _userAdressRepository.GetById(id);
            if(address.UserId != userId) return false;  
            return _userAdressRepository.Delete(address);
        }
    }
}
