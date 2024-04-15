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
function GetComments(ownerId, commentFor, pageId) {
    
    $.ajax({
        type: "Get",
        url: `/Comments/${ownerId}/${commentFor}?pageId=${pageId}`
    }).done(function (res) {
        var model = [];
        model = JSON.parse(res);
        var h3CommentCount = $("h3#h3CommentCount");
        h3CommentCount.text(`${model.Comments.length} نظر`);
        var iCommentCount = $("i#iCommentCount");
        iCommentCount.text(` ${model.Comments.length} `);
        var CommentParentList = $("ul#CommentParentList");
        for (var i = 0; i < model.Comments.length; i++) {
            var commentParemt = `
             <li class="comment" id="liParentComment_${model.Comments[i].Id}">
                            <div class="comment-body">
                                <div class="comment-avatar">
                                    <img alt="${model.Comments[i].FullName}" src="${model.Comments[i].Avatar}">
                                </div>
                                <div class="comment-text">
                                    <h6 class="comment-author">${model.Comments[i].FullName}</h6>
                                    <div class="comment-metadata">
                                        <a href="#" class="comment-date">${model.Comments[i].CreationDate}</a>
                                    </div>
                                    <p>
                                    ${model.Comments[i].Text}
                                    </p>
                                    <a onclick="AnswerComment(${model.Comments[i].Id},'${model.Comments[i].FullName}')" style="cursor:pointer;float:left" 
                                    class="comment-reply">پاسخ</a>
                                </div>
                            </div>
                        </li>
                                `;
            CommentParentList.append(commentParemt);
            if (model.Comments[i].Childs.length > 0) {
                var parent = $(`li#liParentComment_${model.Comments[i].Id}`);
                for (var j = 0; j < model.Comments[i].Childs.length; j++) {
                    var childComment = `
                <ul class="children px-5">
                                <li class="comment">
                                    <div class="comment-body">
                                        <div class="comment-avatar">
                                            <img alt="${model.Comments[i].Childs[j].FullName}" src="${model.Comments[i].Childs[j].Avatar}">
                                        </div>
                                        <div class="comment-text">
                                            <h6 class="comment-author">${model.Comments[i].Childs[j].FullName}</h6>
                                            <div class="comment-metadata">
                                                <a href="#" class="comment-date">${model.Comments[i].Childs[j].CreationDate}</a>
                                            </div>
                                            <p>
                                            ${model.Comments[i].Childs[j].Text}
                                            </p>
                                        </div>
                                    </div>
                                </li> 
                            </ul>
                `;
                    parent.append(childComment);
                }
            }
           
        }

        var getMoreComment = $("div#getMoreCommentDiv");
        getMoreComment.html("");
        if (model.PageCount > model.PageId) {
            var getMore = `
             
                    <a class="btn btn-lg btn-color px-4 py-2" onclick="GetComments( '${model.OwnerId}', '${commentFor}' , ${model.PageId + 1})">
                        ادامه نظرات
                    </a>
                    
            `;
            getMoreComment.append(getMore);
        }
    });
}
function AnswerComment(id, fullName) {
    $("#labalFullNameComment").text(`پاسخ برای  ${fullName}`);
    var parent = $("input#parentIdComment");
    parent.val(id);
    ScroolToEleman('respond');
    $("textarea#textComment").select();
    return;
}
function submitComment() {
    var fullName = $("input#fullNameComment").val();
    var email = $("input#emailComment").val();
    var text = $("textarea#textComment").val();
    if (fullName === null || fullName === "") {
        $("span#fullNameCommentValid").text("نام کامل اجباری است .");
    }
    else {
        $("span#fullNameCommentValid").text("");
        if ((email !== null && email !== "") && ValidateEmail(email) === false) {
            $("span#emailCommentValid").text("یک ایمیل معتبر وارد کنید .");
        }
        else {
            $("span#emailCommentValid").text("");
            if (text === "" || text === null) {
                $("span#textCommentValid").text("متن نظر اجباری است .");
            }
            else {
                $("span#textCommentValid").text("");
                $("form#formComment").submit();
            }
        }
    }
}