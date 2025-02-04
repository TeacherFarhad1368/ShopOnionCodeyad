using Discounts.Application.Contract.OrderDiscountApplication.Command;
using Discounts.Domain.OrderDiscountAgg;
using Shared.Application;
using Shared.Domain.Enum;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Discounts.Application.Services;
internal class OrderDiscountApplication : IOrderDiscountApplication
{
    private readonly IOrderDiscountRepository _orderDiscountRepository;

    public OrderDiscountApplication(IOrderDiscountRepository orderDiscountRepository)
    {
        _orderDiscountRepository = orderDiscountRepository;
    }

    public async Task<OperationResult> CreateAsync(CreateOrderDiscount command, OrderDiscountType type, int shopId)
    {
        DateTime startDate = command.StartDate.ToEnglishDateTime();
        DateTime endDate = command.EndDate.ToEnglishDateTime();
        if (endDate.Date < DateTime.Now.Date || endDate.Date < startDate.Date) return new(false, "تاریخ پایان باید حد اقل امروز باشد .");
        if (command.Percent < 1 || command.Percent > 99) return new(false, "درصد تخفیف باید از 1 تا 99 باشد . .");
        if(command.Count < 1) return new(false, "تعداد تخفیف باید بیشتر از 0 باشد . .");
        if(await _orderDiscountRepository.ExistByAsync(d=>d.Code == command.Code.Trim())) return new(false, "کد تخفیف تکراری است .");
        OrderDiscount discount = new(command.Percent, command.Title, command.Code, command.Count, type, startDate, endDate,shopId);
        if (await _orderDiscountRepository.CreateAsync(discount)) return new(true);
        return new(false,ValidationMessages.SystemErrorMessage);
    }

    public async Task<OperationResult> EditAsync(EditOrderDiscount command)
    {
        var orderDiscount = await _orderDiscountRepository.GetByIdAsync(command.Id);
        DateTime startDate = command.StartDate.ToEnglishDateTime();
        DateTime endDate = command.EndDate.ToEnglishDateTime();
        if (endDate.Date < DateTime.Now.Date || endDate.Date < startDate.Date) return new(false, "تاریخ پایان باید حد اقل امروز باشد .");
        if (command.Percent < 1 || command.Percent > 99) return new(false, "درصد تخفیف باید از 1 تا 99 باشد . .");
        if (command.Count < 1) return new(false, "تعداد تخفیف باید بیشتر از 0 باشد . .");
        if (await _orderDiscountRepository.ExistByAsync(d => d.Code == command.Code.Trim() && d.Id != command.Id)) return new(false, "کد تخفیف تکراری است .");
        orderDiscount.Edit(command.Percent, command.Title, command.Code, command.Count, startDate, endDate);
        if (await _orderDiscountRepository.SaveAsync()) return new(true);
        return new(false, ValidationMessages.SystemErrorMessage);
    }

    public async Task<OperationResult> EditBySellerAsync(EditOrderDiscount command, List<int> sellerIds)
    {
        var orderDiscount = await _orderDiscountRepository.GetByIdAsync(command.Id);
        if(!sellerIds.Any(s=>s == orderDiscount.ShopId)) return new(false, "تخفیف متعلق به شما نیست .");
        DateTime startDate = command.StartDate.ToEnglishDateTime();
        DateTime endDate = command.EndDate.ToEnglishDateTime();
        if (endDate.Date < DateTime.Now.Date || endDate.Date < startDate.Date) return new(false, "تاریخ پایان باید حد اقل امروز باشد .");
        if (command.Percent < 1 || command.Percent > 99) return new(false, "درصد تخفیف باید از 1 تا 99 باشد . .");
        if (command.Count < 1) return new(false, "تعداد تخفیف باید بیشتر از 0 باشد . .");
        if (await _orderDiscountRepository.ExistByAsync(d => d.Code == command.Code.Trim() && d.Id != command.Id)) return new(false, "کد تخفیف تکراری است .");
        orderDiscount.Edit(command.Percent, command.Title, command.Code, command.Count, startDate, endDate);
        if (await _orderDiscountRepository.SaveAsync()) return new(true);
        return new(false, ValidationMessages.SystemErrorMessage);
    }

