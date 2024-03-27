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
            debugger;
            $.ajax({
                type: "Get",
                url: url1
            }).done(function (res) {

                debugger;
                if (res) {
                    AlertSweetTimer("عملیات موفق", "success", 3000);
                    setTimeout($(`#${deletedId}`).hide('slow'), 3000);
                }
                else {
                    AlertSweetTimer("عملیات نا موفق", "error", 3000);
                }
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