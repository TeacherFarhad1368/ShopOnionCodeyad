using Shared.Domain;
using Shared.Domain.Enum;
using PostModule.Domain.StateEntity;

namespace PostModule.Domain.CityEntity
{
    public class City : BaseEntityCreate<int>
    {
        public int StateId { get; private set; }
        public string Title { get; private set; }
        public CityStatus Status { get; private set; }
        public State State { get; private set; }
        public City(int stateId,string title,CityStatus status)
        {
            StateId = stateId;
            Title = title;
            Status = status;
        }
        public void Edit(string title, CityStatus status)
        {
            Title = title;
            Status = status;
        }
        public void IsTehran()
        {
            Status = CityStatus.تهران;
        }
        public void IsCenter()
        {
            Status = CityStatus.مرکز_استان;
        }
        public void INotCenterOrTehran()
        {
            Status = CityStatus.شهرستان_معمولی;
        }
    }
}