    public async Task<EditOrderDiscount> GetForEditAsync(int id)
    {
        var discount = await _orderDiscountRepository.GetByIdAsync(id);
        if (discount == null || discount.Type == OrderDiscountType.OrderSeller) return null;
        return new EditOrderDiscount
        {
            Code = discount.Code,
            Count = discount.Count,
            EndDate = discount.EndDate.ToPersainDatePicker(),
            Id = id,
            Percent = discount.Percent, 
            Title = discount.Title,
            StartDate = discount.StartDate.ToPersainDatePicker()
        };
    }

    public EditOrderDiscount GetForEditBySeller(int id, List<int> sellerIds)
    {
        var discount = _orderDiscountRepository.GetById(id);
        if (discount == null || discount.Type == OrderDiscountType.Order || !sellerIds.Any(s => s == discount.ShopId)) return null;
        return new EditOrderDiscount
        {
            Code = discount.Code,
            Count = discount.Count,
            EndDate = discount.EndDate.ToPersainDatePicker(),
            Id = id,
            Percent = discount.Percent,
            Title = discount.Title,
            StartDate = discount.StartDate.ToPersainDatePicker()
        };
    }

    public async Task<OperationResultOrderDiscount> GetOrderDiscountForAddOrderdiscountAsync(string code)
    {
        var orderDiscount = await _orderDiscountRepository.GetByCodeAsync(code);
        if (orderDiscount == null )
            return new(false, $"تخفیفی با کد {code} یافت نشد .");
        if(orderDiscount.ShopId != 0)
            return new(false, $"تخفیفی با کد {code} یافت نشد .");
        if (orderDiscount.StartDate.Date > DateTime.Now.Date)
            return new(false, $"تاریخ شروع تخفیف {code} از {DateTime.Now.ToPersainDate()} است .");
        if (orderDiscount.EndDate.Date < DateTime.Now.Date)
            return new(false, $"  تخفیف {code} در تاریخ {DateTime.Now.ToPersainDate()} به اتمام رسیده است .");
        if (orderDiscount.Use == orderDiscount.Count)
            return new(false, $"تعداد استفاده از کد تخفیف {code} به اتمام رسیده است .");
        orderDiscount.UsePlus();
        await _orderDiscountRepository.SaveAsync();
        return new OperationResultOrderDiscount(true, $"کد تخفیف {code} با موفقیت اضافه شد .", $"تخفیف {orderDiscount.Title} با کد {orderDiscount.Code}", orderDiscount.Id, orderDiscount.Percent);
    }

    public async Task<OperationResultOrderDiscount> GetOrderDiscountForAddOrderSellerdiscountAsync(int id, string code)
    {
        var orderDiscount = await _orderDiscountRepository.GetByCodeAsync(code);
        if (orderDiscount == null || orderDiscount.ShopId != id)
            return new(false, $"تخفیفی با کد {code} یافت نشد .");
        if(orderDiscount.StartDate.Date > DateTime.Now.Date)
            return new(false, $"تاریخ شروع تخفیف {code} از {DateTime.Now.ToPersainDate()} است .");
        if(orderDiscount.EndDate.Date < DateTime.Now.Date)
            return new(false, $"  تخفیف {code} در تاریخ {DateTime.Now.ToPersainDate()} به اتمام رسیده است .");
        if (orderDiscount.Use == orderDiscount.Count)
            return new(false, $"تعداد استفاده از کد تخفیف {code} به اتمام رسیده است .");
        orderDiscount.UsePlus();
        await _orderDiscountRepository.SaveAsync();
        return new OperationResultOrderDiscount(true, $"کد تخفیف {code} با موفقیت اضافه شد .", $"تخفیف {orderDiscount.Title} با کد {orderDiscount.Code}", orderDiscount.Id, orderDiscount.Percent);
    }

    public async Task MinusUseAsync(int id)
    {
        var discount= await _orderDiscountRepository.GetByIdAsync(id);
        discount.UseMinus();
        await _orderDiscountRepository.SaveAsync();
    }

    public async Task<bool> UseOrderDiscount(int id)
    {
        var orderDiscount = await _orderDiscountRepository.GetByIdAsync(id);
        orderDiscount.UsePlus();
        return await _orderDiscountRepository.SaveAsync();
    }
}
