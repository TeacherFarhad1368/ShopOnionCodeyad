using Azure.Core;
using Emails.Application.Contract.EmailUserApplication.Command;
using Emails.Application.Contract.MessageUserApplication.Command;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Query.Contract.UI.Site;
using Shared.Application.Services.Auth;
using Shared.Application.Validations;
using System.Diagnostics;


namespace ShopBoloor.WebApplication.Controllers;

public class HomeController : Controller
{
    private readonly IEmailUserApplication _emailUserApplication;
    private readonly IAuthService _authService;
    private readonly ISiteUiQuery _siteUiQuery;
    private readonly IMessageUserApplication _messageUserApplication;
    public HomeController(IEmailUserApplication emailUserApplication, IAuthService authService,
        ISiteUiQuery siteUiQuery, IMessageUserApplication messageUserApplication)
    {
        _emailUserApplication = emailUserApplication;
        _authService = authService;
        _siteUiQuery = siteUiQuery;
        _messageUserApplication = messageUserApplication;
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
}


    internal class GeoResponse
    {
        public string CountryCode { get; set; }
    }