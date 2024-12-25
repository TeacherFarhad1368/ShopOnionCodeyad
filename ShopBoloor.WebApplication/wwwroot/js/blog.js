function ChangeTabBlog(currentAttrValue) {
    $('li#' + currentAttrValue).addClass('tabs__item--active').siblings().removeClass('tabs__item--active');
    $('div#' + currentAttrValue).addClass('tabs__content-pane--active').siblings().removeClass('tabs__content-pane--active');
}
function GetBestBlogs() {
    $.ajax({
        type: "Get",
        url: "/GetBestBlogs"
    }).done(function (res) {
        var model = [];
        model = JSON.parse(res);
        var parent = $("ul#parentBestBlogUL");
        var childs = $("div#childBestBlogDiv");
        for (var i = 0; i < model.length; i++) {
            if (i == 0) {
                var item = ` <li class="tabs__item tabs__item--active" id="parent_${model[i].Id}">
                                <a onclick="ChangeTabBlog('parent_${model[i].Id}')" class="tabs__trigger">${model[i].CategoryTitle}</a>
                            </li>`;
                parent.append(item);
            }
            else {
                var item1 = ` 
                            <li class="tabs__item" id="parent_${model[i].Id}">
                                 <a onclick="ChangeTabBlog('parent_${model[i].Id}')" class="tabs__trigger">${model[i].CategoryTitle}</a>
                            </li>`;

                parent.append(item1);
            }
           
        }
        
        for (var i = 0; i < model.length; i++) {
            if (i == 0) {
                var child = `
                    <div class="tabs__content-pane tabs__content-pane--active" id="parent_${model[i].Id}">
                      <div class="row card-row" id="childesDiv_${model[i].Id}">
                        </div>
                    </div>`;
                childs.append(child);
            }
            else {
                var child1 = `
                    <div class="tabs__content-pane" id="parent_${model[i].Id}">
                     <div class="row card-row" id="childesDiv_${model[i].Id}">
                        </div>
                    </div>`;

                childs.append(child1);
            }
            var blogParent = $(`div#childesDiv_${model[i].Id}`);
            for (var j = 0; j < model[i].Blogs.length; j++) {
                var blog = `<div class="col-md-6">
                                <article class="entry card">
                                    <div class="entry__img-holder card__img-holder">
                                        <a href="/blog/${model[i].Blogs[j].Slug}">
                                            <div class="thumb-container thumb-70">
                                                <img data-src="${model[i].Blogs[j].ImageName}" src="${model[i].Blogs[j].ImageName}"
                                                class="entry__img lazyload" alt="${model[i].Blogs[j].ImageAlt}" />
                                            </div>
                                        </a>
                                        <a href="/Blogs/${model[i].CategorySlug}" class="entry__meta-category entry__meta-category--label entry__meta-category--align-in-corner entry__meta-category--violet">${model[i].CategoryTitle}</a>
                                    </div>

                                    <div class="entry__body card__body">
                                        <div class="entry__header">

                                            <h2 class="entry__title">
                                                <a href="/blog/${model[i].Blogs[j].Slug}">${model[i].Blogs[j].Title}</a>
                                            </h2>
                                            <ul class="entry__meta">
                                                <li class="entry__meta-author">
                                                    <span>نویسنده:</span>
                                                    <a>${model[i].Blogs[j].Writer}</a>
                                                </li>
                                                <li class="entry__meta-date">
                                                   ${model[i].Blogs[j].CreationDate}
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="entry__excerpt">
                                            <p>             
                                            ${model[i].Blogs[j].ShortDescription}
                                            </p>
                                        </div>
                                    </div>
                                </article>
                            </div>`;
                blogParent.append(blog);
            }

        }

    });
}

function SendContactMessage() {
    var fullName = $("input#fullNameContact").val();
    var email = $("input#emailContact").val();
    var subject = $("input#subjectContact").val();
    var message = $("textarea#messageContact").val();
    var fullNameValid = $("span#fullNameContactValid");
    var emailValid = $("span#emailContactValid");
    var subjectValid = $("span#subjectContactValid");
    var messageValid = $("span#messageContactValid");
    var formContact = $("form#contactform");
    if (fullName === null || fullName === "") {
        fullNameValid.text("نام کامل خود را وارد کنید .");
    }
    else {
        fullNameValid.text("");
        if (email === null || email === "" ) {
            emailValid.text("یک ایمیل یا شماره تماس معتبر وارد کنید  .");
        }
        else {
            emailValid.text("");
            if (ValidateEmail(email) || (isValueNumber(email) && email.length == 11)) {
                emailValid.text("");
                if (subject === null || subject === "") {
                    subjectValid.text("موضوع پیام را وارد کنید .");
                }
                else {
                    subjectValid.text("");
                    if (message === null || message === "") {
                        messageValid.text("متن پیام را وارد کنید .");
                    }
                    else {
                        messageValid.text("");
                        formContact.submit();
                    }
                }
            }
            else {
                emailValid.text("یک ایمیل یا شماره تماس معتبر وارد کنید  .");
            }
        }
    }
}