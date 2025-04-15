using Shared.Application;
using Shared.Domain.Enum;
using Shop.Application.Contract.OrderApplication.Command;
using Shop.Domain.OrderAgg;
using Shop.Domain.ProductAgg;
using Shop.Domain.ProductSellAgg;
using Shop.Domain.SellerAgg;

namespace Shop.Application.Services;
internal class OrderApplication : IOrderApplication
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository; 
    private readonly IProductSellRepository _productSellRepository; 
    private readonly ISellerRepository _sellerRepository;
    public OrderApplication(IOrderRepository orderRepository, IProductRepository productRepository,
        IProductSellRepository productSellRepository, ISellerRepository sellerRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository; 
        _productSellRepository = productSellRepository;
        _sellerRepository = sellerRepository;   
    }

    public async Task<bool> AddOrderDiscountAsync(int userId, int id, string title, int percent)
    {
        var order = await _orderRepository.GetOpenOrderForUserAsync(userId);
        order.AddDiscount(id, percent, title);
        return await _orderRepository.SaveAsync();
    }

    public async Task<bool> AddOrderSellerDiscountAsync(int userId, int sellerId, int discountId, string title, 
        int percent)
    {
        var order = await _orderRepository.GetOpenOrderForUserAsync(userId);
        if (order.OrderSellers.Any(s => s.SellerId == sellerId) == false) return false;
        var orderSeller = order.OrderSellers.Single(s => s.SellerId == sellerId);
        orderSeller.AddDiscount(discountId, percent, title);
        return await _orderRepository.SaveAsync();
    }

    public async Task CheckOrderEmpty(int userId)=>
        await _orderRepository.CheckOrderEmpty(userId); 

    public async Task<bool> DeleteOrderItemAsync(int id, int userId) =>
        await _orderRepository.DeleteOrderItemAsync(id,userId);

    public async Task<OperationResult> OrderItemMinus(int id, int userId)=>
        await _orderRepository.OrderItemMinus(id,userId);

    public async Task<OperationResult> OrderItemPlus(int id, int userId) =>
       await _orderRepository.OrderItemPlus(id, userId);

    public async Task<OperationResult> AddOrderItemAsync(int userId, int id)
    {
       var productSell = await _productSellRepository.GetByIdAsync(id);
        if (productSell.Amount < 1) return new OperationResult(false, "موجودی نداریم .");
        var order = await _orderRepository.GetOpenOrderForUserAsync(userId);
        if (order.OrderSellers.Any(o => o.SellerId == productSell.SellerId) == false)
        {
            OrderSeller orderSeller = new(productSell.SellerId);
            OrderItem orderItem = new(productSell.Id,1, productSell.Price, productSell.Price, productSell.Unit);
            orderSeller.AddOrderItem(orderItem);
            order.AddOrderSeller(orderSeller);
        }
        else
        {
            OrderSeller orderSeller = order.OrderSellers.Single(o => o.SellerId == productSell.SellerId);
            if (orderSeller.OrderItems.Any(i => i.ProductSellId == productSell.Id) == false)
            {
                OrderItem orderItem = new(productSell.Id, 1, productSell.Price, productSell.Price, productSell.Unit);
                orderSeller.AddOrderItem(orderItem);
            }
            else
            {
                OrderItem orderItem = orderSeller.OrderItems.Single(i => i.ProductSellId == productSell.Id);
                if (productSell.Amount < (orderItem.Count + 1)) return new OperationResult(false, "موجودی نداریم .");
                orderItem.PlusCount(1);
            }
            orderSeller.AddPostPrice(0, 0, "");
        }
        if (await _orderRepository.SaveAsync())
            return new(true);
        return new(false,ValidationMessages.SystemErrorMessage);
    }

    public async Task<bool> UbsertOpenOrderForUserAsync(int _userId, List<ShopCartViewModel> cart)
    {
        var order = await _orderRepository.GetOpenOrderForUserAsync(_userId); 
        foreach (var item in cart)
        {
            var productSell = await _productSellRepository.GetByIdAsync(item.productSellId);
             if(order.OrderSellers.Any(o=>o.SellerId == productSell.SellerId) == false)
            {
                OrderSeller orderSeller = new(productSell.SellerId);
                OrderItem orderItem = new (item.productSellId,item.count,item.price,item.priceAfterOff,item.unit);  
                orderSeller.AddOrderItem(orderItem); 
                order.AddOrderSeller(orderSeller);
            }
            else
            {
                OrderSeller orderSeller = order.OrderSellers.Single(o => o.SellerId == productSell.SellerId);
                if(orderSeller.OrderItems.Any(i=>i.ProductSellId == item.productSellId) == false)
                {
                    OrderItem orderItem = new(item.productSellId, item.count, item.price, item.priceAfterOff,item.unit);
                    orderSeller.AddOrderItem(orderItem);
                }
                else
                {
                    OrderItem orderItem = orderSeller.OrderItems.Single(i => i.ProductSellId == item.productSellId);
                    orderItem.PlusCount(item.count);    
                }
            }
        }
        return await _orderRepository.SaveAsync();
    }

    public async Task<OperationResult> CreateOrderAddressAsync(CreateOrderAddress model, int userId)
    {
        var order = await _orderRepository.GetOpenOrderForUserAsync(userId);
        if(order.OrderAddressId > 0)
        {
            OrderAddress address = await _orderRepository.GetOrderAddressByIdAsync(order.OrderAddressId);
            if (address != null)
            {
                address.Edit(model.StateId, model.CityId, model.AddressDetail, model.PostalCode, model.Phone, model.Phone, model.IranCode);
                foreach (var item in order.OrderSellers)
                    item.AddPostPrice(0, 0, "");
                if (await  _orderRepository.SaveAsync())
                    return new(true);
                else
                    return new(false, ValidationMessages.SystemErrorMessage);
            }
            else
            {
                OrderAddress orderAddress = new(model.StateId, model.CityId, model.AddressDetail, model.PostalCode, model.Phone, model.FullName, model.IranCode, order.Id);
                var key = await _orderRepository.CreateOrderaddressReturnKey(orderAddress);
                if (key > 0)
                {
                    order.ChangeAddress(key);
                    foreach (var item in order.OrderSellers)
                        item.AddPostPrice(0, 0, "");
                    if (await _orderRepository.SaveAsync())
                        return new(true);
                    else
                        return new(false, ValidationMessages.SystemErrorMessage);
                }
                else
                    return new(false, ValidationMessages.SystemErrorMessage);
            }
        }
        else
        {

            OrderAddress orderAddress = new(model.StateId, model.CityId, model.AddressDetail, model.PostalCode, model.Phone, model.FullName, model.IranCode, order.Id);
            var key = await _orderRepository.CreateOrderaddressReturnKey(orderAddress);
            if (key > 0)
            {
                order.ChangeAddress(key);
                foreach (var item in order.OrderSellers)
                    item.AddPostPrice(0, 0, "");
                if (await _orderRepository.SaveAsync())
                    return new(true);

                return new(false, ValidationMessages.SystemErrorMessage);
            }
            else
            {
                return new(false, ValidationMessages.SystemErrorMessage);
            }
        }
    }

    public async Task<bool> AddPostToOrdersellerAsync(int userId, int id, int postId, int price, string title)
    {
        var order = await _orderRepository.GetOpenOrderForUserAsync(userId);
        var seller = order.OrderSellers.SingleOrDefault(s => s.Id == id);
        if (seller == null) return false;
        seller.AddPostPrice(price,postId,title);
        return await _orderRepository.SaveAsync();
    }

    public async Task<OperationResult> ChangeOrderPayment(int userId, OrderPayment pay)
    {
        var order = await _orderRepository.GetOpenOrderForUserAsync(userId);
        order.ChangePayment(pay);
        if(await _orderRepository.SaveAsync()) return new(true);
        return new(false,ValidationMessages.SystemErrorMessage);
    }

    public async Task<int> PaymentSuccessOrderAsync(int userId, int price)
    {
        var order = await _orderRepository.GetOpenOrderForUserAsync(userId);
        if(order.PaymentPrice != price) return 0;   
        order.ChamgeStatus(OrderStatus.پرداخت_شده);
        foreach (var item in order.OrderSellers)
            item.ChangeStatus(OrderSellerStatus.پرداخت_شده);
        if (await _orderRepository.SaveAsync()) return order.Id;
        return 0;   
    }

    public async Task<bool> ChnageOrderSellerStatusBySellerAsync(int orderSellerId, OrderSellerStatus status, int userId)=>
        await _orderRepository.ChnageOrderSellerStatusBySellerAsync(orderSellerId, status, userId);

    public async Task<bool> CancelByAdminAsync(int id)
    {
        return await _orderRepository.CancelByAdminAsync(id);
    }

    public async Task<bool> CancelOrderSellersAsync(int id) =>
        await _orderRepository.CancelOrderSellersAsync(id);

    public async Task<bool> ImperfectOrderAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null || order.OrderStatus != OrderStatus.پرداخت_شده) return false;
        order.ChamgeStatus(OrderStatus.ناقص);
        return await _orderRepository.SaveAsync();
    }

    public async Task<bool> SendOrderAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null || order.OrderStatus != OrderStatus.پرداخت_شده) return false;
        order.ChamgeStatus(OrderStatus.ارسال_شده);
        return await _orderRepository.SaveAsync();
    }
}
