
$(function () {
    $(".btnWorksheetSubmit").click(function (event) {
        var Validated = true;

        $("input[name='QuestionId']").each(function () {
            var QuestionId = $(this).val();
            if ($("input[name='opt_" + QuestionId + "']").length > 0) {
                if ($("input[name='opt_" + QuestionId + "']").is(':checked') == false) {
                    $(".validateWorksheet").css("display", "block");
                    Validated = false;
                    $("html, body").animate({ scrollTop: 0 }, 600);
                }
            }
            if ($("input[name='txtAnswer_" + QuestionId + "']").length > 0) {
                if ($("#txtAnswer_" + QuestionId).val() == '') {
                    $(".validateWorksheet").css("display", "block");
                    Validated = false;
                    $("html, body").animate({ scrollTop: 0 }, 600);
                }
            }
        });
        if (Validated) {
            $(".validateWorksheet").css("display", "none");
            $(".sheetPreview").html('');
            var Answer = "";
            $("input[name='QuestionId']").each(function () {
                var QuestionId = $(this).val();
                Answer = Answer + QuestionId + "~";
                if ($("input[name='opt_" + QuestionId + "']").length > 0) {
                    $("input[name = 'opt_" + QuestionId + "']:checked").each(function () {
                        Answer = Answer + $(this).val() + "Ø";
                    });
                }
                if ($("input[name='txtAnswer_" + QuestionId + "']").length > 0) {
                    Answer = Answer + $("#txtAnswer_" + QuestionId).val() + "Ø";
                }
            });
            var $url = "/sheet/getworksheetstatus";
            $url = $url.toLowerCase();
            $.ajax({
                url: $url,
                data: { SheetId: $("#sheetId").val(), QuestionAnswers: Answer, IsSubmitted: false },
                cache: false,
                type: "POST",
                async: false,
                success: function (data) {
                    $('#modalBox').html(data);
                },
                error: function () {
                    alert("error");
                }
            });
            $('#SheetPreviewModal').modal();
        }
        event.preventDefault();
        return false;
    });
});

function OnSuccess(arg) {
    $('#SheetPreviewModal').modal('hide');
    $('#modalBox').html(arg);
    $('#SheetFinalModal').modal('show');
}

function Retry() {
    $('#SheetFinalModal').modal('hide');
    $('.modal-backdrop').hide();
        $(".worksheet-content input:radio").attr("checked", false);
        $(".worksheet-content input:text").val("");
}

function Close() {
    $('#SheetFinalModal').modal('hide');
    $('.modal-backdrop').hide();   
}



$(".view-worksheet").click(function () {
    $(".validateWorksheet").css("display", "none");
    $(".ready-worksheet").hide();
    $(".worksheet-content").show();
    $(".view-Worksheet-hide").hide();
    $(".online-Worksheet-hide").show();
    $(".online-Worksheet").show();
    $(".view-Worksheet").hide();
});

$(".online-worksheet").click(function () {
    $(".ready-worksheet").hide();
    $(".worksheet-content").show();

    $(".view-Worksheet-hide").show();
    $(".online-Worksheet-hide").hide();
    $(".online-Worksheet").hide();
    $(".view-Worksheet").show();
});


$("#btnPrint").click(function () {    
    var eventCat = 'PrintWorksheet';
    var eventLabel = 'top' + $("#sheetId").val() + " - " + $("#skillId").val()
    window.print();
    trackEventByAnalystics(eventCat, eventLabel);
    //ga('send', 'event', eventCat, 'click', eventLabel, { 'nonInteraction': 1 });
});

$("#btnPrint2").click(function () {
    var eventCat = 'PrintWorksheet';
    var eventLabel = 'bottom' + $("#sheetId").val() + " - " + $("#skillId").val()
    window.print();
    trackEventByAnalystics(eventCat, eventLabel);
    //ga('send', 'event', eventCat, 'click', eventLabel, { 'nonInteraction': 1 });
});



//canvas 
function ShowCanvas() {
        if ($("#divCanvas").hasClass("hide")) {
            $("#divCanvas").removeClass("hide");
            $('#tools_sketch').sketch({ defaultColor: "#000" });
            $(".showMarker").removeClass("hide");
            $(".eraseSketch").removeClass("hide");
            $('.btnShowCanvas').text('Hide Scrapbook');
        }
        else {
            $("#divCanvas").addClass("hide");
            $(".showMarker").addClass("hide");
            $(".eraseSketch").addClass("hide");
            $('.btnShowCanvas').text('Show Scrapbook');
        }
    }
    function ShowMarker() {
        $(".eraseSketch").css('color', '#9be4ff');
        $(".showMarker").css('color', '#428bca');

    }
    function EraseSketch() {
        $(".eraseSketch").css('color', '#428bca');
        $(".showMarker").css('color', '#9be4ff');

    }
    //end////




