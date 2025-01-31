const cookieCartName = "boloorShop-cart-items";
$(function () {
    UpdateBasket();
})
function AddToBasket(pId, psId, title, shopTitle, p, pAO, slug, imageName, amount) {
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
                            count
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
function plusCart(id,amount) {
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
    if (item.count >= amount) {
        AlertSweet("عملیات نا موفق","موجودی نداریم !!","error")
    }
    item.count = item.count + 1;
    $.cookie(cookieCartName, JSON.stringify(products), {
        expires: 7, path: "/"
    });
    AlertSweet("عملیات موفق", `یک عدد به محصول ${item.title} از فروشگاه ${item.shopTitle} اضافه شد`, "success")
}
function minusCart(id) {
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
        DeleteFromBasket
    }
    item.count = item.count - 1;
    $.cookie(cookieCartName, JSON.stringify(products), {
        expires: 7, path: "/"
    });
    AlertSweet("عملیات موفق", `یک عدد از محصول ${item.title} از فروشگاه ${item.shopTitle} کم شد`, "success")
}
function DeleteFromBasket(productSellId) {
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
                    UpdateBasket();

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
            var liProduct = ` <li>
                                <div class="basket-item">
                                    <button onclick="DeleteFromBasket('${x.productSellId}')" class="basket-item-remove"></button>
                                    <div class="basket-item-content">
                                        <div class="basket-item-image">
                                            <img alt="" src="${x.imageName}">
                                        </div>
                                        <div class="basket-item-details">
                                        <div class="basket-item-title">
                                             ${x.title} 
                                            </div>
                                            <div class="basket-item-params">
                                                <div class="basket-item-props">
                                                    <span> ${x.count} عدد</span>
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
                var trProduct = ` <tr class="checkout-item">
                                <td>
                                    <img src="assets/img/cart/1335154.jpg" alt="">
                                    <button class="checkout-btn-remove"></button>
                                </td>
                                <td>
                                    <h3 class="checkout-title">
                                       ${x.title} - ${x.shopTitle}
                                    </h3>
                                </td>
                                <td>${x.count} عدد</td>
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
    }
}
