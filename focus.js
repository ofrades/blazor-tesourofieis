var blazorHelpers = blazorHelpers || {};
let today = new Date().toISOString().split("T")[0];

blazorHelpers.scrollToFragment = (today) => {
  var element = document.getElementById(today);
  if (element) {
    element.scrollIntoView({
      behavior: "smooth",
    });
  }
};
