const cookieCartName = "boloorShop-cart-items";
function AddToBasket(productSellId, title,shopTitle, price, priceAfterOff, slug, imageName, amount) {
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
}