///DEBUGGER FUNCTION && CONSOLE FUNCTION

function addDebugger() {
    debugger;
}

function PrintLog(value)
{
    console.log(value);
}


function isNotNullAndUndef(variable) {

    return (variable !== null && variable !== undefined && variable !== '');
}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////

function validateFields() {
    if ($("#txtAdminEmail").val() != "") {
        var email = $("#txtAdminEmail").val();
        var Phone = $("#Phone").val();
        //addDebugger();
        if (email != "") {
            if (!isValidEmailAddress(email)) {
                $("#divSuccess").html("Invalid Email");
                $("#divSuccess").css("display", "block");
                $("#divSuccess").removeClass("alert-success");
                $("#divSuccess").addClass("alert-danger");
                return false;
            }
            else {
                $("#divSuccess").css("display", "none");
            }
        }        
        if (Phone != "") {
            if (!isValidPhone(Phone)) {
                $("#divSuccess").html("Enter a valid 10-digit phone number with or without hyphen");
                $("#divSuccess").css("display", "block");
                $("#divSuccess").removeClass("alert-success");
                $("#divSuccess").addClass("alert-danger");
                return false;
            }
        }
        else {
            $("#divSuccess").css("display", "none");
        }
        return true;
    }
    else {
        $("#divSuccess").html("Please enter Email.");
        $("#divSuccess").css("display", "block");
        $("#divSuccess").removeClass("alert-success");
        $("#divSuccess").addClass("alert-danger");
        return false;
    }
}

function showSuccessMessage(result) {    
    if (result.toLowerCase().indexOf("already registered") > 0 || result.toLowerCase().indexOf("error") > 0) {
            $("#divSuccess").css("display", "block");
            $("#divSuccess").removeClass("alert-success");
            $("#divSuccess").addClass("alert-danger");            
        }
        else {
            $("#divSuccess").css("display", "block");
            $("#divSuccess").removeClass("alert-danger");
            $("#divSuccess").addClass("alert-success");            
        }
        $("#divSuccess").html(result);
    }
//});
$(document).ready(function () {
    $(".setting_click").click(function () {
        var X = $(this).attr('id');
        if (X == 1) {
            $(".setting_list").hide();
            $(this).attr('id', '0');
        }
        else {
            $(".setting_list").show();
            $(this).attr('id', '1');
        }

    });

    $(".setting_click").mouseup(function () {
        return false
    });

    $(document).mouseup(function () {
        $(".setting_list").hide();
        $(".setting_click").attr('id', '');
    });
});

//$(document).ready(function () {    
    function checkSubscriberEmail() {
        if ($("#txtFooterEmail").val() == '') {
            $(".divSubscriberMessage").html("* Email Required.");
            $(".divSubscriberMessage").css("display", "block");
            setTimeout(function () {
                $(".divSubscriberMessage").fadeOut();
            }, 2000);

            return false;
        }
        else {
            if (!isValidEmailAddress($("#txtFooterEmail").val())) {
                $(".divSubscriberMessage").html("* Invalid Email Address");
                $(".divSubscriberMessage").css("display", "block");
                $('#txtFooterEmail').val('');
                setTimeout(function () {
                    $(".divSubscriberMessage").fadeOut();
                }, 2000);
                return false;
            }
            else {
                $(".divSubscriberMessage").css("display", "none");
                return true;
            }
        }
    }
    function ShowSubscriberEmail(result) {
        $("#txtFooterEmail").val('');
        $(".divSubscriberMessage").html(result);
        $(".divSubscriberMessage").css("display", "block");
        setTimeout(function () {
            $(".divSubscriberMessage").fadeOut();
        }, 2000);
    }
    function isValidEmailAddress(emailAddress) {
        var pattern = new RegExp(/^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$/i);
        return pattern.test(emailAddress);
    }
    function isValidPhone(Phone) {
        var filter = new RegExp(/^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/);
        return filter.test(Phone);
    }
