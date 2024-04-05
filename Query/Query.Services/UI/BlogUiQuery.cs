using Blogs.Domain.BlogAgg;
using Blogs.Domain.BlogCategoryAgg;
using Query.Contract.UI;
using Query.Contract.UI.Blog;
using Seos.Domain;
using Shared.Application;
using Shared.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Services.UI;

internal class BlogUiQuery : IBlogUiQuery
{
    private readonly IBlogRepository _blogRepository;
    private readonly IBlogCategoryRepository _blogCategoryRepository;
    private readonly ISeoRepository _seoRepository;

    public BlogUiQuery(IBlogRepository blogRepository, IBlogCategoryRepository blogCategoryRepository, ISeoRepository seoRepository)
    {
        _blogRepository = blogRepository;
        _blogCategoryRepository = blogCategoryRepository;
        _seoRepository = seoRepository;
    }

    public BlogUiPaging GetBlogsForUi(string slug, int pageId, string filter)
    {
        string title = "آرشیو مقالات";
        int ownerSeoId = 0;
        string categoryTitle = "آرشیو مقالات";
        var res = _blogRepository.GetAllByQuery(b => b.Active).OrderByDescending(b=>b.Id);
        if (!string.IsNullOrEmpty(slug))
        {
            var category = _blogCategoryRepository.GetBySlug(slug);
            if (category == null) slug = "";
            res = res.Where(r => r.CategoryId == category.Id || r.SubCategoryId == category.Id).OrderByDescending(b => b.Id);
            title = title + " " + category.Title;
            ownerSeoId = category.Id;
            categoryTitle = category.Title;
        }
        if (!string.IsNullOrEmpty(filter))
        {
            res = res.Where(b => b.Title.Contains(filter) || b.ShortDescription.Contains(filter)
            || b.Text.Contains(filter)).OrderByDescending(b=>b.Id);
            title = title + " شامل : " + filter;
        }
        BlogUiPaging model = new();
        model.GetData(res, pageId, 4, 2);
        model.Filter = filter;
        model.Slug = slug;
        model.Title = title;
        model.Blogs = new();
        if (res.Count() > 0)
            model.Blogs = res.Skip(model.Skip).Take(model.Take)
                .Select(b => new BlogCardQueryModel
                {
                    ImageAlt = b.ImageAlt,
                    CategoryId = b.SubCategoryId > 0 ? b.SubCategoryId : b.CategoryId,
                    CategoryTitle = "",
                    CategorySlug = "",
                    CreationDate = b.CreateDate.ToPersainDate(),
                    Id = b.Id,
                    ImageName = FileDirectories.BlogImageDirectory400 + b.ImageName,
                    ShortDescription = b.ShortDescription,
                    Slug = b.Slug,
                    Title = b.Title,
                    Writer = b.Writer
                }).OrderByDescending(b => b.Id).ToList();

        if (model.Blogs.Count() > 0)
            model.Blogs.ForEach(x =>
            {
                var category = _blogCategoryRepository.GetById(x.CategoryId);
                x.CategoryTitle = category.Title;
                x.CategorySlug = category.Slug;
            });

        var seo = _seoRepository.GetSeoForUi(ownerSeoId, WhereSeo.BlogCategory,categoryTitle);
        model.Seo = new(seo.MetaTitle, seo.MetaDescription, seo.MetaKeyWords, seo.IndexPage, seo.Canonical, seo.Schema);
        model.Categories = GetArchiveBlogCategories();
        model.BreadCrumb = GetArchiveBreadCrumb(slug);
        return model;
    }

    private List<BreadCrumbQueryModel> GetArchiveBreadCrumb(string slug)
    {
        List<BreadCrumbQueryModel> model = new()
        {
            new BreadCrumbQueryModel(){Number = 1 , Title ="خانه" , Url = "/"} , 
            new BreadCrumbQueryModel(){Number = 2, Title = "مجله خبری" , Url = "/Blog"},
            new BreadCrumbQueryModel(){Number = 3, Title = "آرشیو مقالات" , Url = "/Blogs"}
        };
        if (!string.IsNullOrEmpty(slug))
        {
            var category = _blogCategoryRepository.GetBySlug(slug);
            if (category != null)
                model.Add(new BreadCrumbQueryModel() { Number = 4, Title = category.Title, Url = "" });
        }
        return model;
    }

    private List<BlogCategorySearchQueryModel> GetArchiveBlogCategories()
    {
        var model =  _blogCategoryRepository.GetAllByQuery(b => b.Active && b.Parent == 0)
            .Select(b => new BlogCategorySearchQueryModel
            {
                BlogCount = 0,
                Childs = new(),
                Slug = b.Slug,
                Title = b.Title,
                Id = b.Id
            }).ToList();
        model.ForEach(x =>
        {
            x.BlogCount = _blogRepository.GetAllByQuery(b => b.Active && (b.SubCategoryId == x.Id || b.CategoryId == x.Id)).Count();
            x.Childs = _blogCategoryRepository.GetAllByQuery(b => b.Active && b.Parent == x.Id)
            .Select(b => new BlogCategorySearchQueryModel
            {
                BlogCount =0 ,
                Childs = new(),
                Id = b.Id,
                Slug = b.Slug,
                Title = b.Title
            }).ToList();
        });
        return model;
    }
}
