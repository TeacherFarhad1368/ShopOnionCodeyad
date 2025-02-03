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
}
