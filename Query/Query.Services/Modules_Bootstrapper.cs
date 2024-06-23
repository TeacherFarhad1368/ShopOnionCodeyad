using Blogs.Query;
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
using Query.Contract.UserPanel.Address;
using Query.Contract.UserPanel.PostOrder;
using Query.Contract.UserPanel.User;
using Query.Contract.UserPanel.Wallet;
using Query.Services.Admin;
using Query.Services.UI;
using Query.Services.UserPanel;
using Seos.Query;
using Site.Query;
using Transactions.Query;
using Users.Query;

namespace Query.Services
{
	public static class Modules_Bootstrapper
	{
		public static void Config(IServiceCollection services , string connection)
		{
            #region Modules
            Blog_Bootstrapper.Config(services, connection);
            User_Bootstrapper.Config(services, connection);
            Comment_Bootstrapper.Config(services, connection);
            Site_Bootstrapper.Config(services, connection);
            Seo_Bootstrapper.Config(services, connection);
            Post_Bootstrapper.Config(services, connection);
            Email_Bootstrapper.Config(services, connection);
            Transaction_Bootstrapper.Config(services, connection);
            #endregion
            #region Admin Query
            services.AddTransient<ICommentAdminQuery, CommentAdminQuery>();
            services.AddTransient<IEmailAdminQuery, EmailAdminQuery>();
            services.AddTransient<ISeoAdminQuery, SeoAdminQuery>();
            services.AddTransient<IMessageUserAdminQuery, MessageUserAdminQuery>();
            #endregion
            #region UI Query 
            services.AddTransient<IBlogUiQuery, BlogUiQuery>();
            services.AddTransient<ICommentUiQuery, CommentUiQuery>();
            services.AddTransient<ISiteUiQuery, SiteUiQuery>();
            services.AddTransient<IPackageUiQuery, PackageUiQuery>();
            #endregion
            #region UserPanel
            services.AddTransient<IUserPanelQuery, UserPanelQuery>();
            services.AddTransient<IPostOrderUserPanelQuery, PostOrderUserPanelQuery>();
            services.AddTransient<IUserAddressUserPanelQuery, UserAddressUserPanelQuery>();
            services.AddTransient<IUserPanelWalletQuery, UserPanelWalletQuery>();
            #endregion
        }
    }
}
