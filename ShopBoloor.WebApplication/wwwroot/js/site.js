$(function () {
    CheckWishLists();
});
function isNumber(event) {
    var ASCIICode = (event.which) ? event.which : event.keyCode
    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
        return false;

    return true;
}
function isValueNumber(val) {
    return !isNaN(parseInt(val));
}
//success
//question
//info
//warning
//error
function AlertSweet(title,message,icon) {
    Swal.fire(title,message,icon)
}
function AlertSweetTimer(title,icon,time) {
    Swal.fire({
        position: 'top-end',
        icon: icon,
        title: title,
        showConfirmButton: true,
        timer: time
    })
}
// seperate number
function separate(Number) {
    Number += '';
    Number = Number.replace(',', '');
    x = Number.split('.');
    y = x[0];
    z = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(y))
        y = y.replace(rgx, '$1' + ',' + '$2');
    return y + z;
}

//change src when choose picture
const toBase64 = file => new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = reject;
});
async function ReadImageUrlAjax(chooseId, destinationId) {
    var img = $(`img#${destinationId}`);
    const file = document.querySelector(`#${chooseId}`).files[0];
    var imageUrl = await toBase64(file);
    img.attr('src', imageUrl);
}
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('img#destinationImage').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}
$("input#chooseImage").change(function () {
    readURL(this);
});
function readURL1(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('img#destinationImage1').attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}
$("input#chooseImage1").change(function () {
    readURL1(this);
});
function ValidateEmail(email) {

    var validRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

    if (email.match(validRegex)) {

        return true;

    } else {
        return false;

    }

}
function AddEmailUser() {
    var mail = $("input#inputEmailUser").val();
    var mailValid = $("span#inputEmailUserValid");
    if (mail === null || mail === "" || ValidateEmail(mail) == false) {
        mailValid.text("لطفا یک ایمیل معتبر وارد کنید .");
    }
    else {
        mailValid.text("");
        Loding();
        $.ajax({
            type: "Post",
            url: "/Home/AddEmailUser",
            data: {email: mail}
        }).done(function (res) {

            if (res == "") {
                AlertSweetTimer( "ایمیل شما به موفقیت اضافه شد .",'success', 3000);
                $("input#inputEmailUser").val("");
            }
            else {
                AlertSweetTimer(res,'error' , 3000);
                mailValid.text(res);
            }
            EndLoading();
        });
    }
}
function ScroolToEleman(id) {
    $('html, body').animate({
        scrollTop: $(`#${id}`).offset().top
    }, 1000);
}
function copy(id) {
    var copyText = document.getElementById(id);
    copyText.select();
    copyText.setSelectionRange(0, 99999);
    document.execCommand("copy");
    AlertSweet("عملیات موفق", "متن مورد نظر کپی شد.","success");
}


function GetStatesForSelectBox(selectId) {
    $.ajax({
        type: "Get",
        url: "/Post/GetStates"
    }).done(function (res) {
        var list = [];
        var selectBox = $(`select#${selectId}`);
        list = JSON.parse(res);
        list.forEach(x => {
            var state = `<option value=${x.Id}>${x.Title}</option>`;
            selectBox.append(state);
        });
    });
}
function GetCitiesForSelectBox(selectparentId, selectChildId) {
    debugger;
    var stateId = $(`select#${selectparentId}`).val();
    var selectBox = $(`select#${selectChildId}`);
    selectBox.html("");
    $.ajax({
        type: "Get",
        url: `/Post/GetCities/${stateId}`
    }).done(function (res) {
        var list = [];
        list = JSON.parse(res);
        list.forEach(x => {
            var city = `<option value=${x.CityCode}>${x.Title}</option>`;
            selectBox.append(city);
        });
    });
}
function GetCitiesForSelectBoxForEdit(selectparentId, selectChildId, stateId, cityId) {
    $.ajax({
        type: "Get",
        url: "/Post/GetStates"
    }).done(function (res) {
        var list = [];
        var selectBox = $(`select#${selectparentId}`);
        list = JSON.parse(res);
        list.forEach(x => {
            if (parseInt(stateId) === x.Id) {
                var state = `<option selected="selected" value=${x.Id}>${x.Title}</option>`;
                selectBox.append(state);
            }
            else {
                var state = `<option value=${x.Id}>${x.Title}</option>`;
                selectBox.append(state);
            }
        });
    });
    var selectBox1 = $(`select#${selectChildId}`);
    selectBox1.html("");
    $.ajax({
        type: "Get",
        url: `/Post/GetCities/${stateId}`
    }).done(function (res) {
        var list1 = [];
        list1 = JSON.parse(res);
        list1.forEach(x => {
            debugger;
            if (cityId === x.CityCode) {
                var city = `<option selected="selected" value=${x.CityCode}>${x.Title}</option>`;
                selectBox1.append(city);
            }
            else {
                var city = `<option value=${x.CityCode}>${x.Title}</option>`;
                selectBox1.append(city);
            }
        });
    });
}
function CalculatePost() {
    var sourceId = $("select#SourceCityId").val();
    var destinationId = $("select#DestinationCityId").val();
    var weight = $("input#weight").val();
    var sourceValid = $("span#SourceCityIdValid");
    var destinationValid = $("span#DestinationCityIdValid");
    var weightValid = $("span#weightValid");
    var parentDiv = $("div#calculatePostDiv");
    parentDiv.html("");
    if (sourceId < 1 || sourceId === null || isNumber(sourceId) === false) {
        sourceValid.text("لطفا استان و شهر مبدا رو انتخاب کنید .");
    }
    else if (destinationId < 1 || destinationId === null || isNumber(destinationId) === false) {

        sourceValid.text("");
        destinationValid.text("لطفا استان و شهر مقصد رو انتخاب کنید .");
    }
    else if (weight === null || weight === "" || isNumber(weight) === false) {
        sourceValid.text("");
        destinationValid.text("");
        weightValid.text("لطفا وزن را بر اساس گرم وارد کنید .");
    }
    else {
        Loding();
        sourceValid.text("");
        destinationValid.text("");
        weightValid.text("");

        $.ajax({
            type: "Post",
            url: "/Post/Calculate",
            data: { sourceId: sourceId, destinationId: destinationId, weight: weight }
        }).done(function (res) {
            var model = [];
            model = JSON.parse(res);
            model.forEach(x => {
                var price = `<div class="d-flex flex-column justify-content-center align-item-center
                p-3 border border-1 border-success m-2">
                <h2 class="text-center">${x.Title}</h2>
                <h3 class="text-center">${x.Status}</h3>
                <p  class="text-center text-success"> ${separate(x.Price)} تومان</p>
                </div>`;
                parentDiv.append(price);
            });
            EndLoading();
            ScroolToEleman("calculatePostDiv");
        });
    }
}

