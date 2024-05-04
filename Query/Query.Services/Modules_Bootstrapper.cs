﻿using Blogs.Query;
using Comments.Query;
using Emails.Application.Contract.EmailUserApplication.Command;
using Emails.Query;
using Microsoft.Extensions.DependencyInjection;
using PostModule.Query;
using Query.Contract.Admin.Comment;
using Query.Contract.Admin.Email;
using Query.Contract.Admin.MessageUser;
using Query.Contract.Admin.Seo;
using Query.Contract.UI.Blog;
using Query.Contract.UI.Comment;
using Query.Contract.UI.PostPackage;
using Query.Contract.UI.Site;
using Query.Services.Admin;
using Query.Services.UI;
using Seos.Query;
using Site.Query;
using Users.Query;

namespace Query.Services
{
	public static class Modules_Bootstrapper
	{
		public static void Config(IServiceCollection services , string connection)
		{
			Blog_Bootstrapper.Config(services, connection);
			User_Bootstrapper.Config(services, connection);
			Comment_Bootstrapper.Config(services, connection);
			Site_Bootstrapper.Config(services, connection);
			Seo_Bootstrapper.Config(services, connection);
			Post_Bootstrapper.Config(services, connection);
			Email_Bootstrapper.Config(services, connection);



			services.AddTransient<ICommentAdminQuery, CommentAdminQuery>();
			services.AddTransient<IEmailAdminQuery, EmailAdminQuery>();
			services.AddTransient<ISeoAdminQuery, SeoAdminQuery>();
			services.AddTransient<IMessageUserAdminQuery, MessageUserAdminQuery>();


			services.AddTransient<IBlogUiQuery, BlogUiQuery>();
			services.AddTransient<ICommentUiQuery, CommentUiQuery>();
			services.AddTransient<ISiteUiQuery, SiteUiQuery>();
			services.AddTransient<IPackageUiQuery, PackageUiQuery>();
		}
	}
}
