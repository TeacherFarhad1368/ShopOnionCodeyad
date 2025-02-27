using Shared.Application;
using Shared.Infrastructure;
using PostModule.Application.Contract.PostApplication;
using PostModule.Domain.PostEntity;
using PostModule.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostModule.Application.Contract.PostCalculate;
using Shared.Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace PostModule.Infrastracture.EF.Repositories;

internal class PostRepository : Repository<int,Post> , IPostRepository
{
    private readonly Post_Context _context;
    public PostRepository(Post_Context context) : base(context)
    {
        _context = context;
    }

    public async Task<List<PostPriceResponseModel>> CalculatePostAsync(PostPriceRequestModel command)
    {
        List<PostPriceResponseModel> model = new();
        IQueryable<Post> posts = _context.Posts.Include(p=>p.PostPrices);
        List<Post> posts1 = new();
        CalculatePost calculatePost = await GetCalculatePostAsync(command);
        switch (calculatePost)
        {
            case CalculatePost.درون_شهری:
                posts1 = await posts.Where(p => p.InsideCity).ToListAsync();
                break;
            case CalculatePost.تهران:
                posts1 = await posts.Where(p => p.InsideCity).ToListAsync();
                break;
            case CalculatePost.مرکز_استان:
                posts1 = await posts.Where(p => p.InsideCity).ToListAsync();
                break;
            case CalculatePost.درون_استانی:
                posts1 = await posts.Where(p => p.OutSideCity).ToListAsync();
                break;
            case CalculatePost.هم_جوار:
                posts1 = await posts.Where(p => p.OutSideCity).ToListAsync();
                break;
            case CalculatePost.غیر_هم_جوار:
                posts1 = await posts.Where(p => p.OutSideCity).ToListAsync();
                break;
            case CalculatePost.هیچکدام:
                break;
            default:
                break;
        }
        if(posts1.Count() > 0)
        {
            foreach (var item in posts1) 
            {
                int price = item.Calculate(calculatePost, command.Weight);
                PostPriceResponseModel postPrice = new(item.Title, item.Status, price,item.Id);
                model.Add(postPrice);
            }
        }



        return model;
    }

    private async Task<CalculatePost> GetCalculatePostAsync(PostPriceRequestModel command)
    {
        
        var sourceCity = await _context.Cities.Include(c => c.State).SingleOrDefaultAsync(c=>c.Id ==command.SourceCityId);
        var destinationCity = await _context.Cities.Include(c => c.State).SingleOrDefaultAsync(c=>c.Id ==command.DestinationCityId);
        if (sourceCity == null || destinationCity == null) return CalculatePost.هیچکدام;
        if(command.SourceCityId == command.DestinationCityId)
        {
            switch (sourceCity.Status)
            {
                case CityStatus.تهران: return CalculatePost.تهران;
                case CityStatus.مرکز_استان: return CalculatePost.مرکز_استان;
                case CityStatus.شهرستان_معمولی: return CalculatePost.درون_شهری;
                default: return CalculatePost.هیچکدام;
            }
        }
        else
        {
            if (sourceCity.StateId == destinationCity.StateId)
                return CalculatePost.درون_استانی;
            else
            {
                if (sourceCity.State.CloseStates.StartsWith($"{destinationCity.StateId}-") ||
                    sourceCity.State.CloseStates.Contains($"-{destinationCity.StateId}-") ||
                    sourceCity.State.CloseStates.EndsWith($"_{destinationCity.StateId}"))
                    return CalculatePost.هم_جوار;
                else return CalculatePost.غیر_هم_جوار;
            }
        }
    }

    public EditPost GetForEdit(int id)
    {
        return _context.Posts.Select(p => new EditPost
        {
            CityPricePlus = p.CityPricePlus,
            Id = p.Id,
            InsideStatePricePlus = p.InsideStatePricePlus,
            StateCenterPricePlus = p.StateCenterPricePlus,
            StateClosePricePlus = p.StateClosePricePlus,
            StateNonClosePricePlus = p.StateNonClosePricePlus,
            Status = p.Status,
            TehranPricePlus = p.TehranPricePlus,
            Title = p.Title,
            Description = p.Description
        }).SingleOrDefault(p => p.Id == id);
    }
}
