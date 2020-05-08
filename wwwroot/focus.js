var blazorHelpers = blazorHelpers || {};
let today = new Date().toISOString().slice(0, 10);

blazorHelpers.scrollToFragment = (today) => {
  var element = document.getElementById(today);
  if (element) {
    element.scrollIntoView({
      behavior: "smooth",
    });
  }
};
