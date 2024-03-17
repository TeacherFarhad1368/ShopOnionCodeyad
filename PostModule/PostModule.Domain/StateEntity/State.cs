using Shared.Domain;
using PostModule.Domain.CityEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Domain.StateEntity
{
    public class State : BaseEntityCreate<int>
    {
        public string Title { get; private set; }
        public string CloseStates { get; private set; }
        public List<City> Cities { get; private set; }
        public State(string title)
        {
            Title = title;
            CloseStates = "";
            Cities = new();
        }
        public void Edit(string title)
        {
            Title = title;
        }
        public void ChangeCloseStates(List<int> States)
        {
            CloseStates = string.Join('-',States); 
        }
    }
}
