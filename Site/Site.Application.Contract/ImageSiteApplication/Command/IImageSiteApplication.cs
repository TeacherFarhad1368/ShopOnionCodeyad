using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Contract.ImageSiteApplication.Command
{
	public interface IImageSiteApplication
	{
		OperationResult Create(CreateImageSite command);
		bool DeleteFromDataBase(int id);
	}
}
