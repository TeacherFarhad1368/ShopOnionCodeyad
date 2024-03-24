using Shared.Domain.Enum;

namespace PostModule.Application.Contract.CityApplication
{
	public class EditCityModel : CreateCityModel
    {
        public int Id { get; set; }
    }
}