//});
//////////////////////////////////////////////////////////////////////////////////////////////////////////////
//Grade 10 page notify me
//$(document).ready(function () {
    function successNotification(result) {
        if (result.toLowerCase().indexOf("already registered") > 0 || result.toLowerCase().indexOf("error") > 0) {
            $("#divmsg").addClass("row0 ErrorOccurs");
            $("#divmsg").removeClass("result");
        }
        else {
            $("#divmsg").addClass("result");
            $("#divmsg").removeClass("row0 ErrorOccurs");
            $("#txtEmail").val("");
        }        
        $("#divmsg").html(result);
    }
    function checkEmailNotification() {

        if ($("#txtEmail").val() == '') {
            $("#divmsg").html("* Email Required.");
            $("#divmsg").addClass("row0 ErrorOccurs");
            $("#divmsg").removeClass("result");
            return false;
        }
        else {
            if (!isValidEmailAddress($("#txtEmail").val())) {
                $("#divmsg").html("* Invalid Email Address");
                $("#divmsg").addClass("row0 ErrorOccurs");
                $("#divmsg").removeClass("result");
                $('#txtEmail').val('');
                return false;
            }
            else {
                $("#divmsg").html("");
                $("#divmsg").addClass("result");
                $("#divmsg").removeClass("row0 ErrorOccurs");
                return true;
            }
        }
    }
//});
//////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// Used in RegisterChildAccountPartial
//$(document).ready(function () {
    function successRegisterPartial(result) {
        if (result == 'false') {
            $("#btnRegisterSubmit").removeAttr("disabled");
            $("#errorOccurs").css("display", "block");
        }
        else {
            $("#errorOccurs").css("display", "none");
            $('#registerModal').modal('hide')
            window.location.href = '/membership/registrationsuccess';
        }
    }
    function BeginRegisterPartial() {
        $("#btnRegisterSubmit").attr("disabled", "disabled");
    }

    function resultLoginCommon(result) {
        if (result == 'false') {
            $("#btnLoginSubmit").removeAttr("disabled");
            $("#errorOccurs").css("display", "block");
        }
        else {
            $("#errorOccurs").css("display", "none");

            var returlUrl = result;

            var $d = $('#returnUrl');
            if ($d.length != 0) {
                if ($d.val().length != 0) {
                    var returlUrl = $d.val();
                }
            }
            window.location.href = returlUrl;
        }
    }
    function BeginLoginCommon() {
        $("#btnLoginSubmit").attr("disabled", "disabled");
    }

///************************* Scripts for register page//////

    $(document).ready(function () {
        var role = $("#RoleType").val();        
        if ((role != null || role != "undefined") && role == "teacher") {
            $("#Email").focus(function () {
                $("#divWarning").slideToggle("slow");
            });

            $("#Email").focusout(function () {
                $("#divWarning").slideToggle("slow");
            });
        }

        $("#Email").blur(function () {
           var email = $("#Email").val().replace(/^\s+|\s+$/g, '');
            $("#Email").val(email);
        });


        $("#Email").focusout(function () {
            var $url = "/membership/IsEmailExists";
            var email = $("#Email").val().replace(/^\s+|\s+$/g, '');
            $("#Email").val(email);
            $url = $url.toLowerCase();
            $.ajax({
                type: "GET",
                url: $url,
                data: { Email: email },
                success: function (data) {
                    if (data=="True") {
                        $("#divErrEmail").html("");
                    }
                    else {
                        $("#divErrEmail").html("Email Id Already Exists");                        
                    }
                }
            });
        });

        $('#btnRegisterSubmit').click(function () {
        var success = true;
        if ($("#UserName").val() == "" || $("#Email").val() == "" || $("#Password").val() == "" || $("#ConfirmPassword").val() == "") {
            return true;
        }
        else if ($("#TermsOfMemeberShip").is(':checked')) {
            $("#chkTerms").hide();
            success = true;
        }
        else {
            $("#chkTerms").show();
            success = false;
        }
       
        var role = $("#RoleType").val();
        if ((role != null || role != "undefined") && role == "teacher") {
            if ($("#SchoolName").val() == "") {
                $("#divErrSchoolName").html("School Name is Required");
                success = false;
            }
            else {
                $("#divErrSchoolName").html("")
            }
        }
        if ($("#Phone").val() == "") {
            $("#divErrPhone").html("Phone is Required");
            success = false;
        }
        else {
            $("#divErrPhone").html("");
        }
        
        if (success) {
            $("#FormRegister").submit();
        }
        else
            return success;
    });

    $("#SchoolName").focusout(function () {
        if ($("#SchoolName").val() != "") {
            $("#divErrSchoolName").html("");
        }
    });

    $("#Phone").focusout(function () {
        if ($("#Phone").val() != "") {
            $("#divErrPhone").html("");
        }
    });
});

    ///************************* Scripts for login page//////

    $(document).ready(function () {
        $('form').on('submit', function (e) {
            var $form = $(this);
            var IsClass = false;
            var classVal = $form.attr("class");
            if (classVal != "undefined" && classVal != undefined) {
                if (classVal.indexOf('preventReSubmission') > 0) {
                    IsClass = true;
                    //PrintLog(IsClass);
                }
            }
            
            if ($form.attr("id") === "frmLogin" || $form.attr("id") === "FormRegister" || IsClass) {
                if ($form.data('submitted') === true) {
                    // Previously submitted - don't submit again
                    e.preventDefault();
                    
                    //PrintLog("supressed double submission");
                } else {
                    // Mark it so that the next submit can be ignored
                    $form.data('submitted', true);
                    //PrintLog("submitting first time");
                }
            }
        });
    });


