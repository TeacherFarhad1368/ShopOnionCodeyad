using Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Domain.SiteServiceAgg
{
    public class SiteService : BaseEntityCreateActive<int>
    {
        public SiteService(string imageName, string imageAlt, string title)
        {
            ImageName = imageName;
            ImageAlt = imageAlt;
            Title = title;
        }
        public void Edit(string imageName, string imageAlt, string title)
        {
            ImageName = imageName;
            ImageAlt = imageAlt;
            Title = title;
        }
        public string ImageName { get; private set; }
        public string ImageAlt { get; private set; }
        public string Title { get; private set; }
    }
}
