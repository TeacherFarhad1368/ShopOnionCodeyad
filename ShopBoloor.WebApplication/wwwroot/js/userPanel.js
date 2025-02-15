function AjaxSweet(title1, text1, icon1, confirmButtonText1, cancelButtonText1, url1, deletedId) {
    Swal.fire({
        title: title1,
        text: text1,
        icon: icon1,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: confirmButtonText1,
        cancelButtonText: cancelButtonText1
    }).then((result) => {
        if (result.isConfirmed) {
            Loding();
            $.ajax({
                type: "Get",
                url: url1
            }).done(function (res) {

                if (res) {
                    AlertSweetTimer("عملیات موفق", "success", 3000);
                    setTimeout($(`#${deletedId}`).hide('slow'), 3000);
                }
                else {
                    AlertSweetTimer("عملیات نا موفق", "error", 3000);
                }
                EndLoading();
            });
        }
    })
}
function AjaxSweetNotDelete(title1, text1, icon1, confirmButtonText1, cancelButtonText1, url1) {
    Swal.fire({
        title: title1,
        text: text1,
        icon: icon1,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: confirmButtonText1,
        cancelButtonText: cancelButtonText1
    }).then((result) => {
        if (result.isConfirmed) {
            Loding();
            $.ajax({
                type: "Get",
                url: url1
            }).done(function (res) {
                if (res) {
                    AlertSweetTimer("عملیات موفق", "success", 3000);
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
function AjaxSweetRefresh(title1, text1, icon1, confirmButtonText1, cancelButtonText1, url1) {
    Swal.fire({
        title: title1,
        text: text1,
        icon: icon1,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: confirmButtonText1,
        cancelButtonText: cancelButtonText1
    }).then((result) => {
        if (result.isConfirmed) {
            Loding();
            $.ajax({
                type: "Get",
                url: url1
            }).done(function (res) {
                if (res) {
                    AlertSweetTimer("عملیات موفق", "success", 3000);
                    setTimeout(function () {
                        location.reload();
                    }, 3000);

                }
                else {
                    AlertSweetTimer("عملیات نا موفق", "error", 3000);
                    setTimeout(function () {
                        location.reload();
                    }, 3000);
                }

            });
        }
        else {
            location.reload();
        }
    })
}
function CreateTransaction() {
    var price = $("input#price");
    var priceValid = $("span#price-validation");
    var portal = $("input#portal");
    var portalValid = $("span#portal-validation");
    if (price.val() < 1000) {
        priceValid.text("مبلغ باید بیشتر از 999 تومان باشد ")
    }
    else {
        priceValid.text("");
            $("form#form-transaction").submit();
        }
}
function GetCategoriesForAddProductSell() {
    var selectParent = $("select#CategoryId");
    $.ajax({
        type: "Post",
        url: "/UserPanel/Product/Categories/0"
    }).done(function (res) {
        selectParent.html("");
        var model = JSON.parse(res);
        var cate1 = `<option value=${0}>انتخاب سردسته</option>`;
        selectParent.append(cate1);
        for (var i = 0; i < model.length; i++) {
            var cate = `<option value=${model[i].Id}>${model[i].Title}</option>`;
            selectParent.append(cate);
        }
        selectParent.select2();
    });
}
function GetSubCategoriesForAddProductSell() {
    var selectChild = $("select#SubCategoryId");
    selectChild.html("");
    var id = $("select#CategoryId").val();
    if (id === 0 || id === "0" || id === null || id === undefined) {
        $("span#CategoryIdValid").text("یک سردسته انتخاب کنید .");
    }
    else {
        $("span#CategoryIdValid").text("");
      
        $.ajax({
            type: "Post",
            url: `/UserPanel/Product/Categories/${id}`
        }).done(function (res) {
           
            debugger;
            var model = JSON.parse(res);
            var cate1 = `<option value=${id}> محصولات این سردسته</option>`;
            selectChild.append(cate1);
            for (var i = 0; i < model.length; i++) {
                var cate = `<option value=${model[i].Id}>${model[i].Title}</option>`;
                selectChild.append(cate);
            }
            selectChild.select2();
        });
    }
    
}
function GetProductsFotCategory() {
    var selectProduct = $("select#ProductId");
    selectProduct.html("");
    var categoryId = $("select#SubCategoryId").val();
    if (categoryId === 0 || categoryId === "0" || categoryId === null || categoryId === undefined) {
        $("span#SubCategoryIdValid").text("یک دسته بندی انتخاب کنید .");
    }
    else {
        $("span#SubCategoryIdValid").text("");

        $.ajax({
            type: "Post",
            url: `/UserPanel/Product/GetProducts/${categoryId}`
        }).done(function (res) {

            debugger;
            var model = JSON.parse(res);
            var cate1 = `<option value=0> انتخاب محصول</option>`;
            selectProduct.append(cate1);
            for (var i = 0; i < model.length; i++) {
                var cate = `<option value=${model[i].Id}>${model[i].Title}</option>`;
                selectProduct.append(cate);
            }
            selectProduct.select2();
        });
    }
}
function OpenAjaxModal(url, title) {
    $.get(url, function (res) {
        $("h4#myuserPanelModal").text(title);
        var content = $("div#myuserPanelModalBody");
        content.html(res);
        var titleModal = $("h4#ajax-modal-title");
        titleModal.text(title);

        if (url.includes('CreateDiscount')) {
            $.ajax({
                type: "Get",
                url: '/UserPanel/Seller/GetSellersForAddDiscount'
            }).done(function (result) {
                debugger;
                var model = JSON.parse(result);
                var xxx = $("select#shopId");
                xxx.html(`<option value="0" >انتخاب فروشگاه</option>`);
                model.forEach(x => {
                    var x = `<option value="${x.Id}" >${x.Title}</option>`;
                    xxx.append(x);
                });
            });
        }
        openUserPanelAjaxModal();
        var form = $("form#form-ajax-userPanel");
        $.validator.unobtrusive.parse(form);
        if (url.includes('Discount')) {
            $("input#StartDate").persianDatepicker();
            $("input#EndDate").persianDatepicker();
        }
    });
}
function OpenFactorModal(url, title) {
    $.get(url, function (res) {
        $("h4#myuserPanelModal").text(title);
        var content = $("div#myuserPanelModalBody");
        content.html(res);
        var titleModal = $("h4#ajax-modal-title");
        titleModal.text(title);

        openUserPanelAjaxModal();
    });
}
function openUserPanelAjaxModal() {
    var modal = $("div#userPanelModal");
    modal.removeClass("close-modal-form");
    modal.addClass("show");
    modal.addClass("open-modal-form");
}
function closeUserPanelAjaxModal() {
    var content = $("div#myuserPanelModalBody");
    content.html("");
    var titleModal = $("h4#ajax-modal-title");
    titleModal.text("");
    var modal = $("div#userPanelModal");
    modal.addClass("close-modal-form");
    modal.removeClass("show");
    modal.removeClass("open-modal-form");
}
function AjaxUserPanelSucceded(model) {
    debugger;
    var res = JSON.parse(model);
    if (res.Success) {
        closeUserPanelAjaxModal();
        AlertSweetTimer("عملیات موفقیت آمیز بود .", "success", 3000);
        setTimeout(function () {
            location.reload();
        }, 3000);
    }
    else {
        var span = $("span#ajax-userPanel-modal-valid");
        span.text(res.Message);
    }
}
function AjaxUserPanelFaild() {
    closeUserPanelAjaxModal();
    AlertSweet("عملیات ناموفق", "خطای سیستمی !!!", "error");
}