//*********Complete Admin profile******************************
$(document).ready(function () {
    $('#AdminProfileModal').modal();
});

$('#btnSkip').click(function () {
    $('#AdminProfileModal').modal('hide');
});

//****************************************
//*********Complete User profile******************************
$(document).ready(function () {
    $('#UserProfileModal').modal();
});

$('#btnSkipUser').click(function () {
    $('#UserProfileModal').modal('hide');
});


//****************************************

//function will return true when value is null
function IsValidOrEmpty(value) {
    var success = true;
    if (value != null && value != "NULL") {
        success = false;
    }
    else if (value == null || value == "NULL") {

        success = true;
    }
    return success;
}



///code for show user accounts

$(document).ready(function () {
    $("#btnShowUser").click(function () {
        $.ajax({
            url: '/membership/searchuser',
            data: { userId: $("#txtSwitchUserId").val() },
            cache: false,
            type: "GET",
            async: false,
            success: function (data) {
                $('#divUserAccounts').html(data);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var message = "";
                if (jqXHR.status == 500) {
                    message = "Opps ! Some server side error occured. Please  try again.";
                } else if (jqXHR.status == 404) {
                    message = "Opps ! your internet connection is not working. Please try again.";
                }
                else {
                    message = "Opps ! some error occured. Please try again.";
                }
                $().toastmessage({ sticky: true, inEffectDuration: 4000, stayTime: 6000, position: 'middle-center' });
                var successToast = $().toastmessage('showSuccessToast', message);
                $().toastmessage('removeToast', successToast);
            }
        });
    });
});


$("#Others").change(function () {
    if (this.checked) {
        $("#divRefByOther").removeClass("hide");
    }
    else {
        $("#divRefByOther").addClass("hide");
    }
});

//Grade links 

$(".showmore").click(function () {
    var Id = $(this).attr("id");
    if ($("." + Id).hasClass("hide")) {
        $("." + Id).removeClass("hide");
        $(this).text("Hide.....");
    }
    else
    {
        $("." + Id).addClass("hide");
        $(this).text("Show more....");
    }
})


//ga('send', 'event', eventCat, 'CompleteProgress', eventLabel, { 'nonInteraction': 1 });

$("#CanvasQLinks").click(function () {
    var href = $(this).attr("href");
});

// ajax call to log UserStats
function AddUserStats(userAction,remarks)
{
    $.ajax({
        url: '/membership/adduserstats',
        data: { userAction: userAction,Remarks:remarks },
        cache: false,
        type: "POST",
        async: false,
        success: function (data) {
            PrintLog(data);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            PrintLog(errorThrown);
        }
    });
}




//function to track event

function trackEventByAnalystics(eventCategory,eventLabel)
{
    ga('send', 'event', eventCategory, 'click', eventLabel, { 'nonInteraction': 1 });
}

