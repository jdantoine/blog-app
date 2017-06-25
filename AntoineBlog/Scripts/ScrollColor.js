
$(window).scroll(function () {
    if ($(this).scrollTop() > 200) { // Set position from top to add class
        $('.navbar-custom').addClass("logohd");
    } else {
        $('.navbar-custom').removeClass("logohd");
    }
});