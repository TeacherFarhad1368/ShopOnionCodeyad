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
        public PostSetting(string? packageTitle, string? packageDescription)
        {
            PackageTitle = packageTitle;
            PackageDescription = packageDescription;
        }
        public void Edit(string? packageTitle, string? packageDescription)
        {
            PackageTitle = packageTitle;
            PackageDescription = packageDescription;
            UpdateEntity();
        }
        public string? PackageTitle { get; private set; }
        public string? PackageDescription { get; private set; }
    }
}
