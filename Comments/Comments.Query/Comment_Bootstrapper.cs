using Comments.Application.Contract.CommentApplication.Query;
using Comments.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comments.Query
{
    public static class Comment_Bootstrapper
    {
        public static void Config(IServiceCollection services, string connection)
        {
            CommentInfrastryctureBootstrapper.Config(services, connection);
        }
    }
}
