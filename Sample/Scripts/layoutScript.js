//Script from _layout.cshtml

$(document).ready(function () {
    $("ul.vertical-tabs-ul li").click(function (e) {
        $("ul.vertical-tabs-ul li").removeClass("vertical-tabs-li");
        $(this).addClass("vertical-tabs-li");
    });

    if ($("span[class^='progress-']").length > 0) {
        var $url = "/topic/getuserprogress";
        ajaxGet($url.toLowerCase(), null, function (result) {

            if (result != undefined && result != null) {
                console.log(result);
                var previousSubtopic = 0;
                var Count = 0;
                $.each(result, function (i, item) {
                    if (previousSubtopic != item.SubTopicId) {
                        previousSubtopic = item.SubTopicId;
                        Count = 0;
                    }
                    Count = Count + 1;
                    if (item.IsCompleted || (item.Correct) >= 10) {
                        var accuracy = ((item.Correct / (item.Correct + item.Wrong)) * 100).toFixed(2) + '%';
                        if (item.Correct == (item.Correct + item.Wrong))
                            accuracy = '100%';
                        $(".progress-" + item.SubTopicId).append("<span Title='Accuracy :- " + accuracy + "'> <img src='/content/images/success.png' alt='User Progress Success' /></span>");
                    }
                });
            }
        });
    }

    $('#btnUserProfileUpdate').click(function () {
        $("#collapse-Two").removeClass("collapse");
        $("#collapse-Three").removeClass("collapse");
        $("#aLink-collapse-Two").removeClass("collapsed");
        $("#aLink-collapse-Three").removeClass("collapsed");
    });
});





//$(document).ready(function () {
//    var findGroup = window.location.href.split("#")[1];
//    if (findGroup != undefined && findGroup.length > 0) {
//        $('li[class^="menu-Tabs"]').removeClass('active');
//        $("#" + findGroup).parent('li').addClass('active');
//        $('div[class^="tab-pane"]').removeClass('active');
//        $("#div-" + findGroup).addClass('active');
//        $("html, body").animate({ scrollTop: 0 }, 500);
//    }
//});
$("li.menu-Tabs a").click(function () {
    $('li[class^="menu-Tabs"]').removeClass('active');
    $(this).parent('li').addClass('active');
    $('div[class^="tab-pane"]').removeClass('active');
    $("#div-" + $(this).attr('id')).addClass('active');
    $("html, body").animate({ scrollTop: 0 }, 2000);
});

////end///
/// scripts from _layoutHome

$(document).ready(function () {
    $(':input[placeholder]').placeholder();
    $(".toolTip").tooltip();
    $("#searchResult").hide();
});

///end///

//scripts from _layoutLearn


$(document).ready(function () {

    $(window).scroll(function () {
        if ($(this).scrollTop() > 100) {
            $('.scrollup').fadeIn();
        } else {
            $('.scrollup').fadeOut();
        }
    });
    $('.scrollup').click(function () {
        $("html, body").animate({ scrollTop: 0 }, 600);
        return false;
    });
    $('.accordion-heading a').on('click', function (e) {
        var parentAccordion = $(this).attr('data-parent');
        $(parentAccordion).find('.accordion-group').each(function () {
            $(this).find('.accordion-heading a').removeClass('collapsed');
            var icon = $(this).find('.accordion-heading a').find('i');
            $(icon).removeClass('icon-chevron-down');
            $(icon).addClass('icon-chevron-left');
            $(icon).attr("data-icon-hide", "icon-chevron-down");
            $(icon).attr("data-icon-show", "icon-chevron-left");
        });
        var openSection = $(this).attr('href');
        $(this).find('i').each(function () {
            if ($(openSection).hasClass('in')) {
                $(this).removeClass('icon-chevron-down');
                $(this).addClass('icon-chevron-left');
                $(this).attr("data-icon-hide", "icon-chevron-down");
                $(this).attr("data-icon-show", "icon-chevron-left");
            }
            else {
                $(this).removeClass('icon-chevron-left');
                $(this).addClass('icon-chevron-down');
                $(this).attr("data-icon-hide", "icon-chevron-left");
                $(this).attr("data-icon-show", "icon-chevron-down");
            }
        });
    })
});


