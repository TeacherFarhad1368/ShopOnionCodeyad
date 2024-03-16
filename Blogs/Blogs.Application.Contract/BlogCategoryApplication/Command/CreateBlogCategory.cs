using Microsoft.AspNetCore.Http;
using Shared.Application;
using Shared.Application.BaseCommands;
using System.ComponentModel.DataAnnotations;

namespace Blogs.Application.Contract.BlogCategoryApplication.Command
{
    public class CreateBlogCategory : Title_Slug_Image_ImageAlt
	{ 
        public int Parent { get; set; }
    }
}
