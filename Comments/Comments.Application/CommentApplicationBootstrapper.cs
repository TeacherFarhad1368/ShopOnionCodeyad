using Comments.Application.Contract.CommentApplication.Command;
using Comments.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comments.Application
{
    public static class CommentApplicationBootstrapper
    {
        public static void Config(IServiceCollection services)
        {
            services.AddTransient<ICommentApplication, CommentApplication>();
        }
    }
}
