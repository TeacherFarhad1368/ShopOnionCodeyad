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