using PostModule.Domain.UserPostAgg;
using Query.Contract.UserPanel.PostOrder;
using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Services.UserPanel
{
    internal class PostOrderUserPanelQuery : IPostOrderUserPanelQuery
    {
        // generate transaction Repo
        private readonly IPOstOrderRepository _pOstOrderRepository;
        private readonly IPackageRepository _packageRepository;
        public PostOrderUserPanelQuery(IPOstOrderRepository pOstOrderRepository, IPackageRepository packageRepository)
        {
            _pOstOrderRepository = pOstOrderRepository;
            _packageRepository = packageRepository;
        }

        public PostOrderUserPanelPaging GetPostOrdersForUsePanel(int pageId, int userId)
        {
            IQueryable<PostOrder> res = _pOstOrderRepository.GetAllByQuery(b=>b.UserId == userId)
                .OrderByDescending(o => o.Id);
            PostOrderUserPanelPaging model = new();
            model.GetData(res, pageId,10,1);
            model.Orders = new();
            if(res.Count() > 0)
            {
                model.Orders = res.Skip(model.Skip).Take(model.Take).Select(o => new PostOrderUserPanelQueryModel()
                    {
                    Count = 0,
                    Date = o.CreateDate.ToPersainDate(),
                    Id = o.Id,
                    PackageId = o.PackageId,
                    PackageImage = "",
                    PackageTitle = "",
                    Price = o.Price,
                    Status = o.Status,
                    transactionId = o.TransactionId,
                    TransactionRef = ""
                    }).OrderByDescending(o => o.Id).ToList();


                model.Orders.ForEach(x =>
                {
                    var package = _packageRepository.GetById(x.PackageId);
                    x.Count = package.Count;
                    x.PackageImage = $"{FileDirectories.PackageImageDirectory400}{package.ImageName}";
                    x.PackageTitle = package.Title;

                    // set transaction REF
                });
            }
            return model;
        }
    }
}
