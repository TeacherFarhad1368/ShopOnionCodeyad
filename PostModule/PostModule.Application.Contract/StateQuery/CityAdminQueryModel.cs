using Shared.Domain.Enum;

namespace PostModule.Application.Contract.StateQuery
{
	public class CityAdminQueryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CreationDate { get; set; }
		public CityStatus Status { get; set; }
	}
}
