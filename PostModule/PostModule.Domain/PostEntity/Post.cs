using Shared.Domain;
using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Domain.PostEntity
{
    public class Post : BaseEntityCreateActive<int>
    {
        public string Title { get; private set; }
        public string Status { get; private set; }
        public string? Description { get; private set; }
        public int TehranPricePlus { get; private set; }
        public int StateCenterPricePlus { get; private set; }
        public int CityPricePlus { get; private set; }
        public int InsideStatePricePlus { get; private set; }
        public int StateClosePricePlus { get; private set; }
        public int StateNonClosePricePlus { get; private set; }
        public bool InsideCity { get; private set; }
        public bool OutSideCity { get; private set; }
        public List<PostPrice> PostPrices { get; set; }
        public Post(string title, string status, int tehranPricePlus, 
            int stateCenterPricePlus, int cityPricePlus, int insideStatePricePlus,
            int stateClosePricePlus, int stateNonClosePricePlus, string description)
        {
			SetValues(title, status, tehranPricePlus, stateCenterPricePlus,
				cityPricePlus, insideStatePricePlus, stateClosePricePlus, stateNonClosePricePlus, description);
            InsideCity = true;
            OutSideCity = true;
        }
        public void Edit(string title, string status, int tehranPricePlus,
            int stateCenterPricePlus, int cityPricePlus, int insideStatePricePlus,
            int stateClosePricePlus, int stateNonClosePricePlus, string description)
        {
            SetValues(title, status, tehranPricePlus, stateCenterPricePlus,
                cityPricePlus, insideStatePricePlus, stateClosePricePlus, stateNonClosePricePlus, description);
        }
        private void SetValues(string title, string status, int tehranPricePlus,
			int stateCenterPricePlus, int cityPricePlus, int insideStatePricePlus,
			int stateClosePricePlus, int stateNonClosePricePlus, string description)
        {
			Title = title;
			Status = status;
			TehranPricePlus = tehranPricePlus;
			StateCenterPricePlus = stateCenterPricePlus;
			CityPricePlus = cityPricePlus;
			InsideStatePricePlus = insideStatePricePlus;
			StateClosePricePlus = stateClosePricePlus;
			StateNonClosePricePlus = stateNonClosePricePlus;
			Description = description;
		}
        public void InsideCityChange()
        {
            if (InsideCity) InsideCity = false;
            else InsideCity = true;
        }
        public void OutSideCityChange()
        {
            if (OutSideCity) OutSideCity = false;
            else OutSideCity = true;
        }
        public int Calculate(CalculatePost calculate,int weight)
        {
            int price = 0;
            int pricePlus = 0;
            int count = 0;
            PostPrice postPrice = null;
            PostPrice lastPostPrice = null;
             postPrice = PostPrices.SingleOrDefault(p => p.Start <= weight && p.End >= weight);
            if (postPrice == null)
            {

                lastPostPrice = PostPrices.OrderByDescending(p => p.End).First();
                var end = lastPostPrice.End;
                var m = weight - end;
                count = m / 1000;
                if (m % 1000 > 0) count++;
            }


            switch (calculate)
            {
                case CalculatePost.درون_شهری:
                    if(postPrice != null)
                    {
                        price = postPrice.CityPrice;
                    }
                    else
                    {
                        price = lastPostPrice.CityPrice;
                        pricePlus = count * CityPricePlus;
                    }
                    break;
                case CalculatePost.تهران:
                    if (postPrice != null)
                    {
                        price = postPrice.TehranPrice;
                    }
                    else
                    {
                        price = lastPostPrice.TehranPrice;
                        pricePlus = count * TehranPricePlus;
                    }
                    break;
                case CalculatePost.مرکز_استان:
                    if (postPrice != null)
                    {
                        price = postPrice.StateCenterPrice;
                    }
                    else
                    {
                        price = lastPostPrice.StateCenterPrice;
                        pricePlus = count * StateCenterPricePlus;
                    }
                    break;
                case CalculatePost.درون_استانی:
                    if (postPrice != null)
                    {
                        price = postPrice.InsideStatePrice;
                    }
                    else
                    {
                        price = lastPostPrice.InsideStatePrice;
                        pricePlus = count * InsideStatePricePlus;
                    }
                    break;
                case CalculatePost.هم_جوار:
                    if (postPrice != null)
                    {
                        price = postPrice.StateClosePrice;
                    }
                    else
                    {
                        price = lastPostPrice.StateClosePrice;
                        pricePlus = count * StateClosePricePlus;
                    }
                    break;
                case CalculatePost.غیر_هم_جوار:
                    if (postPrice != null)
                    {
                        price = postPrice.StateNonClosePrice;
                    }
                    else
                    {
                        price = lastPostPrice.StateNonClosePrice;
                        pricePlus = count * StateNonClosePricePlus;
                    }
                    break;
                default: return 0;
            }
            return price + pricePlus;
        }
    }
}
