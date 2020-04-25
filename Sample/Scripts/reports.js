//$(document).ready(function () {
//    $("#toggle").click(function () {
//        if ($(this).data('name') == 'show') {
//            $("#divReportMenu").removeClass("col-md-3 col-sm-3");
//            $("#divReportMenu").addClass("col-md-1 col-sm-1");
//            $("#divMenu").hide();

//            $("#divReports").removeClass("col-md-9 col-sm-9");
//            $("#divReports").addClass("col-md-11 col-sm-11");
//            $(this).data('name', 'hide');
            
//        } else {
//            $("#divReportMenu").removeClass("col-md-1 col-sm-1");
//            $("#divReportMenu").addClass("col-md-3 col-sm-3");
//            $("#divMenu").show();

//            $("#divReports").removeClass("col-md-11 col-sm-11");
//            $("#divReports").addClass("col-md-9 col-sm-9");
//            $(this).data('name', 'show');
//        }
//    });
//});

//old code
//$(document).ready(function () {
  
//    $("#GenerateReport").click(function () {
//        $('#divReportDetails').html('');
//        $('#Error').html('');
//        if (Date.parse($("#txtStartDate").val()) > Date.parse($("#txtEndDate").val())) {
//            $('#Error').css('display', 'block');
//            $('#Error').html("* End date must be equal or greater then Start Date.");
//            return false;
//        }
//        $.ajax({
//            //url: '@Url.Action("UserActivityDetails", "Reports")',
//            url: '/reports/useractivitydetails',
//            data: { StartDate: $("#txtStartDate").val(), EndDate: $("#txtEndDate").val() },
//            cache: false,
//            type: "GET",
//            async: false,
//            success: function (data) {
//                $('#Error').css('display', 'none');
//                $('#divReportDetails').html(data);
//                $("#tblUserActivity tbody tr").each(function () {
//                    var perf = $(this).find("td:last-child span").text().toLowerCase();
//                    if (perf == "excellent")
//                        $(this).find("td:last-child span").addClass("label-success");
//                    else if (perf == "very good")
//                        $(this).find("td:last-child span").addClass("label-blue");
//                    else if (perf == "good")
//                        $(this).find("td:last-child span").addClass("label-warning");
//                    else if (perf == "average")
//                        $(this).find("td:last-child span").addClass("label-yellow");
//                    else
//                        $(this).find("td:last-child span").addClass("label-red");
//                    // compare id to what you want
//                });
//            },
//            error: function (jqXHR, textStatus, errorThrown) {
//                var message = "";
//                if (jqXHR.status == 500) {
//                    message = "Opps ! Some server side error occured. Please  try again.";
//                } else if (jqXHR.status == 404) {
//                    message = "Opps ! your internet connection is not working. Please try again.";
//                }
//                else {
//                    message = "Opps ! some error occured. Please try again.";
//                }
//                $().toastmessage({ sticky: true, inEffectDuration: 4000, stayTime: 6000, position: 'middle-center' });
//                var successToast = $().toastmessage('showSuccessToast', message);
//                $().toastmessage('removeToast', successToast);
//            }
//        });
//    });
//});


////report script end

//// scripts for user worksheets
//    $(document).ready(function () {
       
//        $("#GenerateWorksheetReport").click(function () {
//            $('#divReportDetails').html('');
//            $('#Error').html('');
//            if (Date.parse($("#txtStartDate").val()) > Date.parse($("#txtEndDate").val())) {
//                $('#Error').css('display', 'block');
//                $('#Error').html("* End date must be equal or greater then Start Date.");
//                return false;
//            }
//            $.ajax({
//                url: '/reports/userworksheetsdetails',
//                data: { StartDate: $("#txtStartDate").val(), EndDate: $("#txtEndDate").val() },
//                cache: false,
//                type: "GET",
//                async: false,
//                success: function (data) {
//                    $('#Error').css('display', 'none');
//                    $('#divReportDetails').html(data);
//                },
//                error: function (jqXHR, textStatus, errorThrown) {
//                    var message = "";
//                    if (jqXHR.status == 500) {
//                        message = "Opps ! Some server side error occured. Please  try again.";
//                    } else if (jqXHR.status == 404) {
//                        message = "Opps ! your internet connection is not working. Please try again.";
//                    }
//                    else {
//                        message = "Opps ! some error occured. Please try again.";
//                    }
//                    $().toastmessage({ sticky: true, inEffectDuration: 4000, stayTime: 6000, position: 'middle-center' });
//                    var successToast = $().toastmessage('showSuccessToast', message);
//                    $().toastmessage('removeToast', successToast);
//                }
//            });
//        });
//    });

//// end////////

