using PostModule.Domain.UserPostAgg;
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
        private readonly IPOstOrderRepository _pOstOrderRepository;
        public UserPanelQuery(IUserRepository userRepository, IPOstOrderRepository pOstOrderRepository)
        {
            _userRepository = userRepository;
            _pOstOrderRepository = pOstOrderRepository; 
        }

        public UserInfoForPanelQueryModel GetUserInfoForPanel(int userId)
        {
            var user = _userRepository.GetById(userId);
            return new UserInfoForPanelQueryModel(user.FullName, user.Mobile, 
                user.Email, user.UserGender, 0, 0, user.CreateDate.ToPersainDate());
        }

        public UserPanelSideBarQueryModel GetUserPanelSideBarModel(int userId)
        {
            bool haveOrderPost = _pOstOrderRepository.ExistBy(o => o.UserId == userId);
            return new()
            {
                HaveUserOrderPost = haveOrderPost
            };
        }
    }
}