$(document).ready(function () {
    var finalTab = window.location.href.toLowerCase();
    if (finalTab.lastIndexOf("/learn/") > 0) {
        $(".learnBreadCrum").show();
    }
    else {
        $(".learnBreadCrum").hide();
    }
    $(".accordion-toggle").click(function () {
        if ($(this).children('i').hasClass('fa-chevron-left')) {
            $(this).children('i').removeClass('fa-chevron-left').addClass('fa-chevron-down');
        }
        else {
            $(this).children('i').removeClass('fa-chevron-down').addClass('fa-chevron-left');
        }
    });


});

///end///

///layout question////

$(document).ready(function () {
    $(document).keypress(function (e) {
        if (e.which == 13) {
            if ($('#Submit').css('display') == "inline-block") {
                if ($('#Submit').attr("disabled") != "disabled") {
                    $('#Submit').focus();
                    $("#Submit").click();
                }
            }
            else if ($('#CompleteProgress').css('display') == "inline-block") {
                if ($('#CompleteProgress').attr("disabled") != "disabled") {
                    $('#CompleteProgress').focus();
                    $("#CompleteProgress").click();
                }
            }
        }
    });
});
//function ShowCanvas() {
//    if ($("#divCanvas").hasClass("hide")) {
//        $("#divCanvas").removeClass("hide");
//        $('#tools_sketch').sketch({ defaultColor: "#000" });
//        $(".showMarker").removeClass("hide");
//        $(".eraseSketch").removeClass("hide");
//        $('.btnShowCanvas').text('Hide Scrapbook');
//    }
//    else {
//        $("#divCanvas").addClass("hide");
//        $(".showMarker").addClass("hide");
//        $(".eraseSketch").addClass("hide");
//        $('.btnShowCanvas').text('Show Scrapbook');
//    }
//}
//function ShowMarker() {
//    $(".eraseSketch").css('color', '#9be4ff');
//    $(".showMarker").css('color', '#428bca');

//}
//function EraseSketch() {
//    $(".eraseSketch").css('color', '#428bca');
//    $(".showMarker").css('color', '#9be4ff');

//}
//end////


///layout report///


$(document).ready(function () {
    if ($('#lnkAdminSchool').length > 0) {
        $('.reportlnkAdminSchool').show();
    }
    else {
        $('.reportlnkAdminSchool').remove();
    }
});



///end////
//script to highlight grades tabs

$(document).ready(function () {
    var finalTab = window.location.href.toLowerCase();
    if (finalTab.lastIndexOf("/grade/") > 0) {
        finalTab = finalTab.substring(finalTab.lastIndexOf("/") + 1);
        if (finalTab != undefined && finalTab.length > 0) {
            $("." + finalTab).addClass('active');
        }
    }
});

$(document).ready(function () {
    var finalTab = window.location.href.toLowerCase();
    var innerTab = window.location.href.toString();
    var str = finalTab.split('/');
    if (finalTab.lastIndexOf("/") > 0) {
        if (str.length > 5 && (str[3] == "learn")) {
            innerTab = innerTab.substring(finalTab.lastIndexOf("/") + 1);
            $("." + innerTab).parent('li').addClass('active');
        }
        if (str[3] != "") {
            if (str.length > 3 && str[3] != "certificate") {
                if (str[3] == "math-problem") {
                    finalTab = "math-practice";
                }
                else if (str[3] == "sheet") {
                    finalTab = "worksheets";
                }
                else {
                    finalTab = str[3];
                }
            }
            else {
                finalTab = finalTab.substring(finalTab.lastIndexOf("/") + 1);
            }

            $("." + finalTab).parent('li').addClass('active');
        }

    }
});

