using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Application.Contract.PostPriceApplication
{
    public interface IPostPriceApplication
    {
        OperationResult Create(CreatePostPrice command);
        OperationResult Edit(EditPostPrice command);
        List<PostPriceModel> GetAllForPost(int postId);
        EditPostPrice GetForEdit(int id);
    }
}
