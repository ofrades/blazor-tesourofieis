var win = $(this); // browser window
var nav = $("#main-nav"); // your navigation bar
var desktop = $(".desktop");
var mobile = $(".mobile");

function switchNavbar() {
  if (win.width() < 768) {
    // on mobile
    nav.removeClass("fixed-top");
    nav.addClass("fixed-bottom");
    mobile.removeClass("d-none");
    desktop.addClass("d-none");
    !(function () {
      var t;
      var e = document.getElementById("darkSwitch2");
      if (e) {
        (t =
          null !== localStorage.getItem("darkSwitch") &&
          "dark" === localStorage.getItem("darkSwitch")),
          (e.checked = t)
            ? document.body.setAttribute("data-theme", "dark")
            : document.body.removeAttribute("data-theme"),
          e.addEventListener("change", function (t) {
            e.checked
              ? (document.body.setAttribute("data-theme", "dark"),
                localStorage.setItem("darkSwitch", "dark"))
              : (document.body.removeAttribute("data-theme"),
                localStorage.removeItem("darkSwitch"));
          });
      }
    })();
  } else {
    // on desktop
    nav.removeClass("fixed-bottom");
    nav.addClass("fixed-top");
    desktop.removeClass("d-none");
    mobile.addClass("d-none");
    !(function () {
      var t;
      var e = document.getElementById("darkSwitch");
      if (e) {
        (t =
          null !== localStorage.getItem("darkSwitch") &&
          "dark" === localStorage.getItem("darkSwitch")),
          (e.checked = t)
            ? document.body.setAttribute("data-theme", "dark")
            : document.body.removeAttribute("data-theme"),
          e.addEventListener("change", function (t) {
            e.checked
              ? (document.body.setAttribute("data-theme", "dark"),
                localStorage.setItem("darkSwitch", "dark"))
              : (document.body.removeAttribute("data-theme"),
                localStorage.removeItem("darkSwitch"));
          });
      }
    })();
  }
}

// On first load
$(function () {
  switchNavbar();
});

// When browser resized
$(window).on("resize", function () {
  switchNavbar();
});
