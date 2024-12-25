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
