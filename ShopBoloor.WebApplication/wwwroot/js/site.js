function isNumber(event) {
    var ASCIICode = (event.which) ? event.which : event.keyCode
    if (ASCIICode > 31 && (ASCIICode < 48 || ASCIICode > 57))
        return false;

    return true;
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
        $.ajax({
            type: "Post",
            url: "/Home/AddEmailUser",
            data: {email: mail}
        }).done(function (res) {

            debugger;
            if (res == "") {
                AlertSweetTimer( "ایمیل شما به موفقیت اضافه شد .",'success', 3000);
                $("input#inputEmailUser").val("");
            }
            else {
                AlertSweetTimer(res,'error' , 3000);
                mailValid.text(res);
            }
        });
    }
}