////code for student worksheet
//$(document).ready(function () {
//    $("#GenerateStudentWorksheetReport").click(function () {
//        $('#divReportDetails').html('');
//        $('#Error').html('');
//        if (Date.parse($("#txtStartDate").val()) > Date.parse($("#txtEndDate").val())) {
//            $('#Error').css('display', 'block');
//            $('#Error').html("* End date must be equal or greater then Start Date.");
//            return false;
//        }
//        $.ajax({
//            url: '/reports/studentworksheetsdetails',
//            data: { StartDate: $("#txtStartDate").val(), EndDate: $("#txtEndDate").val() },
//            cache: false,
//            type: "GET",
//            async: false,
//            success: function (data) {
//                $('#Error').css('display', 'none');
//                $('#divReportDetails').html(data);
//            },
//            error: function (jqXHR, textStatus, errorThrown) {
//                var message = "";
//                if (jqXHR.status == 500) {
//                    message = "Opps ! Some server side error occured. Please  try again.";
//                } else if (jqXHR.status == 404) {
//                    message = "Opps ! your internet connection is not working. Please try again.";
//                }
//                else {
//                    message = "Opps ! some error occured. Please try again.";
//                }
//                $().toastmessage({ sticky: true, inEffectDuration: 4000, stayTime: 6000, position: 'middle-center' });
//                var successToast = $().toastmessage('showSuccessToast', message);
//                $().toastmessage('removeToast', successToast);
//            }
//        });
//    });
//});

/////end////

/////code for student activity
    
//    $(document).ready(function () {            
//        $("#GenerateStudentActivityReport").click(function () {
//                $('#divReportDetails').html('');
//                $('#Error').html('');
//                if (Date.parse($("#txtStartDate").val()) > Date.parse($("#txtEndDate").val())) {
//                    $('#Error').css('display', 'block');
//                    $('#Error').html("* End date must be equal or greater then Start Date.");
//                    return false;
//                }
//                $.ajax({
//                    url: '/reports/studentsactivitydetails',
//                    data: { StartDate: $("#txtStartDate").val(), EndDate: $("#txtEndDate").val() },
//                    cache: false,
//                    type: "GET",
//                    async: false,
//                    success: function (data) {
//                        $('#Error').css('display', 'none');
//                        $('#divReportDetails').html(data);
//                        $("#tblStudentActivity tbody tr").each(function () {
//                            var perf = $(this).find("td:last-child span").text().toLowerCase();
//                            if (perf == "excellent")
//                                $(this).find("td:last-child span").addClass("label-success");
//                            else if (perf == "very good")
//                                $(this).find("td:last-child span").addClass("label-blue");
//                            else if (perf == "good")
//                                $(this).find("td:last-child span").addClass("label-warning");
//                            else if (perf == "average")
//                                $(this).find("td:last-child span").addClass("label-yellow");
//                            else
//                                $(this).find("td:last-child span").addClass("label-red");
//                            // compare id to what you want
//                        });
//                    },
//                    error: function (jqXHR, textStatus, errorThrown) {
//                        var message = "";
//                        if (jqXHR.status == 500) {
//                            message = "Opps ! Some server side error occured. Please  try again.";
//                        } else if (jqXHR.status == 404) {
//                            message = "Opps ! your internet connection is not working. Please try again.";
//                        }
//                        else {
//                            message = "Opps ! some error occured. Please try again.";
//                        }
//                        $().toastmessage({ sticky: true, inEffectDuration: 4000, stayTime: 6000, position: 'middle-center' });
//                        var successToast = $().toastmessage('showSuccessToast', message);
//                        $().toastmessage('removeToast', successToast);
//                    }
//                });
//            });
//        });   


//////end //////

//    $(document).ready(function () {
//        $("#tblUserActivity tbody tr").each(function () {
//            var perf = $(this).find("td:last-child span").text().toLowerCase();
//            if (perf == "excellent")
//                $(this).find("td:last-child span").addClass("label-success");
//            else if (perf == "very good")
//                $(this).find("td:last-child span").addClass("label-blue");
//            else if (perf == "good")
//                $(this).find("td:last-child span").addClass("label-warning");
//            else if (perf == "average")
//                $(this).find("td:last-child span").addClass("label-yellow");
//            else
//                $(this).find("td:last-child span").addClass("label-red");
//            // compare id to what you want
//        });

//        $("#tblStudentActivity tbody tr").each(function () {
//            var perf = $(this).find("td:last-child span").text().toLowerCase();
//            if (perf == "excellent")
//                $(this).find("td:last-child span").addClass("label-success");
//            else if (perf == "very good")
//                $(this).find("td:last-child span").addClass("label-blue");
//            else if (perf == "good")
//                $(this).find("td:last-child span").addClass("label-warning");
//            else if (perf == "average")
//                $(this).find("td:last-child span").addClass("label-yellow");
//            else
//                $(this).find("td:last-child span").addClass("label-red");
//            // compare id to what you want
//        });
//    });


//    //$(document).ready(function () {
       
//    //    sorttable.init();
        
//    //});