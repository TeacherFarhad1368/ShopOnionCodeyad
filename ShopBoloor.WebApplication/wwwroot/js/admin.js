function AjaxSweet(title1, text1, icon1, confirmButtonText1, cancelButtonText1,url1,deletedId) {
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

        else {
            location.reload();
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
function AjaxSweetInput(title1, confirmButtonText1, url1, deletedId) {
    Swal.fire({
        title: title1,
        input: "text",
        inputAttributes: {
            autocapitalize: "off"
        },
        showCancelButton: true,
        confirmButtonText: confirmButtonText1,
        cancelButtonText: 'انصراف',
        showLoaderOnConfirm: true,
        allowOutsideClick: () => !Swal.isLoading()
    }).then((result) => {
        if (result.isConfirmed) {
            Loding();
            $.ajax({
                type: "Get",
                url: url1 +  result.value 
            }).done(function (res) {
                if (res) {
                    AlertSweetTimer("عملیات موفق", "success", 3000);
                    setTimeout($(`#${deletedId}`).hide('slow'), 3000);

                }
                else {
                    AlertSweetTimer("عملیات نا موفق", "error", 3000);
                    setTimeout(function () {
                        location.reload();
                    }, 3000);
                }
                EndLoading();
            });
        }
    });
}
function makeSlug(source, destination) {

    var titleStr = $('#' + source).val();
    titleStr = titleStr.replace(/^\s+|\s+$/g, '');
    titleStr = titleStr.toLowerCase();
    titleStr = titleStr.replace(/[^a-z0-9_\s-ءاأإآؤئبتثجحخدذرزسشصضطظعغفقكلمنهويةى]#u/, '')
        .replace(/\s+/g, '-')
        .replace(/-+/g, '-');
    $('#' + destination).val(titleStr);
}
function ChooseParentBlogCategory(parentSelect, childSelect) {
    var id = $(`select#${parentSelect}`).val();
    if (id > 0) {
        $.ajax({
            type: "Get",
            url: `/Admin/Blog/GetCategories/${id}`
        }).done(function (res) {
            var model = [];
            var childEleman = $(`select#${childSelect}`);
            childEleman.html("");
            model = JSON.parse(res);
            var opt = `<option value=0 selected="selected">انتخاب زیر گروه</option>`;
            childEleman.append(opt);
            model.forEach(x => {
                var y = `<option value=${x.Id}> ${x.Title}</option>`;
                childEleman.append(y);
            })
        });
    }
}
function CheckBlogCategories(parentId, subId, childSelect) {
    var id = $(`input#${parentId}`).val();
    var subId = $(`input#${subId}`).val();
    if (id > 0) {
        $.ajax({
            type: "Get",
            url: `/Admin/Blog/GetCategories/${id}`
        }).done(function (res) {
            var model = [];
            var childEleman = $(`select#${childSelect}`);
            childEleman.html("");
            model = JSON.parse(res);
            if (subId > 0) {

                var opt = `<option value=0>انتخاب زیر گروه</option>`;
                childEleman.append(opt);
            }
            else {
                var opt = `<option value=0 selected="selected">انتخاب زیر گروه</option>`;
                childEleman.append(opt);
            }
            model.forEach(x => {
                if (x.Id == subId) {
                    var y = `<option value=${x.Id} selected="selcted"> ${x.Title}</option>`;
                    childEleman.append(y);
                }
                else {
                    var y = `<option value=${x.Id}> ${x.Title}</option>`;
                    childEleman.append(y);
                }
            })
        });
    }
}
function AjaxAdminGet(url, title) {
    $.get(url, function (res) {
        var content = $("div#modal-content-default-ajax");
        content.html(res);
        openAjaxModal();
        var titleModal = $("h4#ajax-modal-title");
        titleModal.text(title);
        if (url.includes('Discount')) {
            $("input#StartDate").persianDatepicker();
            $("input#EndDate").persianDatepicker();
        }
        var form = $("form#form-ajax-admin");
        $.validator.unobtrusive.parse(form);
    });
}
function openAjaxModal() {
    var modal = $("div#modal-default-ajax");
    modal.removeClass("close-form");
    modal.addClass("in");
    modal.addClass("open-form");
}
function closeAjaxModal() {
    var content = $("div#modal-content-default-ajax");
    content.html("");
    var modal = $("div#modal-default-ajax");
    modal.removeClass("in");
    modal.removeClass("open-form");
    modal.addClass("close-form");

}
function AjaxAdminSucceded(res) {
    debugger;
    var model = JSON.parse(res);
    if (model.Success) {
        closeAjaxModal();
        AlertSweetTimer("عملیات موفقیت آمیز بود .", "success", 3000);
        setTimeout(function () {
            location.reload();
        }, 3000);
    }
    else {
        var span = $("span#ajax-modal-valid");
        span.text(model.Message);
    }
}
function AjaxAdminFaild() {
    closeAjaxModal();
    AlertSweet("عملیات ناموفق", "خطای سیستمی !!!", "error");
}
function ChangePagination(page) {
    $("input#inputPageId").val(page);
    $("form#formSearchAdmin").submit();
}
function GetAdminMessages() {
    var parent = $("#UlMessageNotifications");
    var header = $("#UlMessageNotificationsHeader");
    var headerSpan = $("#UlMessageNotificationsHeaderspan");
    parent.html("");
    $.ajax({
        type: "Post",
        url: "/Admin/Message/GetMessageNotifications"
    }).done(function (res) {
        var model = [];
        model = JSON.parse(res);
        debugger;
        model.forEach(x => {
            var li = ` <li>
                                    <a href="/Admin/Message/Detail/${x.Id}">
                                        <div class="pull-right">
                                            <img src="${x.UserAvatar}" class="img-circle" alt="${x.FullName}">
                                        </div>
                                        <h4>
                                            ${x.FullName}
                                            <small><i class="fa fa-clock-o"></i>${x.CreationDate}</small>
                                        </h4>
                                        <p>${x.Message}</p>
                                    </a>
                                </li>`;
            parent.append(li);
        });
        header.text(`${model.length} پیام خوانده نشده`)
        headerSpan.text(model.length);
    });
}