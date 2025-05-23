using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Shared.Caching;
using Site.Application.Contract.SliderApplication.Query;
using System.Threading.Tasks;

namespace ShopBoloor.WebApplication.ViewComponents;

public class SliderViewComponent : ViewComponent
{
    private readonly ISliderQuery _query;
    private readonly IMemoryCache _memoryCache;
    private readonly string _cacheKey = "Index_Slider";
    public SliderViewComponent(ISliderQuery query, IMemoryCache memoryCache )
    {
        _query = query;
        _memoryCache = memoryCache;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = await _memoryCache.GetFromMemoryAsync<List<SliderForUi>>(_cacheKey);
        if(model == null)
        {
            model = _query.GetAllForUi();
            _memoryCache.SetInMemory<List<SliderForUi>>(_cacheKey, model,TimeSpan.FromMinutes(2),5);
        }
        return View(model);
    }
}
