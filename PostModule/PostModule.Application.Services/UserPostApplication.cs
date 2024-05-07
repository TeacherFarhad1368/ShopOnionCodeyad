using PostModule.Application.Contract.UserPostApplication.Command;
using PostModule.Domain.SettingAgg;
using PostModule.Domain.UserPostAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PostModule.Application.Services
{
    internal class UserPostApplication : IUserPostApplication
    {
        private readonly IPOstOrderRepository _pOstOrderRepository;
        private readonly IPackageRepository _packageRepository;
        private readonly IUserPostRepository _userPostRepository;
        private readonly IPostSettingRepository _postSettingRepository;
        public UserPostApplication(IPOstOrderRepository pOstOrderRepository, 
            IPackageRepository packageRepository, IUserPostRepository userPostRepository,
            IPostSettingRepository postSettingRepository)
        {
            _pOstOrderRepository = pOstOrderRepository;
            _packageRepository = packageRepository;
            _userPostRepository = userPostRepository;
            _postSettingRepository = postSettingRepository;
        }

        public async Task<bool> CreatePostOrderAsync(CreatePostOrder command)
        {
            var postOrder = await _pOstOrderRepository.GetPostOrderNotPaymentForUserAsync(command.UserId);
            if(postOrder == null)
            {
                postOrder = new(command.PackageId, command.UserId, command.Price);
                return await Task.Run(() => _pOstOrderRepository.Create(postOrder));
            }
            else
            {
                if(command.PackageId != postOrder.PackageId || command.Price != postOrder.Price)
                {
                    postOrder.Edit(command.PackageId, command.Price);
                    return await Task.Run(() => _pOstOrderRepository.Save());
                }
                return true;
            }
        }

        public async Task<CreatePostOrder> GetCreatePostModelAsync(int userId, int packageId) =>
            await _packageRepository.GetCreatePostModelAsync(userId, packageId);

        public async Task<PostOrderUserPanelModel> GetPostOrderNotPaymentForUser(int userId) =>
            await _pOstOrderRepository.GetPostOrderNotPaymentForUser(userId);

        public async Task<UserPostPanelModel> GetUserPostModelForPanel(int userId)
        {
            UserPost userPost = await _userPostRepository.GetForUser(userId);
            var setting = _postSettingRepository.GetSingle();
            return new UserPostPanelModel(setting.ApiDescription, userPost.Count, userPost.ApiCode);
        }

        public async Task<bool> PaymentPostOrderAsync(PaymentPostModel command)
        {
            var postOrder = await _pOstOrderRepository.GetPostOrderNotPaymentForUserAsync(command.UserId);
            if (postOrder == null || postOrder.Price != command.Price ||
                postOrder.Status == Shared.Domain.Enum.PostOrderStatus.پرداخت_شده ||
                postOrder.UserId != command.UserId) return false;
            postOrder.SuccessPayment(command.TransactionId);
            UserPost userPost = await _userPostRepository.GetForUser(command.UserId);
            var package = _packageRepository.GetById(postOrder.PackageId);
            userPost.CountPlus(package.Count);
            return _userPostRepository.Save();
        }
    }
   
}
