using Shared.Domain;
using PostModule.Application.Contract.PostPriceApplication;
using PostModule.Domain.PostEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Domain.Services
{
    public interface IPostPriceRepository : IRepository<int, PostPrice>
    {
        EditPostPrice GetForEdit(int id);
    }
}
