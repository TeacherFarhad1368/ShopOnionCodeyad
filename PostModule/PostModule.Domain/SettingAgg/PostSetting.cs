using Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Domain.SettingAgg
{
    public class PostSetting : BaseEntityCreateUpdate<int>
    {
        public PostSetting(string? packageTitle, string? packageDescription, string? apiDescription)
        {
            PackageTitle = packageTitle;
            PackageDescription = packageDescription;
            ApiDescription = apiDescription;
        }
        public void Edit(string? packageTitle, string? packageDescription, string? apiDescription)
        {
            PackageTitle = packageTitle;
            PackageDescription = packageDescription;
            ApiDescription = apiDescription;
            UpdateEntity();
        }
        public string? PackageTitle { get; private set; }
        public string? PackageDescription { get; private set; }
        public string? ApiDescription { get; private set; }
    }
}
