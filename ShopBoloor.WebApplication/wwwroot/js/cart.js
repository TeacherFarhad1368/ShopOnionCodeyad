const cookieCartName = "boloorShop-cart-items";
$(function () {
    UpdateBasket();
})
function AddToBasket(pId, psId, title, shopTitle, p, pAO, slug, imageName, amount,unit) {
    var productId = parseInt(pId);
    var productSellId = parseInt(psId);
    var price = parseInt(p);
    var priceAfterOff = parseInt(pAO);
    swal.fire({
        title: "افزودن به سبد خرید",
        text: `${title} از فروشگاه ${shopTitle} به سبد شما اضافه شود ؟`,
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: "اضافه شود ",
        cancelButtonText: "انصراف"
    })
        .then((willDelete) => {
            if (willDelete.isConfirmed) {
                let products = $.cookie(cookieCartName);

                if (products === undefined) {
                    products = [];
                }
                else {
                    products = JSON.parse(products);
                }
                const currentProduct = products.find(x => x.productSellId == productSellId);

                if (currentProduct !== undefined) {
                    if (currentProduct.count < amount) {
                        products.find(x => x.productSellId == productSellId).count = parseInt(currentProduct.count) + 1;
                        $.cookie(cookieCartName, JSON.stringify(products), {
                            expires: 7, path: "/"
                        });
                        AlertSweet("عملیات موفق", "محصول به سبد خرید اضافه شد .","success");
                    }
                    else {
                        AlertSweet("عملیات نا موفق !!", "این محصول در سبد شما موجود است و بیشتر موجودی نداریم .","error");
                    }

                }
                else {

                    var count = 1;
                    if (amount > 0) {
                        const product = {
                            productId,
                            productSellId,
                            title,
                            shopTitle,
                            slug,
                            imageName,
                            price,
                            priceAfterOff,
                            count,
                            unit
                        }
                        products.push(product);
                        $.cookie(cookieCartName, JSON.stringify(products), {
                            expires: 7, path: "/"

                        });
                        AlertSweet("عملیات موفق", "محصول به سبد خرید اضافه شد .", "success");
                    }
                    else {
                        AlertSweet("عملیات نا موفق !!", "موجودی نداریم .", "error");
                    }
                }

                UpdateBasket();
            }
        });

}
function plusCart(id) {
    var route = window.location.pathname.toLowerCase();
    let products = $.cookie(cookieCartName);

    if (products === undefined) {
        return;
    }
    else {
        products = JSON.parse(products);
    }
    const item = products.find(x => x.productSellId == id);
    if (item === undefined) {
        return;
    }
    item.count = item.count + 1;
    $.cookie(cookieCartName, JSON.stringify(products), {
        expires: 7, path: "/"
    });
    debugger;
    $(`span#liliCount_${id}`).text(`${item.count}`);
    $(`span#divCount_${id}`).text(`${item.count}`);
    var price = 0;
    var priceAfterOff = 0;
    products.forEach(x => {
        var p = parseInt(x.priceAfterOff);
        var p1 = parseInt(x.price);
        var c = parseInt(x.count);
        priceAfterOff = priceAfterOff + (c * p);
        price = price + (c * p1);
    });
    $("span#priceSumCart").text(separate(priceAfterOff));
    $("span#cartBasketCount").text(products.length);
    if (route === "/cart") {
        $("span#sumPrice").text(`${separate(price)} تومان`);
        $("span#sumPriceAfterOff").text(`${separate(priceAfterOff)} `);
        $("span#countProduct").text(`مبلغ کل (${products.length} کالا`);
    }
}
function minusCart(id) {
    var route = window.location.pathname.toLowerCase();
    let products = $.cookie(cookieCartName);

    if (products === undefined) {
        return;
    }
    else {
        products = JSON.parse(products);
    }
    const item = products.find(x => x.productSellId == id);
    if (item === undefined) {
        return;
    }
    if (item.count == 1) {
        DeleteFromBasket(id);
    }
    else {

        item.count = item.count - 1;
        $.cookie(cookieCartName, JSON.stringify(products), {
            expires: 7, path: "/"
        });
        $(`span#liliCount_${id}`).text(item.count);
        $(`span#divCount_${id}`).text(item.count);
        var price = 0;
        var priceAfterOff = 0;
        products.forEach(x => {
            var p = parseInt(x.priceAfterOff);
            var p1 = parseInt(x.price);
            var c = parseInt(x.count);
            priceAfterOff = priceAfterOff + (c * p);
            price = price + (c * p1);
        });
        $("span#priceSumCart").text(separate(priceAfterOff));
        $("span#cartBasketCount").text(products.length);
        if (route === "/cart") {
            $("span#sumPrice").text(`${separate(price)} تومان`);
            $("span#sumPriceAfterOff").text(`${separate(priceAfterOff)} `);
            $("span#countProduct").text(`مبلغ کل (${products.length} کالا`);
        }
    }
}
function DeleteFromBasket(productSellId) {
    var route = window.location.pathname.toLowerCase();
    let products = $.cookie(cookieCartName);

    if (products === undefined) {
        return;
    }
    else {
        products = JSON.parse(products);
    }
    const itemRemove = products.find(x => x.productSellId == productSellId);
    if (itemRemove === undefined) {
        return;
    }
    swal.fire({
        title: "حذف از سبد خرید",
        text: `${itemRemove.title} از فروشگاه ${itemRemove.shopTitle} از سبد شما حذف شود ؟`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: "حذف شود ",
        cancelButtonText: "انصراف"
    })
        .then((willDelete) => {
            if (willDelete.isConfirmed) {
               
                if (itemRemove !== undefined) {
                    products.splice(itemRemove, 1);
                    $.cookie(cookieCartName, JSON.stringify(products), {
                        expires: 7, path: "/"
                    });
                    AlertSweet("عملیات موفق", "محصول از سبد خرید شما حذف شد .", "success");
                    if (products.length == 0) {
                        $("div#basketTotalCart").addClass("d-none");
                        $("a#linkCart").addClass("d-none");

                    }
                    // UpdateBasket();
                    var price = 0;
                    var priceAfterOff = 0;
                    products.forEach(x => {
                        var p = parseInt(x.priceAfterOff);
                        var p1 = parseInt(x.price);
                        var c = parseInt(x.count);
                        priceAfterOff = priceAfterOff + (c * p);
                        price = price + (c * p1);
                    });
                    $("span#priceSumCart").text(separate(priceAfterOff));
                    $(`li#lili_${productSellId}`).hide('slow');
                    $("span#cartBasketCount").text(products.length);
                    if (route === "/cart") {
                        $("span#sumPrice").text(`${separate(price)} تومان`);
                        $("span#sumPriceAfterOff").text(`${separate(priceAfterOff)} `);
                        $("span#countProduct").text(`مبلغ کل (${products.length} کالا`);
                        $(`tr#tr_${productSellId}`).hide('slow');
                    }
                }
                else {
                    return;
                }
            }
        });
    

}
function UpdateBasket() {
    debugger;
    var route = window.location.pathname.toLowerCase();
    let products = $.cookie(cookieCartName);

    if (products === undefined) {
        products = [];
    }
    else {
        products = JSON.parse(products);
    }
    var count = products.length;
    $("span#cartBasketCount").text(count);
    $("ul#ParentCartProducts").html("");
    if (route === "/cart") {
        $("tbody#orderItemsTable").html("");
    }
    if (products.length > 0) {
        $("div#basketTotalCart").removeClass("d-none");
        $("a#linkCart").removeClass("d-none");
        var price = 0;
        var priceAfterOff = 0;
        products.forEach(x => {
            var liProduct = ` <li id="lili_${x.productSellId}">
                                <div class="basket-item">
                                    <button onclick="DeleteFromBasket('${x.productSellId}')" class="basket-item-remove"></button>
                                    <div class="basket-item-content">
                                        <div class="basket-item-image">
                                            <img alt="" src="${x.imageName}">
                                        </div>
                                        <div class="basket-item-details">
                                        <div class="basket-item-title">
                                             ${x.title} - ${x.unit}
                                            </div>
                                            <div class="basket-item-params">
                                                <div class="basket-item-props">
                                                    <span id="liliCount_${x.productSellId}"> ${x.count} عدد</span>
                                                    <span> ${x.shopTitle}</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </li>`;
            $("ul#ParentCartProducts").append(liProduct);
            if (route === "/cart") {
                var priceTd = `<td><span class="text-success">${separate(x.price)} تومان</span></td>`;
                if (x.price > x.priceAfterOff) {
                    priceTd = `<td><del class="text-danger">${separate(x.price)} </del>
                    <br />
                    <span class="text-success">${separate(x.priceAfterOff)} تومان</span>
                    </td>`;
                }
                var trProduct = ` <tr class="checkout-item" id="tr_${x.productSellId}">
                                <td>
                                    <img src="assets/img/cart/1335154.jpg" alt="">
                                    <button  onclick="DeleteFromBasket('${x.productSellId}')" class="checkout-btn-remove"></button>
                                </td>
                                <td>
                                    <h3 class="checkout-title">
                                       ${x.title} - ${x.unit} - ${x.shopTitle}
                                    </h3>
                                </td>
                                <td>
                                <div class="d-flex justify-content-center align-items-center">
                                
                                <a class="btn btn-sm btn-success mx-1"  onclick="plusCart('${x.productSellId}')">
                                <i class="fa fa-plus"></i>
                                </a>
                                <span id="divCount_${x.productSellId}">${x.count}</span>
                                 <a class="btn btn-sm btn-danger mx-1"  onclick="minusCart('${x.productSellId}')">
                                <i class="fa fa-minus"></i>
                                </a>
                                </div>
                                </td>
                                ${priceTd}
                            </tr>`;
                $("tbody#orderItemsTable").append(trProduct);
            }
            var p = parseInt(x.priceAfterOff);
            var p1 = parseInt(x.price);
            var c = parseInt(x.count);
            priceAfterOff = priceAfterOff + (c * p);
            price = price + (c * p1);
        });
        $("span#priceSumCart").text(separate(priceAfterOff));
        if (route === "/cart") {
            $("span#sumPrice").text(`${separate(price)} تومان`);
            $("span#sumPriceAfterOff").text(`${separate(priceAfterOff)} `);
            $("span#countProduct").text(`مبلغ کل (${count} کالا`);
        }
    }
    else {
        var liProduct = ` <li>
                                <div class="basket-item">
                                    <div class="basket-item-content">
                                        <p class="text-danger">سبد خرید خالی است</p>
                                    </div>
                                </div>
                            </li>`;
        $("ul#ParentCartProducts").append(liProduct);
        $("span#priceSumCart").text(separate(priceAfterOff));
        if (route === "/cart") {
            $("span#sumPrice").text(`0 تومان`);
            $("span#sumPriceAfterOff").text(`0`);
            $("span#countProduct").text(`مبلغ کل (0 کالا`);
            var trProduct = ` <tr class="checkout-item" id="tr_${x.productSellId}">
                                <td colspan="2">
                                  <div class="alert alert-warning">'
                                  <h4>سبد خرید خالی است</h4>
                                  </div>
                                </td>
                                <td  colspan="2">
                                <a class="btn btn-info" href="/shop">
                                فروشگاه
                                </a>
                                </td>
                            </tr>`;
            $("tbody#orderItemsTable").append(trProduct);
        }
    }
}
function DeleteOrderItem(id, title) {
    swal.fire({
        title: "حذف از فاکتور",
        text: `${title} از فاکتور شما حذف شود ؟`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: "حذف شود ",
        cancelButtonText: "انصراف"
    })
        .then((willDelete) => {
            if (willDelete.isConfirmed) {
                Loding();
                $.ajax({
                    type: "Get",
                    url: `/UserPanel/Order/DeleteOrderItem/${id}`
                }).done(function (res) {
                    if (res) {
                        AlertSweetTimer("عملیات موفق", "success", 3000);
                    }
                    else {
                        AlertSweetTimer("عملیات نا موفق", "error", 3000);
                    }
                    setTimeout(
                        () => { location.reload(); }
                        , 3000);
                });
            }
        });
}
function OrderItemMinus(id) {
    $.ajax({
        type: "Get",
        url: `/UserPanel/Order/OrderItemMinus/${id}`
    }).done(function (res) {
        var model = JSON.parse(res);
        if (!model.Success) {
            AlertSweetTimer(model.Message, "error", 3000);
            setTimeout(
                () => { location.reload(); }
                , 3000);
        }
        else {
            location.reload();
        }
    });
}
function OrderItemPlus(id) {
    $.ajax({
        type: "Get",
        url: `/UserPanel/Order/OrderItemPlus/${id}`
    }).done(function (res) {
        var model = JSON.parse(res);
        if (!model.Success) {
            AlertSweetTimer(model.Message, "error", 3000);
            setTimeout(
                () => { location.reload(); }
                , 3000);
        }
        else {
            location.reload();
        }
    });
}
function AddOrderSellerDiscount(id) {
    var codeInput = $(`input#discountCode_${id}`);
    if (codeInput === undefined || codeInput.val() === null || codeInput.val() === "") {
        AlertSweetTimer("لطفا کد تخفیف را وارد کنید .", "error", 2000);
    }
    else {
        $.ajax({
            type: "Post",
            url: `/UserPanel/Order/AddOrderSellerDiscount/${id}`,
            data: {
                code: codeInput.val()
            }
        }).done(function (res) {
            var model = JSON.parse(res);
            if (!model.Success) {
                AlertSweetTimer(model.Message, "error", 3000);
            }
            else {
                AlertSweetTimer(model.Message, "success", 3000);
            }
            setTimeout(
                () => { location.reload(); }
                , 3000);
        });
    }
}
function AddOrderDiscount() {
    var inputCode = $("input#orderDiscountCode");
    var spanText = $("span#orderDiscountCodeValid");
    if (inputCode === undefined || inputCode.val() === null || inputCode.val() === "") {
        spanText.text("لطفا کد تخفیف را وارد کنید .");
    }
    else {
        spanText.text("");
        $.ajax({
            type: "Post",
            url: `/UserPanel/Order/AddOrderDiscount`,
            data: {
                code: inputCode.val()
            }
        }).done(function (res) {
            var model = JSON.parse(res);
            if (!model.Success) {
                spanText.text(model.Message);
            }
            else {
                AlertSweetTimer(model.Message, "success", 3000);
                setTimeout(
                    () => { location.reload(); }
                    , 3000);
            }
        });
    }
}