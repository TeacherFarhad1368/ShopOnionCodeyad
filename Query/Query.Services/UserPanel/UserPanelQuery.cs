using Query.Contract.UserPanel.User;
using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.UserAgg.Repository;

namespace Query.Services.UserPanel
{
    internal class UserPanelQuery : IUserPanelQuery
    {
        private readonly IUserRepository _userRepository;

        public UserPanelQuery(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserInfoForPanelQueryModel GetUserInfoForPanel(int userId)
        {
            var user = _userRepository.GetById(userId);
            return new UserInfoForPanelQueryModel(user.FullName, user.Mobile, 
                user.Email, user.UserGender, 0, 0, user.CreateDate.ToPersainDate());
        }
    }
}
