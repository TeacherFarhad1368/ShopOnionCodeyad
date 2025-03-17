using Azure.Core;
using Emails.Application.Contract.EmailUserApplication.Command;
using Emails.Application.Contract.MessageUserApplication.Command;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Query.Contract.UI.Blog;
using Query.Contract.UI.Product;
using Query.Contract.UI.Site;
using Shared.Application.Services.Auth;
using Shared.Application.Validations;
using Shop.Application.Contract.OrderApplication.Command;
using Shop.Application.Contract.WishListApplication.Command;
using Shop.Application.Contract.WishListApplication.Query;
using ShopBoloor.WebApplication.Models;
using System.Collections.Generic;
using System.Diagnostics;


namespace ShopBoloor.WebApplication.Controllers;

public class HomeController : Controller
{
    private readonly IEmailUserApplication _emailUserApplication;
    private readonly IAuthService _authService;
    private readonly ISiteUiQuery _siteUiQuery;
    private readonly IMessageUserApplication _messageUserApplication;
    private readonly IWishListQuery _wishListQuery;
    private readonly IWishListApplication _wishListApplication;
    private readonly IProductUiQuery _productUiQuery;
    private readonly IBlogUiQuery _blogUiQuery; 
    public HomeController(IEmailUserApplication emailUserApplication, IAuthService authService, IWishListApplication wishListApplication,
        ISiteUiQuery siteUiQuery, IMessageUserApplication messageUserApplication, IWishListQuery wishListQuery, IBlogUiQuery blogUiQuery,
        IProductUiQuery productUiQuery)
    {
        _emailUserApplication = emailUserApplication;
        _authService = authService;
        _productUiQuery = productUiQuery;   
        _siteUiQuery = siteUiQuery;
        _messageUserApplication = messageUserApplication;
        _wishListQuery = wishListQuery;
        _wishListApplication = wishListApplication;
        _blogUiQuery = blogUiQuery;
    }
    public IActionResult Index() => View(); 
    [Route("/Page/{slug}")]
    public IActionResult Page(string slug)
    {
        if (string.IsNullOrEmpty(slug)) return NotFound();
        var model = _siteUiQuery.GetSitePageQueryModel(slug);
        if(model == null) return NotFound();
        return View(model);
    }
    [Route("/About")]
    public IActionResult About()
    {
        var remoteIpAddress = HttpContext.Connection.RemoteIpAddress;
        var model = _siteUiQuery.GetAboutUsModelForUi();
        return View(model);
    }
    [Route("/Contact")]
    public IActionResult Contact()
    {
        var model = _siteUiQuery.GetContactUsModelForUi();
        return View(model);
    }
    [HttpPost]
    public IActionResult SendMessage(string fullName,string email,string subject,string message)
    {
        var userId = _authService.GetLoginUserId();
        if(string.IsNullOrEmpty(fullName) || 
            (string.IsNullOrEmpty(email) || (email.IsEmail() == false && email.IsMobile() == false))
            || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(message))
        {
            TempData["FaildMessage"] = true;
            return RedirectToAction("Contact");
        }
        var res = _messageUserApplication.Create(new CreateMessageUser()
        {
            Email = email.IsEmail() ? email : "",
            FullName = fullName,
            Message = message,
            PhoneNumber = email.IsMobile() ? email : "",
            Subject = subject,
            UserId = userId
        });
        if (res.Success)
        {
            TempData["SuccessMessage"] = true;
            return RedirectToAction("Contact");
        }
        else
        {
            TempData["FaildMessage"] = true;
            return RedirectToAction("Contact");
        }
    }
    public IActionResult Error()
    {
        return View();
    }
    [HttpPost]
    public string AddEmailUser(string email)
    {
        var userId = _authService.GetLoginUserId();
        CreateEmailUser model = new()
        {
            Email = email,
            UserId = userId
        };
        var res = _emailUserApplication.Create(model);
        if (res.Success) return "";
        return res.Message;
    }
    [HttpGet]
    public int GetWishListCount()
    {
        var userId = _authService.GetLoginUserId();
        if (userId == 0)
        {
            string cookieName = "boloorShop-wishList-items";
            if (Request.Cookies.TryGetValue(cookieName, out var cartJson))
            {
                List<int> wishesIds = System.Text.Json.JsonSerializer.Deserialize<List<int>>(cartJson);
                if (wishesIds.Count > 0)
                {
                    return wishesIds.Count;
                }
                else return 0;
            }
            else return 0;
        }
        else
        {
            string cookieName = "boloorShop-wishList-items";
            if (Request.Cookies.TryGetValue(cookieName, out var cartJson))
            {
                List<int> wishesIds = System.Text.Json.JsonSerializer.Deserialize<List<int>>(cartJson);
                if (wishesIds.Count > 0)
                {
                    _wishListApplication.AddUsersWishList(userId,wishesIds);
                    Response.Cookies.Delete(cookieName);
                }
            }
            return _wishListQuery.GetUserWishListCount(userId);
        }
    }
    [HttpGet]
    public bool CheckProductWishList(int id)
    {
        var userId = _authService.GetLoginUserId();
        if (userId == 0)
        {
            string cookieName = "boloorShop-wishList-items";
            if (Request.Cookies.TryGetValue(cookieName, out var cartJson))
            {
                List<int> wishesIds = System.Text.Json.JsonSerializer.Deserialize<List<int>>(cartJson);
                if (wishesIds.Count > 0)
                {
                    return wishesIds.Any(w => w == id);
                }
                else return false;
            }
            else return false;
        }
        else
            return _wishListQuery.IsUserHaveProductWishList(userId, id);
    }
    [HttpGet]
    public bool UbsertProductWishList(int id)
    {
        var userId = _authService.GetLoginUserId();
        if (userId == 0)
        {
            List<int> wishesIds = new List<int>();
            string cookieName = "boloorShop-wishList-items";
            if (Request.Cookies.TryGetValue(cookieName, out var cartJson))
            {
                 wishesIds = System.Text.Json.JsonSerializer.Deserialize<List<int>>(cartJson);
                if (wishesIds.Count > 0 && wishesIds.Any(w => w == id))
                {
                    var x = wishesIds.Single(w => w == id);
                    wishesIds.Remove(x);
                }
                else
                    wishesIds.Add(id);
            }
            else
                wishesIds.Add(id);
            Response.Cookies.Delete(cookieName);
            try
            {
                if(wishesIds.Count > 0)
                {
                    var serializedList = System.Text.Json.JsonSerializer.Serialize(wishesIds);
                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(30),
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict
                    };

                    Response.Cookies.Append(cookieName, serializedList, cookieOptions);
                }
                return true;
            }
            catch 
            {
                return false;
            }
        }
        else
            return _wishListApplication.AddUserProductWishList(userId, id);
    }
    [HttpGet]
    public IActionResult WishList()
    {
        var userId = _authService.GetLoginUserId();
        List<WishListProductQueryModel> model = new();
        if (userId == 0)
        {
            string cookieName = "boloorShop-wishList-items";
            if (Request.Cookies.TryGetValue(cookieName, out var cartJson))
            {
                List<int> wishesIds = System.Text.Json.JsonSerializer.Deserialize<List<int>>(cartJson);
                if (wishesIds.Count > 0)
                {
                    model = _productUiQuery.GetWishListForUserFromCppkie(wishesIds);
                }
            }
        }
        else
        {
            string cookieName = "boloorShop-wishList-items";
            if (Request.Cookies.TryGetValue(cookieName, out var cartJson))
            {
                List<int> wishesIds = System.Text.Json.JsonSerializer.Deserialize<List<int>>(cartJson);
                if (wishesIds.Count > 0)
                {
                    _wishListApplication.AddUsersWishList(userId, wishesIds);
                    Response.Cookies.Delete(cookieName);
                }
            }
            model = _productUiQuery.GetWishListForUserLoggedIn(userId);
        }
        return View(model);  
    }
    [HttpPost]
    public JsonResult AjaxSearch(string filter)
    {
        List<SearchAjaxQueryModel> model = new();
        if (!string.IsNullOrEmpty(filter))
        {
            var product = _productUiQuery.SearchAjax(filter);
            if (product.Count() > 0)
                model.AddRange(product.Select(p => new SearchAjaxQueryModel
                {
                    ImageAddress = p.ImageAddress,
                    Url = $"/Product/{p.id}/{p.Slug}",
                    Title = p.Title,    
                }).ToList());
            if(model.Count() < 10)
            {
                int count = 10 - model.Count;
                var blogs = _blogUiQuery.SearchAjax(filter,count);
                if (blogs.Count() > 0)
                    model.AddRange(blogs.Select(p => new SearchAjaxQueryModel
                    {
                        ImageAddress = p.ImageAddress,
                        Url = $"/Blog/{p.Slug}",
                        Title = p.Title,
                    }).ToList());
            }
        }
        return Json(JsonConvert.SerializeObject(model));    
    }
}
