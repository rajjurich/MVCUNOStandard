var $grid_color = "#cccccc";
var $info = "#5B90BF";
var $danger = "#D66061";
var $warning = "#ffaa3a";
var $success = "#76BBAD";
var $fb = "#4c66a4";
var $twitter = "#00acee";
var $linkedin = "#1a85bd";
var $gplus = "#dc4937";
var $brown = "#ab7967";

//Dropdown Menu
$(document).ready(function () {
    $(document.body).on("click", "#menu ul li a", function () {

        $('#menu li').removeClass('active');
        $(this).closest('li').addClass('active');
        var checkElement = $(this).next();
        if ((checkElement.is('ul')) && (checkElement.is(':visible'))) {
            $(this).closest('li').removeClass('active');
            checkElement.slideUp('normal');
        }
        if ((checkElement.is('ul')) && (!checkElement.is(':visible'))) {
            $('#menu ul ul:visible').slideUp('normal');
            checkElement.slideDown('normal');
        }
        if ($(this).closest('li').find('ul').children().length == 0) {
            return true;
        } else {
            return false;
        }
    });
});

$(function () {
    var s = 0;
    $('.menu-toggle.left').click(function () {
        if (s == 0) {
            s = 1;
            $("#sidebar").animate({ left: "0px" }, 100).addClass('open');
            $('.dashboard-wrapper').animate({ 'margin-left': "50px" }, 100);
            $("#sidebar.open .header").animate({ 'width': "50px" }, 100);
            $("#sidebar.open .header").animate({ 'padding': "0px" }, 100);
            $("#sidebar.open .header").hide();
            $("#sidebar.open #menu .header").show();
            $("#sidebar #menu li.header a").animate({ 'padding': "5px 8px" }, 100);
            $('header .logo').animate({ 'width': "50px" }, 100);
            $('header .logo a').animate({ 'padding-left': "7px" }, 100);
            $('header .logo a img').animate({ 'width': "34px", 'height': "34px" }, 100);
            $('header .logo .title-name').hide();
            $('.menu-toggle.left').hide();
            $('.menu-toggle.right').addClass("show");
            $('.menu-toggle.right').removeClass("hide");
        } else {
            s = 0;
            $('#sidebar').animate({ left: "0px" }, 100).removeClass('open');
            $('.dashboard-wrapper').animate({ 'margin-left': "210px" }, 100);
            $("#sidebar .header").animate({ 'width': "100%" }, 100);
            $("#sidebar .header").animate({ 'padding': "0px 20px" }, 100);
            $("#sidebar .header").show();
            $("#sidebar #menu li.header").hide();
            $('.menu-toggle.right').hide();
            $('.menu-toggle.right').css({ 'display': 'none' });
        }
    });


});
//$('.menu-toggle.left').click(function () {
//    $('#menu > ul > li').removeClass('active');
//    $('#menu > ul > li > ul').css({ 'display': 'none' });
//});
$('.menu-toggle.right').click(function () {

    $('#sidebar').animate({ left: "0px" }, 100).removeClass('open');
    $('.dashboard-wrapper').animate({ 'margin-left': "210px" }, 100);
    $("#sidebar .header").animate({ 'width': "100%" }, 100);
    $("#sidebar .header").animate({ 'padding': "0px 20px" }, 100);
    $("#sidebar .header").show();
    $("#sidebar #menu li.header").hide();
    $('header .logo').animate({ 'width': "210px" }, 100);
    $('header .logo a').animate({ 'padding-left': "16px" }, 100);
    $('header .logo a img').animate({ 'width': "42px", 'height': "42px" }, 100);
    $('header .logo .title-name').show();
    $('.menu-toggle.right').addClass("hide");
    $('.menu-toggle.left').show();
    $('.menu-toggle.right').removeClass("show");
});
// scrollUp full options
$(function () {
    $.scrollUp({
        scrollName: 'scrollUp', // Element ID
        scrollDistance: 180, // Distance from top/bottom before showing element (px)
        scrollFrom: 'top', // 'top' or 'bottom'
        scrollSpeed: 300, // Speed back to top (ms)
        easingType: 'linear', // Scroll to top easing (see http://easings.net/)
        animation: 'fade', // Fade, slide, none
        animationSpeed: 200, // Animation in speed (ms)
        scrollTrigger: false, // Set a custom triggering element. Can be an HTML string or jQuery object
        //scrollTarget: false, // Set a custom target element for scrolling to the top
        scrollText: '<i class="fa fa-chevron-up"></i>', // Text for element, can contain HTML // Text for element, can contain HTML
        scrollTitle: false, // Set a custom <a> title if required.
        scrollImg: false, // Set true to use image
        activeOverlay: false, // Set CSS color to display scrollUp active point, e.g '#00FFFF'
        zIndex: 2147483647 // Z-Index for the overlay
    });
});

// Mobile Nav
$('#mob-nav').click(function () {
    if ($('aside.open').length > 0) {
        $("aside").animate({ left: "-250px" }, 500).removeClass('open');
    } else {
        $("aside").animate({ left: "0px" }, 500).addClass('open');
    }
});

// Tooltips
$('a').tooltip('hide');

//show on radio button

$(function () {
    $('input[name=location]').click(function () {
        if (this.id == "watch-me") {
            $("#show-me").show('slow');
        } else {
            $("#show-me").hide('slow');
        }
    });
});

//profilepic
$(function () {
    $("#uploadFile").on("change", function () {
        var files = !!this.files ? this.files : [];
        if (!files.length || !window.FileReader) return; // no file selected, or no FileReader support

        if (/^image/.test(files[0].type)) { // only image file
            var reader = new FileReader(); // instance of the FileReader
            reader.readAsDataURL(files[0]); // read the local file

            reader.onloadend = function () { // set image data as background of div
                $("#imagePreview").css("background-image", "url(" + this.result + ")");
            }
        }
    });
});

// datepicker
$(function () {
    $(".datepicker").datepicker({
        autoclose: true,
        todayHighlight: true        
    }).datepicker(); //.datepicker('update', new Date('2018/01/01'))
});

// form-add region
/*! function(window, document, $) {
    "use strict";
    $(".repeater-default").repeater(), $(".file-repeater, .contact-repeater").repeater({
        show: function() {
            $(this).slideDown()
        },
        hide: function(remove) {
            confirm("Are you sure you want to remove this item?") && $(this).slideUp(remove)
        }
    })
}(window, document, jQuery);

var i = 0;
var original = document.getElementById('form-repeater');
function repeat() {
  var clone = original.cloneNode(true);
  clone.id = "form-repeater" + ++i; 
  original.parentNode.appendChild(clone);
};*/

$(function () {

    // Change the selector if needed
    var $table = $('table.scroll'),
        $bodyCells = $table.find('tbody tr:first').children(),
        colWidth;

    // Adjust the width of thead cells when window resizes
    $(window).resize(function () {
        // Get the tbody columns width array
        colWidth = $bodyCells.map(function () {
            return $(this).width();
        }).get();

        // Set the width of thead columns
        $table.find('thead tr').children().each(function (i, v) {
            $(v).width(colWidth[i]);
        });
    }).resize(); // Trigger resize handler

});
