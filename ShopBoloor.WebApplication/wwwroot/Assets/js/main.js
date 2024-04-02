$(document).ready(function () {
  //Init the carousel
  $("#suggestion-slider").owlCarousel({
    rtl: true,
    items: 1,
    autoplay: true,
    autoplayTimeout: 5000,
    loop: true,
    dots: false,
    onInitialized: startProgressBar,
    onTranslate: resetProgressBar,
    onTranslated: startProgressBar,
  });

  function startProgressBar() {
    // apply keyframe animation
    $(".slide-progress").css({
      width: "100%",
      transition: "width 5000ms",
    });
  }

  function resetProgressBar() {
    $(".slide-progress").css({
      width: 0,
      transition: "width 0s",
    });
  }

  // **************  product slider
  $(".product-carousel").owlCarousel({
    rtl: true,
    margin: 10,
    nav: true,
    navText: [
      '<i class="now-ui-icons arrows-1_minimal-right"></i>',
      '<i class="now-ui-icons arrows-1_minimal-left"></i>',
    ],
    dots: false,
    responsiveClass: true,
    responsive: {
      0: {
        items: 2,
        slideBy: 1,
      },
      576: {
        items: 3,
        slideBy: 1,
      },
      768: {
        items: 4,
        slideBy: 2,
      },
      992: {
        items: 5,
        slideBy: 2,
      },
    },
  });

  $(".brand-slider .owl-carousel").owlCarousel({
    rtl: true,
    margin: 10,
    items: 5,
    nav: true,
    navText: [
      '<i class="now-ui-icons arrows-1_minimal-right"></i>',
      '<i class="now-ui-icons arrows-1_minimal-left"></i>',
    ],
    responsive: {
      0: {
        items: 2,
      },
      576: {
        items: 4,
      },
      768: {
        items: 5,
      },
      992: {
        items: 5,
      },
    },
  });

  $("#gallery-slider").owlCarousel({
    rtl: true,
    margin: 10,
    nav: true,
    navText: [
      '<i class="now-ui-icons arrows-1_minimal-right"></i>',
      '<i class="now-ui-icons arrows-1_minimal-left"></i>',
    ],
    dots: false,
    responsiveClass: true,
    responsive: {
      0: {
        items: 4,
        slideBy: 1,
      },
    },
  });

  $(".back-to-top").click(function (e) {
    e.preventDefault();
    $("html, body").animate({ scrollTop: 0 }, 800, "easeInExpo");
  });

  if ($("#img-product-zoom").length) {
    $("#img-product-zoom").ezPlus({
      zoomType: "inner",
      containLensZoom: true,
      gallery: "gallery_01f",
      cursor: "crosshair",
      galleryActiveClass: "active",
      responsive: true,
      imageCrossfade: true,
      zoomWindowFadeIn: 500,
      zoomWindowFadeOut: 500,
    });
  }

  $(".sum-more").click(function () {
    var sumaryBox = $(this).parents(".parent-expert");
    sumaryBox.find(".content-expert").toggleClass("active");
    sumaryBox.find(".shadow-box").fadeToggle();

    $(this).find("i").toggleClass("active");

    $(this).find(".show-more").fadeToggle(0);
    $(this).find(".show-less").fadeToggle(0);
  });

  $("nav.header-responsive li.active").addClass("open").children("ul").show();

  $("nav.header-responsive li.sub-menu> a").on("click", function () {
    $(this).removeAttr("href");
    var e = $(this).parent("li");
    if (e.hasClass("open")) {
      e.removeClass("open");
      e.find("li").removeClass("open");
      e.find("ul").slideUp(400);
    } else {
      e.addClass("open");
      e.children("ul").slideDown(400);
      e.siblings("li").children("ul").slideUp(400);
      e.siblings("li").removeClass("open");
    }
  });

  // Start scroll

  $(window).scroll(function () {
    if ($(this).scrollTop() > 60) {
      $("nav.header-responsive").css({ height: "60px" });
      $("nav.header-responsive .search-nav").css({
        opacity: "0",
        visibility: "hidden",
      });
    } else {
      $("nav.header-responsive").css({ height: "110px" });
      $("nav.header-responsive .search-nav").css({
        opacity: "1",
        visibility: "visible",
      });
    }
  });

  // End scroll

  // favorites product

  $("ul.gallery-options button.add-favorites").on("click", function () {
    $(this).toggleClass("favorites");
  });

  // favorites product

  // LocalSearch
  if ($("header.main-header").length) {
    $("#gsearchsimple").focus(function () {
      $("header.main-header .search-area form.search ul.search-box-list").css(
        "display",
        "block"
      );
      $(".overlay-search-box").css({ opacity: "1", visibility: "visible" });
    });
    $(
      ".overlay-search-box,header.main-header .col-md-4.col-sm-12,header.main-header .col-lg-2.col-md-3.col-sm-4.col-5"
    ).click(function () {
      $("header.main-header .search-area form.search ul.search-box-list").css(
        "display",
        "none"
      );
      $(".overlay-search-box").css({ opacity: "0", visibility: "hidden" });
    });
    $(".localSearchSimple").jsLocalSearch({
      action: "Show",
      html_search: true,
      mark_text: "marktext",
    });
  }
  // LocalSearch

  // verify-phone-number
  if ($("#countdown-verify-end").length) {
    var $countdownOptionEnd = $("#countdown-verify-end");

    $countdownOptionEnd.countdown({
      date: new Date().getTime() + 180 * 1000, // 1 minute later
      text: '<span class="day">%s</span><span class="hour">%s</span><span>: %s</span><span>%s</span>',
      end: function () {
        $countdownOptionEnd.html(
          "<a href='' class='btn-link-border form-account-link'>ارسال مجدد</a>"
        );
      },
    });

    $(".line-number").keyup(function () {
      $(this).next().focus();
    });
  }
  // verify-phone-number
});
