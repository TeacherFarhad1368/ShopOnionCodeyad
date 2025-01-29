const cookieCartName = "boloorShop-cart-items";
$(function () {
    UpdateBasket();
})
function AddToBasket(productId,productSellId, title,shopTitle, price, priceAfterOff, slug, imageName, amount) {
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
function DeleteFromBasket(productSellId) {
    let products = $.cookie(cookieCartName);

    if (products === undefined) {
        return;
    }
    else {
        products = JSON.parse(products);
    }
    const itemRemove = products.find(x => x.productSellId == productSellId);

    if (itemRemove !== undefined) {
        products.splice(itemRemove, 1);
        $.cookie(cookieCartName, JSON.stringify(products), {
            expires: 7, path: "/"
        });
        AlertSweet("عملیات موفق", "محصول از سبد خرید شما حذف شد .", "success");
        UpdateBasket();

    }
    else {
        return;
    }

}
function UpdateBasket() {
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
    if (products.length > 0) {

        var price = 0;
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
            var p = parseInt(x.priceAfterOff);
            var c = parseInt(x.count);
            price = price + (c * p);
        });
    }

    $("span#priceSumCart").text(separate(price));
}