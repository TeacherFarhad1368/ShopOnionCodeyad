﻿using Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Domain.SliderAgg
{
    public class Slider : BaseEntityCreateActive<int>
    {
        public Slider(string imageName, string imageAlt)
        {
            ImageName = imageName;
            ImageAlt = imageAlt;
        }
        public void Edit(string imageName, string imageAlt)
        {
            ImageName = imageName;
            ImageAlt = imageAlt;
        }
        public string ImageName { get; private set; }
        public string ImageAlt { get; private set; }
    }
}
