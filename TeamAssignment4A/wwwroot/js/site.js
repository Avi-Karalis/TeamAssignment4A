// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $(window).scroll(function () {
        if ($(this).scrollTop() > 200) {
            $('.go-to-top').show();
        } else {
            $('.go-to-top').hide();
        }
    });
});

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


//----1st attempt
const text = "PeopleCert Operations Menu";
const typingDelay = 100; // delay between each character
const newTextDelay = 12000; // delay between current and next text
let index = 0;

function type() {
    if (index < text.length) {
        document.querySelector(".typing-text").textContent += text.charAt(index);
        index++;
        setTimeout(type, typingDelay);
    } else {
        setTimeout(erase, newTextDelay);
    }
}

type();










