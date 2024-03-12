namespace Users.Application.Contract.UserAddressApplication.Command
{
    public class CreateAddress
    {
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string AddressDetail { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public string IranCode { get; set; }
        public int UserId { get; set; }
    }
}
