using Shared.Application;
using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Application.Contract.CityApplication
{
	public class CreateCityModel
    {
        public int StateId { get; set; }
		[Display(Name = "نام شهر")]
		[Required(ErrorMessage = ValidationMessages.RequiredMessage)]
		[MaxLength(250, ErrorMessage = ValidationMessages.MaxLengthMessage)]
		public string Title { get; set; }
    }
}
