using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Contract.SliderApplication.Query
{
    public interface ISliderQuery
    {
        List<SliderForAdmin> GetAllForAdmin();
        List<SliderForUi> GetAllForUi();
    }
}
