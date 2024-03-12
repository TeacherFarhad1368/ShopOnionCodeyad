using Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Domain.SliderAgg
{
    public class Slider : BaseEntity<int>
    {
        public Slider(string imageName, string imageAlt)
        {
            ImageName = imageName;
            ImageAlt = imageAlt;
            Active = true;
        }
        public void Edit(string imageName, string imageAlt)
        {
            ImageName = imageName;
            ImageAlt = imageAlt;
        }
        public void ActivationChange()
        {
            if(Active) Active = false;
            else Active = true;
        }
        public string ImageName { get; private set; }
        public string ImageAlt { get; private set; }
        public bool Active { get; private set; }
    }
}
