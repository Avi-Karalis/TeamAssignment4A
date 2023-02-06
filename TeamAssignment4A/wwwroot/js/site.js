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

//side navbar function
function openNav() {
    document.getElementById("mySidenav").style.width = "160px";

    //document.body.style.backgroundColor = "#111111";
}


function closeNav() {
    document.getElementById("mySidenav").style.width = "0";

    //document.body.style.backgroundColor = "#222222";
}

//toggle open/close from the same button
function toggleNav() {
    var mySidenav = document.getElementById("mySidenav");
    if (mySidenav.style.width === "160px") {
        closeNav();
    } else {
        openNav();
    }
}

////save sideBars potition
//function toggleNav() {
//    var mySidenav = document.getElementById("mySidenav");
//    if (mySidenav.style.width === "160px") {
//        localStorage.setItem("navbarWidth", "0");
//        closeNav();
//    } else {
//        localStorage.setItem("navbarWidth", "160px");
//        openNav();
//    }
//}

//window.onload = function () {
//    var navbarWidth = localStorage.getItem("navbarWidth");
//    if (navbarWidth === "160px") {
//        openNav();
//    } else {
//        closeNav();
//    }
//};

//dark mode function
function myFunction() {
    var element = document.body;
    element.classList.toggle("dark-mode");

    // Save mode
    if (element.classList.contains("dark-mode")) {
        document.getElementById("mySidenav").style.backgroundColor = "#111111";
        localStorage.setItem("dark-mode", "enabled");
    } else {
        document.getElementById("mySidenav").style.backgroundColor = "#222222";
        localStorage.setItem("dark-mode", "disabled");
    }
}

// Check if dark mode was previously enabled
if (localStorage.getItem("dark-mode") === "enabled") {
    document.body.classList.add("dark-mode");
}

// Get the switch element
const switchEl = document.querySelector("input");

// Get the saved switch state from local storage
const savedSwitchState = localStorage.getItem("switchState");

// Set the switch state based on the saved value
switchEl.checked = savedSwitchState === "true";

// Listen for switch changes
switchEl.addEventListener("change", function () {
    // Save the new switch state to local storage
    localStorage.setItem("switchState", this.checked);
});

type();










