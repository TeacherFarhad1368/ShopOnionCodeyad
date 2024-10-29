using Microsoft.EntityFrameworkCore.Storage;
using PostModule.Domain.Services;
using Query.Contract.UserPanel.Seller;
using Shared.Application;
using Shop.Domain.SellerAgg;

namespace Query.Services.UserPanel;
internal class SellerUserPanelQuery : ISellerUserPanelQuery
{
    private readonly ISellerRepository _sellerRepository;
    private readonly IStateRepository _stateRepository; 
    private readonly ICityRepository _cityRepository;

    public SellerUserPanelQuery(ISellerRepository sellerRepository, IStateRepository stateRepository, ICityRepository cityRepository)
    {
        _sellerRepository = sellerRepository;
        _stateRepository = stateRepository;
        _cityRepository = cityRepository;
    }

    public List<SellersUserPanelQueryModel> GetSellersForUser(int userId)
    {
        var res = _sellerRepository.GetAllByQuery(c => c.UserId == userId);
        List<SellersUserPanelQueryModel> model =  res.Select(r => new SellersUserPanelQueryModel
        {
            ImageAccept = r.ImageAccept,
            CityId = r.CityId,
            CityName = "",
            CreationDate = r.CreateDate.ToPersainDate(),
            Id = r.Id,
            ImageName = r.ImageName,
            Phone1 = r.Phone1,
            StateId = r.StateId,
            Status = r.Status,
            Title = r.Title
        }).ToList();
        model.ForEach(x =>
        {
            var state = _stateRepository.GetById(x.StateId);
            var city = _cityRepository.GetById(x.CityId);
            x.CityName = $"{state.Title} - {city.Title}";
        });
        return model;
    }
}
