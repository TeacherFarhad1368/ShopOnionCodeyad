using Comments.Application;
using Comments.Domain.CommentAgg;
using Comments.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Comments.Infrastructure
{
    public static class CommentInfrastryctureBootstrapper
    {
        public static void Config(IServiceCollection services,string connection)
        {
            services.AddTransient<ICommentRepository, CommentRepository>();
            CommentApplicationBootstrapper.Config(services);
            services.AddDbContext<CommentContext>(x =>
            {
                x.UseSqlServer(connection);
            });
        }
    }
}