using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Application.Contract.SitePageApplication.Command
{
	public interface ISitePageApplication
	{
		OperationResult Create(CreateSitePage command);
		OperationResult Edit(EditSitePage command);
		EditSitePage GetForEdit(int id);
		bool ActivationChange(int id);
	}
}
