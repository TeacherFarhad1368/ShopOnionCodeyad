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
    let products =[];
    $.ajax({
        type: "Get",
        url: `/Auth/IsUserLogin`
    }).done(function (res) {
        if (res) {
            $.ajax({
                type: "Get",
                url: `/UserPanel/Order/OpenOrderItems`
            }).done(function (res) {
                products = JSON.parse(res);
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
                                    <button onclick="DeleteOrderItem('${x.productSellId}','${x.title}')" class="basket-item-remove"></button>
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
                        var p = parseInt(x.priceAfterOff);
                        var p1 = parseInt(x.price);
                        var c = parseInt(x.count);
                        priceAfterOff = priceAfterOff + (c * p);
                        price = price + (c * p1);
                    });
                    $("span#priceSumCart").text(separate(priceAfterOff));
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
            });
        }
        else {
            var route = window.location.pathname.toLowerCase();
            if ($.cookie(cookieCartName) !== undefined) {
                products = JSON.parse($.cookie(cookieCartName));
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
        }
    });
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
function AddToFactor(pId, psId, title, shopTitle) {
    var productSellId = parseInt(psId);
    swal.fire({
        title: "افزودن به فاکتور",
        text: `${title} از فروشگاه ${shopTitle} به فاکتور شما اضافه شود ؟`,
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: "اضافه شود ",
        cancelButtonText: "انصراف"
    })
        .then((willDelete) => {
            Loding();
            if (willDelete.isConfirmed) {
                $.ajax({
                    type: "Get",
                    url: `/UserPanel/Order/AddOrderItem/${productSellId}`
                })
                    .done(function (res) {
                        var model = JSON.parse(res);
                        if (model.Success) {
                            AlertSweetTimer("عملیات موفق", "success", 3000);
                            EndLoading();
                            UpdateBasket();
                        }
                        else {
                            AlertSweetTimer(model.Message, "error", 3000);
                            EndLoading();
                        }
                    });
            }
        });
}
function OpenFactorModal(url, title) {
    $.get(url, function (res) {
        //$("h4#myFactorModal").text(title);
        var content = $("div#myFactorModalBody");
        content.html(res);
        var titleModal = $("h4#ajax-modal-title");
        titleModal.text(title);
        GetStatesForSelectBox('StateId');
        openFactorAjaxModal();
    });
}
function OpenPostModal(url, title) {
    $.get(url, function (res) {
        var model = JSON.parse(res);
        if (model.success) {
            debugger;
            var content = $("div#myFactorModalBody");
            var titleModal = $("h4#ajax-modal-title");
            titleModal.text(title);
            var divParent = `<div class="row text-center" id="diveParent"></div>`;
            content.html(divParent);
            var parent = $("div#diveParent");
            var titleChoose = `<h5>
                میتوانید از لیست پست های زیر یکی را انتخاب کنید .
            </h5>`;
            parent.append(titleChoose);
            model.posts.forEach(x => {
                var div = ` <div class="w-50 d-flex flex-column align-items-center p-3">
                    <div class="w-75 m-1 rounded-1 p-3 border border-1">
                    <div class="w-100 d-flex flex-colomn justify-content-between align-items-center">
                        <span class="font-weight-bold">عنوان:</span>
                        <span class="font-weight-bold">${x.Title}</span>
                    </div>
                    <div class="w-100 d-flex flex-colomn justify-content-between align-items-center">
                        <span class="font-weight-bold">توضیح:</span>
                        <span class="font-weight-bold">${x.Status}</span>
                    </div>
                    <div class="w-100 d-flex flex-colomn justify-content-between align-items-center">
                        <span class="font-weight-bold">قیمت:</span>
                        <span class="font-weight-bold">${separate(x.Price)}</span>
                    </div>
                    <button type="button" class="btn btn-success btn-block" 
                            onclick="ChangeOrderSellerPost('${x.PostId}','${model.sellerId}','${x.Title}')">
                        انتخاب پست
                    </button>
                    </div>
                </div>`;
                parent.append(div);
            });
            var div1 = ` <div class="w-50 d-flex flex-column align-items-center p-3">
                    <div class="w-75 m-1 rounded-1 p-3 border border-1">
                    <div class="w-100 d-flex flex-colomn justify-content-between align-items-center">
                        <span class="font-weight-bold">عنوان:</span>
                        <span class="font-weight-bold">ارسال با تیباکس</span>
                    </div>
                    <div class="w-100 d-flex flex-colomn justify-content-between align-items-center">
                        <span class="font-weight-bold">توضیح:</span>
                        <span class="font-weight-bold">پردلخت درب منزل</span>
                    </div>
                    <div class="w-100 d-flex flex-colomn justify-content-between align-items-center">
                        <span class="font-weight-bold">قیمت:</span>
                        <span class="font-weight-bold">پردلخت درب منزل</span>
                    </div>
                    <button type="button" class="btn btn-success btn-block" 
                            onclick="ChangeOrderSellerPost('0','${model.sellerId}','تیباکس')">
                        انتخاب تیباکس
                    </button>
                    </div>
                </div>`;
            parent.append(div1);
        }
        else {
            AlertSweetTimer(model.message, error, 3000);
        }
        openFactorAjaxModal();
    });
}
function ChangeOrderSellerPost(postId, sellerId, title) {
    swal.fire({
        title: "انتخاب پست",
        text: `${title}به عنوان پست انتخاب شود؟`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: "انتخاب شود ",
        cancelButtonText: "انصراف"
    })
        .then((willDelete) => {
            if (willDelete.isConfirmed) {
                Loding();
                $.ajax({
                    type: "Post",
                    url: `/UserPanel/Order/ChoosePostPrice/${sellerId}`,
                    data: {
                        post: postId,
                        postTitle: title
                    }
                }).done(function (res) {
                    if (res) {
                        AlertSweetTimer("عملیات موفق", "success", 3000);
                        setTimeout(
                            () => { location.reload(); }
                            , 3000);
                    }
                    else {
                        AlertSweetTimer("عملیات نا موفق", "error", 3000);
                        EndLoading();
                    }
                });
            }
        });
}
function openFactorAjaxModal() {
    var modal = $("div#factorModal");
    modal.removeClass("close-modal-form");
    modal.addClass("show");
    modal.addClass("open-modal-form");
}
function closeFactorAjaxModal() {
    var content = $("div#myFactorModalBody");
    content.html("");
    var titleModal = $("h4#ajax-modal-title");
    titleModal.text("");
    var modal = $("div#factorModal");
    modal.addClass("close-modal-form");
    modal.removeClass("show");
    modal.removeClass("open-modal-form");
}
function AddOrderAddressToAddress() {
    var stateId = $("select#StateId").val();
    var cityId = $("select#CityId").val();
    var addressDetail = $("input#AddressDetail").val();
    var postalCode = $("input#PostalCode").val();
    var phone = $("input#Phone").val();
    var fullName = $("input#FullName").val();
    var iranCode = $("input#IranCode").val();

    var stateIdValid = $("span#StateIdValid");
    var cityIdValid = $("span#CityIdValid");
    var addressDetailValid = $("span#AddressDetailValid");
    var postalCodeValid = $("span#PostalCodeValid");
    var phoneValid = $("span#PhoneValid");
    var fullNameValid = $("span#FullNameValid");
    var iranCodeValid = $("span#IranCodeValid");
    if (parseInt(stateId) == 0) {
        stateIdValid.text("استان را انتخاب کنید");
    }
    else {
        stateIdValid.text("");
        if (parseInt(cityId) == 0) {
            cityIdValid.text("شهر را انتخاب کنید");
        }
        else {
            cityIdValid.text("");
            if (addressDetail.length < 10) {
                addressDetailValid.text("جزییات آدرس حد اقل 10 حرف دارد");
            }
            else {
                addressDetailValid.text("");
                if (postalCode.length < 10 || postalCode.length > 10) {
                    postalCodeValid.text("یک کد پستی 10 رقمی وارد کنید ");
                }
                else {
                    postalCodeValid.text("");
                    if (phone.length != 11) {
                        phoneValid.text("یک شماره تماس وارد کنید .");
                    }
                    else {
                        phoneValid.text("");
                    }
                    if (fullName === "" || fullName === null) {
                        fullNameValid.text("نام کامل  گیرنده را وارد کنید .");
                    }
                    else {
                        fullNameValid.text("");
                        if (iranCode !== null && iranCode !== "" && iranCode.length != 10) {
                            iranCodeValid.text("کد ملی باید 10 رقمی باشد .");
                        }
                        else {
                            iranCodeValid.text("");
                            Loding();
                            $.ajax({
                                type: "Post",
                                url: `/UserPanel/Order/AddOrderAddress`,
                                data: {
                                    StateId: stateId,
                                    CityId: cityId,
                                    AddressDetail: addressDetail,
                                    PostalCode: postalCode,
                                    Phone: phone,
                                    FullName: fullName,
                                    IranCode: iranCode
                                }
                            }).done(function (res) {
                                var model = JSON.parse(res);
                                if (model.Success) {
                                    AlertSweet("عملیات موفق", "آدرس با موفقیت به فاکتور شما اضافه شد .", "success");
                                    setTimeout(() => {
                                        location.reload();
                                    }, 3000);
                                }
                                else {
                                    $("span#ajax-userPanel-modal-valid").text(res.Message);
                                    EndLoading();
                                }
                            });
                        }
                    }
                }
            }
        }
    }
}
function ChangeOrderAddress(id, state, city, address, postalCode) {
    Swal.fire({
        title: "<strong>انتخاب <u> آدرس </u></strong>",
        icon: "question",
        html: `
        <p>از انتخاب آدرس اطمینان دارید ؟</p>
    <ul>
    <li>استان : ${state}</li>
    <li>شهر : ${city}</li>
    <li>جزییات آدرس : ${address}</li>
    <li>کد پستی : ${postalCode}</li>
    </ul>
  `,
        showCloseButton: true,
        showCancelButton: true,
        focusConfirm: false,
        confirmButtonText: `
    <i class="fa fa-thumbs-up"></i> آره !
  `,
        confirmButtonAriaLabel: "Thumbs up, great!",
        cancelButtonText: `
    <i class="fa fa-thumbs-down"></i> خیر
  `,
        cancelButtonAriaLabel: "Thumbs down"
    }).then((result) => {
        debugger;
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {
            $.ajax({
                type: "Get",
                url: `/UserPanel/Order/ChangeOrderAddress/${id}`
            }).done(function (res) {
                if (res) {
                    Loding();
                    Swal.fire("عملیات موفق!", "", "success");
                    setTimeout(() => {
                        location.reload();
                    }, 3000);
                }
                else {
                    Swal.fire("عملیات ناموفق!", "", "error");
                }
            });
        } else if (result.isDenied) {
            Swal.fire("آدرس برای فاکتور ثبت نشد .", "", "info");
        }
    });
}
function ChangeOrderPayment(payment, orderPayment) {
    if (payment == 'پرداخت_از_درگاه' && payment == orderPayment) {
        Loding();
        Swal.fire("پرداخت روی درگاه است .", "", "info");
        setTimeout(
            () => {
                EndLoading();
                location.reload();
            }
            , 2000);
    }
    else if (payment == 'پرداخت_از_کیف_پول' && payment == orderPayment) {
        Loding();
        Swal.fire("پرداخت روی کیف پول است .", "", "info");
        setTimeout(
            () => {

                EndLoading();
                location.reload();
            }
            , 2000);
    }
    else {
        Loding();
        $.ajax({
            type: "Post",
            url: `/UserPanel/Order/ChangePayment`,
            data: {pay:payment}
        }).done(function (res) {
            var model = JSON.parse(res);
            if (model.Success) {
                Swal.fire("عملیات موفق ", "", "success");
                setTimeout(
                    () => { location.reload(); }
                    , 3000);
            }
            else {
                AlertSweetTimer(model.Message, "error", 3000);
                EndLoading();
            }
        });
    }
}
function PaymentFactor() {
    swal.fire({
        title: "پرداخت فاکتور",
        text: `از پرداخت فاکتور اطمینان دارید ؟`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: "پرداخت شود ",
        cancelButtonText: "انصراف"
    })
        .then((willDelete) => {
            if (willDelete.isConfirmed) {
                Loding();
                $.ajax({
                    type: "Get",
                    url: `/UserPanel/Order/PaymentFactor`
                }).done(function (res) {
                    var model = JSON.parse(res);
                    if (model.Success) {
                        AlertSweetTimer(model.Message, "success", 3000);
                        if (model.Url === null || model.Url === "") {
                            setTimeout(
                                () => { location.reload() }
                                , 3000);
                        }
                        else {
                            setTimeout(
                                () => { location.href = model.Url; }
                                , 3000);
                        }
                       
                    }
                    else {
                        AlertSweetTimer(model.Message, "error", 3000);
                        if (model.Url === null || model.Url === "") {
                            setTimeout(
                                () => { location.reload() }
                                , 3000);
                        }
                        else {
                            setTimeout(
                                () => { location.href = model.Url; }
                                , 3000);
                        }
                        EndLoading();
                    }
                });
            }
        });
}