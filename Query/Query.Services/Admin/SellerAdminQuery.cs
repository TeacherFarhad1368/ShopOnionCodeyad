using PostModule.Infrastracture.EF;
using Query.Contract.Admin.Seller;
using Shared.Application;
using Shop.Infrastructure;
using Users.Infrastructure;

namespace Query.Services.Admin;
internal class SellerAdminQuery : ISellerAdminQuery
{
    private readonly ShopContext _shopContext;
    private readonly UserContext _userContext;   
    private readonly Post_Context _postContext;

    public SellerAdminQuery(ShopContext shopContext, UserContext userContext, Post_Context postContext)
    {
        _shopContext = shopContext;
        _userContext = userContext;
        _postContext = postContext;
    }

    public SellerDetailAdminQueryModel GetSellerDetailForAdmin(int id)
    {
        var s = _shopContext.Sellers.SingleOrDefault(s => s.Id == id);
        if (s == null) return null;
        SellerDetailAdminQueryModel model = new()
        {
            Address = s.Address,
            ImageAccept = s.ImageAccept,
            ImageAlt = s.ImageAlt,
            WhatsApp = s.WhatsApp,
            CityId = s.CityId,
            CityName = "",
            CreateDate = s.CreateDate.ToPersainDate(),
            Email = s.Email,
            GoogleMapUrl = s.GoogleMapUrl,
            Id = id,
            ImageName = s.ImageName,
            Instagram = s.Instagram,
            Phone1 = s.Phone1,
            Phone2 = s.Phone2,
            StateId = s.StateId,
            Status = s.Status,
            Telegram = s.Telegram,
            Title = s.Title,
            UpdateDate = s.UpdateDate.ToPersainDate(),
            UserId = s.UserId,
            UserName = ""
        };
        var user = _userContext.Users.SingleOrDefault(u => u.Id == s.UserId);
        if (user == null) return null;
        model.UserName = string.IsNullOrEmpty(user.FullName) ? user.Mobile : user.FullName;
        var state = _postContext.States.SingleOrDefault(c => c.Id == s.StateId);
        if (state == null) return null;
        var city = _postContext.Cities.SingleOrDefault(c => c.Id == s.CityId);
        if (city == null) return null;
        model.CityName = $"{state.Title} {city.Title}";
        return model;
    }

    public SellerRequestDetailAdminQueryModel GetSellerRequestDetailForAdmin(int id)
    {
        var s = _shopContext.Sellers.SingleOrDefault(s=>s.Id == id);
        if (s == null) return null;
        SellerRequestDetailAdminQueryModel model = new()
        {
            Address = s.Address,
            ImageAccept = s.ImageAccept,
            ImageAlt = s.ImageAlt,
            WhatsApp = s.WhatsApp,
            CityId = s.CityId,
            CityName = "",
            CreateDate = s.CreateDate.ToPersainDate(),
            Email = s.Email,
            GoogleMapUrl = s.GoogleMapUrl,
            Id = id,
            ImageName = s.ImageName,
            Instagram = s.Instagram,
            Phone1 = s.Phone1,
            Phone2 = s.Phone2,
            StateId = s.StateId,
            Status = s.Status,
            Telegram = s.Telegram,
            Title = s.Title,
            UpdateDate = s.UpdateDate.ToPersainDate(),
            UserId = s.UserId,
            UserName = ""
        };
        var user = _userContext.Users.SingleOrDefault(u => u.Id == s.UserId);
        if (user == null) return null;
        model.UserName =string.IsNullOrEmpty(user.FullName) ? user.Mobile : user.FullName;
        var state = _postContext.States.SingleOrDefault(c => c.Id == s.StateId);
        if (state == null) return null;
        var city = _postContext.Cities.SingleOrDefault(c => c.Id == s.CityId);
        if (city == null) return null;
        model.CityName = $"{state.Title} {city.Title}";
        return model;
    }

    public List<SellerRequestAdminQueryModel> GetSellerRequestsForAdmin()
    {
        var res = _shopContext.Sellers.Where(s => s.Status == Shared.Domain.Enum.SellerStatus.درخواست_ارسال_شده);
        var model = res.Select(s => new SellerRequestAdminQueryModel
        {
            ImageAccept = s.ImageAccept,
            CityId = s.CityId,
            CityName = "",
            CreateDate = s.CreateDate.ToPersainDate(),
            Email = s.Email,
            Id = s.Id,
            ImageName = s.ImageName,
            Phone1 = s.Phone1,
            StateId = s.StateId,
            Title = s.Title,
            UpdateDate = s.UpdateDate.ToPersainDate(),
            UserId = s.UserId,
            UserName = ""
        }).ToList();
        model.ForEach(s =>
        {
            var user = _userContext.Users.SingleOrDefault(u => u.Id == s.UserId);
            if (user != null)
            s.UserName = string.IsNullOrEmpty(user.FullName) ? user.Mobile : user.FullName;
            var state = _postContext.States.SingleOrDefault(c => c.Id == s.StateId);
            if (state != null)
                s.CityName = state.Title;
            var city = _postContext.Cities.SingleOrDefault(c => c.Id == s.CityId);
            if (city != null)
            s.CityName = s.CityName + " " + city.Title;
        });
        return model;
    }

    public SellerAdminPaging GetSellersForAdmin(int pageId, int take, string filter)
    {
        var res = _shopContext.Sellers.Where(s => s.Status == Shared.Domain.Enum.SellerStatus.درخواست_تایید_شده);
        if(!string.IsNullOrEmpty(filter))
            res = res.Where(r=> r.Title.ToLower() == filter.ToLower());
        SellerAdminPaging model = new();
        model.GetData(res, pageId, take, 2);
        model.Filter = filter;
        model.Sellers = new();
        if(res.Count() > 0)
        {
            model.Sellers = res.Select(s => new SellerAdminQueryModel
            {
                CityId = s.CityId,
                CityName = "",
                CreateDate = s.CreateDate.ToPersainDate(),
                Email = s.Email,
                Id = s.Id,
                ImageName = s.ImageName,
                Phone1 = s.Phone1,
                StateId = s.StateId,
                Title = s.Title,
                UpdateDate = s.UpdateDate.ToPersainDate(),
                UserId = s.UserId,
                UserName = ""
            }).ToList();
            model.Sellers.ForEach(s =>
            {
                var user = _userContext.Users.SingleOrDefault(u => u.Id == s.UserId);
                if (user != null)
                    s.UserName = string.IsNullOrEmpty(user.FullName) ? user.Mobile : user.FullName;
                var state = _postContext.States.SingleOrDefault(c => c.Id == s.StateId);
                if (state != null)
                    s.CityName = state.Title;
                var city = _postContext.Cities.SingleOrDefault(c => c.Id == s.CityId);
                if (city != null)
                    s.CityName = s.CityName + " " + city.Title;
            });
        }
        return model;   
    }
}