function Loding() {
    $(".loading").fadeIn();
}
function EndLoading() {
    $(".loading").fadeOut();
}
$("form").submit(
    function () {
        $(".loading").fadeIn();
        setTimeout(function () {
            var s = $(".input-validation-error");
            if (s.length > 0) {
                $(".loading").fadeOut();
                $("form button[type=submit]").removeAttr("disabled");
            }
        }, 100, 1);
    }

);
function ChangePagination(page) {
    $("input#inputPageId").val(page);
    $("form#myForm").submit();
}
function chnageOrderBy(orderBy) {
    $("input#inputOrderBy").val(orderBy);
    $("form#myForm").submit();
}
function UbsertToWishList(id) {
    $.ajax({
        type: "Get",
        url: "/Home/CheckProductWishList/" + id
    }).done(function (res) {
        if (res) {
            Swal.fire({
                title: "حذف از علاقه مندی ها ؟ ",
                text: "این محصول در لیست علاقه مندی های شما موجود میباشد . آیا میخواهید از لیست علاقه مندی های شما حذف شود ؟ ",
                icon: "question",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: "حذف شود",
                cancelButtonText: "انصراف"
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: "Get",
                        url: "/Home/UbsertProductWishList/" + id
                    }).done(function (res) {
                        Loding();
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
            })
        }
        else {
            Swal.fire({
                title: "افزودن از علاقه مندی ها ؟ ",
                text: "این محصول در لیست علاقه مندی های شما موجود نیست . آیا میخواهید به لیست علاقه مندی های شما اضافه شود ؟ ",
                icon: "question",
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: "اضافه شود",
                cancelButtonText: "انصراف"
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: "Get",
                        url: "/Home/UbsertProductWishList/" + id
                    }).done(function (res) {
                        Loding();
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
            })
        }
    });
}
function CheckWishLists() {
    $.ajax({
        type: "Get",
        url: "/Home/GetWishListCount"
    }).done(function (res) {
        $("span#count-wish").text(res);
    });
}
function SearchAjax() {
    var filterInput = $("input#gsearchsimple").val();
    var img = ` <li class="list-group-item contsearch">
                    <img class="image-loader-search" src="/Images/icegif-1262.gif" />
                </li>`;
    var parent = $("ul#ul-parent-serach");
    parent.html("");
    parent.append(img);
    if (filterInput !== null && filterInput !== "") {
        $.ajax({
            type: "Post",
            url: "/Home/AjaxSearch",
            data: { filter: filterInput }
        }).done(function (res) {
            var model = JSON.parse(res);
            if (model.length > 0) {
                parent.html("");
                model.forEach(x => {
                    debugger;
                    var data = `
                     <li class="list-group-item contsearch">
                                <a href="${x.Url}" class="gsearch">
                                    <i>
                                    <img src="${x.ImageAddress}" height="30" />
                                    </i>
                                   ${x.Title}
                                </a>
                            </li>
                               `;
                    parent.append(data);
                });
            }
            else {
                parent.html(`
                     <li class="list-group-item contsearch">
                                <p>موردی یافت نشد .</p>
                            </li>
                               `);
            }
        });
    }
}