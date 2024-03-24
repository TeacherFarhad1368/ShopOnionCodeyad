using Shared.Infrastructure;
using PostModule.Application.Contract.PostPriceApplication;
using PostModule.Domain.PostEntity;
using PostModule.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostModule.Infrastracture.EF.Repositories
{
    internal class PostPriceRepository : Repository<int,PostPrice> , IPostPriceRepository
    {
        private readonly Post_Context _context;
        public PostPriceRepository(Post_Context context) : base(context)
        {
            _context = context;
        }

        public EditPostPrice GetForEdit(int id)
        {
            return _context.PostPrices.Select(p => new EditPostPrice
            {
                CityPrice = p.CityPrice,
                End = p.End,
                Id = p.Id,
                InsideStatePrice = p.InsideStatePrice,
                Start = p.Start,
                StateCenterPrice = p.StateCenterPrice,
                StateClosePrice = p.StateClosePrice,
                StateNonClosePrice = p.StateNonClosePrice,
                TehranPrice = p.TehranPrice
            }).SingleOrDefault(p => p.Id == id);
        }
    }
}
