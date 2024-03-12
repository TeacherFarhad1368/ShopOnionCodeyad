using Shared.Domain;
using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Domain.BanerAgg
{
    public class Baner : BaseEntity<int>
    {
        public Baner(string imageName, string imageAlt, string url, BanerState state)
        {
            ImageName = imageName;
            ImageAlt = imageAlt;
            Url = url;
            State = state;
        }
        public void Edit(string imageName, string imageAlt, string url)
        {
            ImageName = imageName;
            ImageAlt = imageAlt;
            Url = url;
        }
        public void ActivationChange()
        {
            if(Active) Active = false;
            else Active = true;
        }
        public string ImageName { get; private set; }
        public string ImageAlt { get; private set; }
        public string Url { get; private set; }
        public BanerState State { get; private set; }
        public bool Active { get; private set; }
    }
}
