// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function scrollToTop() {
    window.scrollTo({
        top: 0,
        behavior: "smooth"
    });
}

$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})

$(function () {
    var threshold = 100;
    var tooltip = $('[data-toggle="tooltip"]');
    $(tooltip).on('click', function () {
        tooltip.tooltip('hide');
        scrollToTop();
    });
    $(window).on('scroll', function () {
        var scroll = $(window).scrollTop();
        if (scroll > threshold) {
            tooltip.tooltip('show');
        } else {
            tooltip.tooltip('hide');
        }
    });
});
