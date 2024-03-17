using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Application.Contract.CityApplication
{
	public class CreateCityModel
    {
        public int StateId { get; set; }
        public string Title { get; set; }
        public CityStatus Status { get; set; }
    }
}
