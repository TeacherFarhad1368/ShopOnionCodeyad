using Shared.Domain;

namespace Shop.Domain.OrderAgg
{
    public class OrderAddress : BaseEntity<int>
    {
        public int StateId { get; private set; }
        public int CityId { get; private set; }
        public string AddressDetail { get; private set; }
        public string PostalCode { get; private set; }
        public string Phone { get; private set; }
        public string FullName { get; private set; }
        public string? IranCode { get; private set; }
        public int OrderId { get; private set; }


        public OrderAddress(int stateId, int cityId, string addressDetail,
            string postalCode, string phone, string fullName, string? iranCode, int orderId)
        {
            StateId = stateId;
            CityId = cityId;
            AddressDetail = addressDetail;
            PostalCode = postalCode;
            Phone = phone;
            FullName = fullName;
            IranCode = iranCode;
            OrderId = OrderId;
        }
        public void Edit(int stateId, int cityId, string addressDetail,
           string postalCode, string phone, string fullName, string? iranCode)
        {
            StateId = stateId;
            CityId = cityId;
            AddressDetail = addressDetail;
            PostalCode = postalCode;
            Phone = phone;
            FullName = fullName;
            IranCode = iranCode;
        }
    }
}
