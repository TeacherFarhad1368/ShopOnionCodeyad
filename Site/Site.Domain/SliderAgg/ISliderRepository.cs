using Shared.Domain;
using Site.Application.Contract.SliderApplication.Command;

namespace Site.Domain.SliderAgg
{
    public interface ISliderRepository : IRepository<int, Slider>
    {
        EditSlider GetForEdit(int id);
    }
}
