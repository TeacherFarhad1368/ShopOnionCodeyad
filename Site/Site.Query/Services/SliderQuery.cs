using Shared.Application;
using Site.Application.Contract.SliderApplication.Query;
using Site.Domain.SliderAgg;

namespace Site.Query.Services;

internal class SliderQuery : ISliderQuery
{
    private readonly ISliderRepository _sliderRepository;

    public SliderQuery(ISliderRepository sliderRepository)
    {
        _sliderRepository = sliderRepository;
    }

    public List<SliderForAdmin> GetAllForAdmin()
    {
        return _sliderRepository.GetAllQuery().Select(s => new SliderForAdmin
        {
            Active = s.Active,
            ImageAlt = s.ImageAlt,
            CreationDate = s.CreateDate.ToPersainDate(),
            Id = s.Id,
            ImageName = FileDirectories.SliderImageDirectory100 + s.ImageName
        }).ToList();

    }

    public List<SliderForUi> GetAllForUi()
    {
        return _sliderRepository.GetAllByQuery(s => s.Active)
            .Select(s => new SliderForUi()
            {
                ImageAlt = s.ImageAlt,
                ImageName = FileDirectories.SliderImageDirectory + s.ImageName,
                Url = s.Url
            }).ToList();
    }
}
