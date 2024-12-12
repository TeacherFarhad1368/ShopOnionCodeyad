var products = [];
var stores = [];
$(function () {
    GetSellersForUser();
    UpdateProducts();
});
function GetSellersForUser() {
    $.ajax({
        type: "Get",
        url: `/UserPanel/Store/GetSellers/`
    }).done(function (res) {
        var SelectSellers = $("select#sellerId");
        SelectSellers.html("");
        var model = JSON.parse(res);
        var opt = `<option value=0>انتخاب فروشگاه</option>`;
        SelectSellers.append(opt);
        model.forEach(x => {
            var opt = `<option value=${x.Id}>${x.SellerName}</option>`;
            SelectSellers.append(opt);
        });
    });
}
function SelectSellerId() {
    var id = $("select#sellerId").val();
    products = [];
   
    var productStoteSelect = $("select#ProductStoreId");
    productStoteSelect.html("");
    if (id < 1) {
        $("span#sellerIdValid").text("فروشگاه را انتخاب کنید .");
        stores = [];
        UpdateProducts();
    }
    else {
        $("span#sellerIdValid").text("");
        $.ajax({
            type: "Get",
            url: `/UserPanel/Store/GetSellerProducts/${id}`
        }).done(function (res) {
            products = JSON.parse(res);

            if (products.length > 0) {
                products.forEach(x => {
                    const currentProduct = stores.find(s => s.ProductSellId == x.ProductSellId);
                    if (currentProduct === undefined) {
                        var opt = `<option value=${x.ProductSellId}> ${x.Unit} - ${x.ProductTitle} - ${separate(x.Price)} تومان</option>`;
                        productStoteSelect.append(opt);
                    }
                });
            }
            else {
                var opt = `<option value=0> این فروشگاه محصولی ندارد .</option>`;
                productStoteSelect.append(opt);
            }
            stores.forEach(x => {
                const currentProduct = products.find(s => s.ProductSellId == x.ProductSellId);
                if (currentProduct === undefined) {
                    const itemRemove = stores.find(s => s.ProductSellId == currentProduct.ProductSellId);
                    stores.splice(itemRemove, 1);
                }
            });
            UpdateProducts();
        });
    }
}
function CreateProductSellStore() {
    var ProductStoreId = $("select#ProductStoreId").val();
    var ProductStoreIdValid = $("span#ProductStoreIdValid");

    var ProductTypeId = $("select#ProductTypeId").val();
    var ProductTypeIdValid = $("span#ProductTypeIdValid");

    var ProductCountId = $("input#ProductCountId").val();
    var ProductCountIdValid = $("span#ProductCountIdValid");
    if (ProductStoreId === null || parseInt(ProductStoreId) < 1) {
        ProductStoreIdValid.text("محصول را انتخاب کنید .");

    }
    else {
        ProductStoreIdValid.text("");
        if (ProductTypeId === "0") {
            ProductTypeIdValid.text("نوع را انتخاب کنید .");
        }
        else {
            ProductTypeIdValid.text("");
            if (ProductCountId === "" || ProductCountId === null || parseInt(ProductCountId) < 1) {
                ProductCountIdValid.text("تعداد باید عدد مثبت باشد .");
            }
            else {
                ProductCountIdValid.text("");
                const currentProduct = stores.find(s => s.ProductSellId == ProductStoreId);
                if (currentProduct !== undefined) {
                    location.reload();
                }
                else {
                    const product = products.find(s => s.ProductSellId == ProductStoreId);
                    var store = {
                        ProductSellId: ProductStoreId,
                        Type: ProductTypeId,
                        Count: parseInt(ProductCountId),
                        ProductTitle: product.ProductTitle,
                        Unit: product.Unit,
                        Price: product.Price
                    }
                    stores.push(store);
                    $("input#ProductCountId").val(0);
                    UpdateProducts();
                    SelectSellerId();
                }
            }
        }
    }
}
function DeleteProductSell(id) {
    const currentProduct = stores.find(s => s.ProductSellId == id);
    if (currentProduct === undefined) {
        AlertSweet("عملیات ناموفق","محصول یافت نشد","error")
    }
    else {
        const itemRemove = stores.find(s => s.ProductSellId == id);
        stores.splice(itemRemove, 1);
        UpdateProducts();
        SelectSellerId();
    }
}
function UpdateProducts(){
    var body = $("tbody#tbodyProducts");
    body.html("");
    if (stores.length > 0) {
        stores.forEach(x => {
            var tr = `
                         <tr id="tr_@item.Id">
                              <td>
                                  ${x.ProductTitle}
                              </td>
                              <td>
                                  ${x.Unit}
                              </td>
                              <td>
                                  ${separate(x.Price)} تومان
                              </td>
                              <td>
                                  ${x.Count}
                              </td>
                              <td>
                                  <a class="btn btn-danger btn-sm" onclick="DeleteProductSell('${x.ProductSellId}')">
                                      حذف
                                  </a>
                              </td>
                          </tr>
                     `;
            body.append(tr);
        });
    }
    else {
        var tr = `
                   <tr>
                      <td colspan="5" class="text-center text-danger">
                          محصولی اضافه نشده
                      </td>
                  </tr>
                 `;
        body.append(tr);
    }
}
function CreateStoreProductSeller() {
    var sellerId = $("select#sellerId").val();
    var sellerIdValid = $("span#sellerIdValid");

    var description = $("textarea#storeDescription").val();
    var descriptionValid = $("span#storeDescriptionValid");

    var storeProductsValid = $("span#storeProductsValid");

    if (sellerId === null || sellerId === "" || sellerId === "0") {
        sellerIdValid.text("فروشگاه انتخاب نشده است");
    }
    else {
        sellerIdValid.text("");
        if (description === null || description === "" || description.length < 50) {
            descriptionValid.text("حد اقل 50 حرف برای توضیحات وارد کنید .");
        }
        else {
            descriptionValid.text("");
            if (stores.length < 1) {
                storeProductsValid.text("حد اقل یک محصول را باید وارد کنید .");
            }
            else {
                storeProductsValid.text("");
                var res = {
                    SellerId: parseInt(sellerId),
                    Description: description,
                    Products: stores
                };
                Swal.fire({
                    title: "ویرایش انبار",
                    text: "بعد از ویرایش موجودی ها لحاظ میشود و قابل ویرایش نیست . از این موجودی ها اطمینان دارید ؟",
                    icon: "question",
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: "بله ویرایش شود .",
                    cancelButtonText: "انصراف"
                }).then((result) => {
                    if (result.isConfirmed) {
                        Loding();
                        $.ajax({
                            type: "Post",
                            url: "/UserPanel/Store/Create",
                            data: {
                                model: JSON.stringify(res)
                            }
                        }).done(function (res) {

                            if (res) {
                                AlertSweetTimer("عملیات موفق", "success", 3000);
                                setTimeout(() => location.href= "/UserPanel/Store/Index", 3000);
                            }
                            else {
                                AlertSweetTimer("عملیات نا موفق", "error", 3000);
                            }
                            EndLoading();
                        });
                    }

                    else {
                        location.reload();
                    }
                })
            }
        }
    }
    
}