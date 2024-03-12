using Site.Application.Contract.SliderApplication.Query;

namespace Site.Query.Services;

internal class SliderQuery : ISliderQuery
{
    public List<SliderForAdmin> GetAllForAdmin()
    {
        throw new NotImplementedException();
    }

    public List<SliderForUi> GetAllForUi()
    {
        throw new NotImplementedException();
    }
}
