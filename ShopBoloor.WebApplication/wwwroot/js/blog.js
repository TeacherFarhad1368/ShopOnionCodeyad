function GetBestBlogs() {
    $.ajax({
        type: "Get",
        url: "/Blog/GetBestBlogs"
    }).done(function (res) {
        var model = [];
        model = JSON.parse(res);
        var parent = $("ul#parentBestBlogUL");
        var childs = $("div#childBestBlogDiv");
        for (var i = 0; i < model.length; i++) {
            if (i == 0) {
                var item = ` <li class="tabs__item tabs__item--active">
                                <a href="#parent_${model[i].Id}" class="tabs__trigger">${model[i].CategoryTitle}</a>
                            </li>`;
                parent.append(item);
            }
            else {
                var item1 = ` 
                            <li class="tabs__item">
                                 <a href="#parent_${model[i].Id}" class="tabs__trigger">${model[i].CategoryTitle}</a>
                            </li>`;

                parent.append(item1);
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
                for (var j = 0; j < model[1].Blogs.length; j++) {
                    var blog = `<div class="col-md-6">
                                <article class="entry card">
                                    <div class="entry__img-holder card__img-holder">
                                        <a href="/blog/${model[1].Blogs[j].Slug}">
                                            <div class="thumb-container thumb-70">
                                                <img data-src="${model[1].Blogs[j].ImageName}" src="${model[1].Blogs[j].ImageName}"
                                                class="entry__img lazyload" alt="${model[1].Blogs[j].ImageAlt}" />
                                            </div>
                                        </a>
                                        <a href="/Blogs/${model[1].CategorySlug}" class="entry__meta-category entry__meta-category--label entry__meta-category--align-in-corner entry__meta-category--violet">${model[1].CategoryTitle}</a>
                                    </div>

                                    <div class="entry__body card__body">
                                        <div class="entry__header">

                                            <h2 class="entry__title">
                                                <a href="/blog/${model[1].Blogs[j].Slug}">${model[1].Blogs[j].Title}</a>
                                            </h2>
                                            <ul class="entry__meta">
                                                <li class="entry__meta-author">
                                                    <span>نویسنده:</span>
                                                    <a>${model[1].Blogs[j].Writer}</a>
                                                </li>
                                                <li class="entry__meta-date">
                                                   ${model[1].Blogs[j].CreationDate}
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="entry__excerpt">
                                            <p>عد از اعلام تصمیم سامسونگ برای به عقب انداختن عرضه گلکسی فولد، شایعاتی مبنی بر احتمال تاخیر در عرضه ...</p>
                                        </div>
                                    </div>
                                </article>
                            </div>`;
                    blogParent.append(blog);
                }

            }
           
        }
    });
}