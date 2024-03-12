using Shared.Domain;

namespace Users.Domain.UserAgg
{
    public class UserAddress : BaseEntityCreate<int>
    {
        public int StateId { get; private set; }
        public int CityId { get; private set; }
        public string AddressDetail { get; private set; }
        public string PostalCode { get; private set; }
        public string Phone { get; private set; }
        public string FullName { get; private set; }
        public string IranCode { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; }
       

        public UserAddress(int stateId, int cityId, string addressDetail, 
            string postalCode, string phone, string fullName, string iranCode, int userId)
        {
            StateId = stateId;
            CityId = cityId;
            AddressDetail = addressDetail;
            PostalCode = postalCode;
            Phone = phone;
            FullName = fullName;
            IranCode = iranCode;
            UserId = userId;
        }
    }
}
