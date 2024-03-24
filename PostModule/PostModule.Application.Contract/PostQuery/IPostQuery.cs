using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Application.Contract.PostQuery
{
    public interface IPostQuery
    {
        List<PostAdminQueryModel> GetAllPostsForAdmin();
        PostAdminDetailQueryModel GetPostDetails(int id);
    }
}
