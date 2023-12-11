
window.addEventListener('DOMContentLoaded', event => {

    // Navbar shrink function
    var navbarShrink = function () {
        const navbarCollapsible = document.body.querySelector('#mainNav');
        if (!navbarCollapsible) {
            return;
        }
        if (window.scrollY === 0) {
            navbarCollapsible.classList.remove('navbar-shrink')
        } else {
            navbarCollapsible.classList.add('navbar-shrink')
        }

    };

    // Shrink the navbar 
    navbarShrink();

    // Shrink the navbar when page is scrolled
    document.addEventListener('scroll', navbarShrink);

    // Activate Bootstrap scrollspy on the main nav element
    //const mainNav = document.body.querySelector('#mainNav');
    //if (mainNav) {
    //    new bootstrap.ScrollSpy(document.body, {
    //        target: '#mainNav',
    //        offset: 74,
    //    });
    //};

    // Collapse responsive navbar when toggler is visible
    const navbarToggler = document.body.querySelector('.navbar-toggler');
    const responsiveNavItems = [].slice.call(
        document.querySelectorAll('#navbarResponsive .nav-link')
    );
    responsiveNavItems.map(function (responsiveNavItem) {
        responsiveNavItem.addEventListener('click', () => {
            if (window.getComputedStyle(navbarToggler).display !== 'none') {
                navbarToggler.click();
            }
        });
    });

});


// CUSTOM CAROUSEL //
$('.carousel .carousel-item').each(function () {
    var minPerSlide = 3;
    var next = $(this).next();
    if (!next.length) {
        next = $(this).siblings(':first');
    }
    next.children(':first-child').clone().appendTo($(this));

    for (var i = 0; i < minPerSlide; i++) {
        next = next.next();
        if (!next.length) {
            next = $(this).siblings(':first');
        }

        next.children(':first-child').clone().appendTo($(this));
    }
});




// CUSTOM FEATURED TABS
$("#custom-featured-tabs .nav-tabs a").click(function () {
    var position = $(this).parent().position();
    var width = $(this).parent().width();
    $("#custom-featured-tabs .slider").css({ "left": + position.left, "width": width });
});

var actWidth = $("#custom-featured-tabs .nav-tabs").find(".active").parent("li").width();
var actPosition = $("#custom-featured-tabs .nav-tabs .active").position();

$("#custom-featured-tabs .slider").css({ "left": + actPosition.left, "width": actWidth });

function GlobalSearch(searchtype) {
    var text = '';
    var redirect = '';

    switch(searchtype){
        case 'library':
            text = $('#library_global_srch_term').val();
            redirect = appPath + "/Library/Index?Search=" + text;
            break;
        case 'physician':
            text = $('#physician_global_srch_term').val();
            redirect = appPath + "/Physicians/Index?Search=" + text;
            break;

    }

    window.location.replace(redirect);
}
