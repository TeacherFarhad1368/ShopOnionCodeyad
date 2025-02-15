﻿using Discounts.Application.Contract.OrderDiscountApplication.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostModule.Application.Contract.StateQuery;
using Query.Contract.UserPanel.Address;
using Query.Contract.UserPanel.Order;
using Shared.Application;
using Shared.Application.Services.Auth;
using Shop.Application.Contract.OrderApplication.Command;
using Shop.Application.Contract.OrderApplication.Query;
using System.Text.Json;
using Users.Application.Contract.UserAddressApplication.Command;

namespace ShopBoloor.WebApplication.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class OrderController : Controller
    {
        private int _userId;
        private readonly IAuthService _authService;
        private readonly IOrderApplication _orderApplication;
        private readonly IOrderQuery _orderQuery;
        private readonly IOrderUserPanelQuery _orderUserPanelQuery;
        private readonly IOrderDiscountApplication _orderDiscountApplication;
        private readonly IUserAddressUserPanelQuery _userAddressUserPanelQuery;
        private readonly IUserAddressApplication _userAddressApplication;
        private readonly IStateQuery _stateQuery;   
        public OrderController(IAuthService authService, IOrderApplication orderApplication, IOrderQuery orderQuery,
            IOrderUserPanelQuery orderUserPanelQuery, IOrderDiscountApplication orderDiscountApplication,
            IUserAddressUserPanelQuery userAddressUserPanelQuery, IUserAddressApplication userAddressApplication,
            IStateQuery stateQuery)
        {
            _authService = authService;
            _orderApplication = orderApplication;
            _orderQuery = orderQuery;
            _orderUserPanelQuery = orderUserPanelQuery; 
            _orderDiscountApplication = orderDiscountApplication;
            _userAddressUserPanelQuery = userAddressUserPanelQuery; 
            _userAddressApplication = userAddressApplication;   
            _stateQuery = stateQuery;
        }

        public async Task<IActionResult> Order()
        {
            _userId = _authService.GetLoginUserId();
            List<ShopCartViewModel> model = new();
            string cookieName = "boloorShop-cart-items";
            if (Request.Cookies.TryGetValue(cookieName, out var cartJson))
            {
                model = JsonSerializer.Deserialize<List<ShopCartViewModel>>(cartJson);
                var ok = await _orderApplication.UbsertOpenOrderForUserAsync(_userId, model);
                Response.Cookies.Delete(cookieName);
            }
            await _orderUserPanelQuery.CheckOrderItemDataAsync(_userId);
            await _orderApplication.CheckOrderEmpty(_userId);
            var res = await _orderUserPanelQuery.GetOpenOrderForUserAsync(_userId);
            if(res == null)
            {
                TempData["noOpenOrder"] = true;
                return Redirect("/Shop");
            }
            return View(res);
        }
        public async Task<bool> DeleteOrderItem(int id)
        {
            _userId = _authService.GetLoginUserId();
            return await _orderApplication.DeleteOrderItemAsync(id,_userId);
        }
        public async Task<IActionResult> OrderItemMinus(int id)
        {
            _userId = _authService.GetLoginUserId();
            OperationResult res = await _orderApplication.OrderItemMinus(id,_userId);
            var model = JsonSerializer.Serialize(res);
            return Json(model);
        }
        public async Task<IActionResult> OrderItemPlus(int id)
        {
            _userId = _authService.GetLoginUserId();
            OperationResult res = await _orderApplication.OrderItemPlus(id, _userId);
            var model = JsonSerializer.Serialize(res);
            return Json(model);
        }
        [HttpPost]
        public async Task<JsonResult> AddOrderSellerDiscount(int id,string code)
        {
            OperationResult model = new(false);
            _userId = _authService.GetLoginUserId();
            OperationResultWithKey result = await
                _orderUserPanelQuery.HaveUserOpenOrderSellerAsyncByOrderSellerIdAsync(_userId, id);
            if(result.Success != false)
            {
                OperationResultOrderDiscount res = await _orderDiscountApplication.GetOrderDiscountForAddOrderSellerdiscountAsync(id, code);
                if(res.Success)
                {
                  bool ok =  await _orderApplication.AddOrderSellerDiscountAsync(_userId, id, res.Id, res.Title, res.Percent);
                    if (ok)
                    {
                        model.Success = true;
                        model.Message = res.Message;
                    }
                    else
                    {
                        await _orderDiscountApplication.MinusUseAsync(res.Id);
                        model.Message = "عملیات نا موفق !! مجددا تلاش کنید ";
                        
                    }
                }
                else
                {
                    model.Message = res.Message;
                }
            }
            else
            {
                model.Message = result.Message;
            }
            var json = JsonSerializer.Serialize(model);
            return Json(json);
        }
        [HttpPost]
        public async Task<JsonResult> AddOrderDiscount(string code)
        {
            OperationResult model = new(false);
            _userId = _authService.GetLoginUserId();
            bool ok = await _orderUserPanelQuery.HaveUserOpenOrderAsync(_userId);
            if (ok)
            {
                OperationResultOrderDiscount res = await _orderDiscountApplication.GetOrderDiscountForAddOrderdiscountAsync(code);
                if (res.Success)
                {
                    bool add = await _orderApplication.AddOrderDiscountAsync(_userId, res.Id, res.Title, res.Percent);
                    if(add)
                    {
                        model.Success = true;
                        model.Message = res.Message;
                    }
                    else
                    {
                        await _orderDiscountApplication.MinusUseAsync(res.Id);
                        model.Message = "عملیات نا موفق !! مجددا تلاش کنید ";
                    }
                }
                else
                    model.Message = res.Message;
            }
            else
                model.Message = "شما فاکتور بازی ندارید .";
            var json = JsonSerializer.Serialize(model);
            return Json(json);
        }
        public async Task<JsonResult> OpenOrderItems()
        {
            _userId = _authService.GetLoginUserId();
            List<ShopCartViewModel> model = new();
            string cookieName = "boloorShop-cart-items";
            if (Request.Cookies.TryGetValue(cookieName, out var cartJson))
            {
                model = JsonSerializer.Deserialize<List<ShopCartViewModel>>(cartJson);
                var ok = await _orderApplication.UbsertOpenOrderForUserAsync(_userId, model);
                Response.Cookies.Delete(cookieName);
            }
            await _orderUserPanelQuery.CheckOrderItemDataAsync(_userId);
            await _orderApplication.CheckOrderEmpty(_userId);
            var res = await _orderUserPanelQuery.GetOpenOrderItemsAsync(_userId);
            var json = JsonSerializer.Serialize(res);
            return Json(json);
        }
        public async Task<JsonResult> AddOrderItem(int id)
        {
            _userId = _authService.GetLoginUserId();
            OperationResult res = await _orderApplication.AddOrderItemAsync(_userId, id);
            if(res.Success) await _orderUserPanelQuery.CheckOrderItemDataAsync(_userId);
            var json = JsonSerializer.Serialize(res);
            return Json(json);
        }
        public async Task<IActionResult> AddOrderAddress()
        {
            _userId = _authService.GetLoginUserId();
            bool ok = await _orderUserPanelQuery.HaveUserOpenOrderAsync(_userId);
            if(ok)
            {
                var model = _userAddressUserPanelQuery.GetAddresseForUserPanel(_userId);
                return PartialView("AddOrderAddress", model);
            }
            return NotFound();
        }
        [HttpPost] 
        public async Task<IActionResult> AddOrderAddress(CreateAddress model)
        {
            OperationResult res = new(false);
            _userId = _authService.GetLoginUserId();
            bool ok = await _orderUserPanelQuery.HaveUserOpenOrderAsync(_userId);
            if (!ok)
            {
                res.Message = "شما فاکتور بازی ندارید .";
            }
            else
            {
                if (_stateQuery.IsStateCorrect(model.StateId) == false)
                    res.Message = "لطفا یک استان انتخاب کنید";
                else if (_stateQuery.IsCityCorrect(model.StateId, model.CityId) == false)
                    res.Message = "لطفا یک شهر انتخاب کنید";
                else
                {
                    res = _userAddressApplication.Create(model, _userId);
                    if (res.Success)
                        res = await _orderApplication.CreateOrderAddressAsync(new CreateOrderAddress
                        {
                            AddressDetail = model.AddressDetail,
                            CityId = model.CityId,
                            FullName = model.FullName,
                            IranCode = model.IranCode,
                            Phone = model.Phone,    
                            PostalCode = model.PostalCode,
                            StateId = model.StateId,
                        }, _userId);
                }
            }
            var json = JsonSerializer.Serialize(res);
            return Json(json);
        }
        public async Task<bool> ChangeOrderAddress(int id)
        {
            _userId = _authService.GetLoginUserId();
            bool isAddressForUser = await _userAddressApplication.IsAddressForUser(id,_userId);
            if (!isAddressForUser) return false;
            CreateAddress model = await _userAddressApplication.GetAddressForAddToFactor(id);
            var res = await _orderApplication.CreateOrderAddressAsync(new CreateOrderAddress
            {
                AddressDetail = model.AddressDetail,
                CityId = model.CityId,
                FullName = model.FullName,
                IranCode = model.IranCode,
                Phone = model.Phone,
                PostalCode = model.PostalCode,
                StateId = model.StateId,
            }, _userId);
            return res.Success;
        }
        public async Task<IActionResult> ChoosePost()
        {
            _userId = _authService.GetLoginUserId();
            var model = await _orderUserPanelQuery.CalculatePostPrices(_userId);
            if(model == null) return NotFound();
            return PartialView("ChoosePost",model)
        }
    }
}
