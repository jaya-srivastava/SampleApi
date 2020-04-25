/// <reference path="jquery-1.11.2.js" />
$(document).ready(function () {

    $.ajaxSetup({ cache: false });
});
    function ajaxGet(url, dataToSave, callback, successMsg) {
        ajaxModify(url, dataToSave, "GET", successMsg, callback, false);
    }

    function ajaxAdd(url, dataToSave, callback, successMsg) {
        ajaxModify(url, dataToSave, "POST", successMsg, callback, false);
    }

    function ajaxUpdate(url, dataToSave, successCallback, successMsg) {
        ajaxModify(url, dataToSave, "PUT", successMsg, successCallback, false);
    }

    function ajaxDelete(url) {
        ajaxModify(url, null, "DELETE", successMsg, false);
    }

    function ajaxModify(url, dataToSave, httpVerb, successMessage, callback, successNotify) {
 
        $.ajax(url, {
            data: dataToSave,
            type: httpVerb,
            dataType: 'text',
            contentType: 'application/json',
            success: function (data) {
                var data = data ? $.parseJSON(data) : null;
                if (successNotify) {
                    $.notifyBar({
                        html: successMessage,
                        cls: "success",
                    });
                }
                if (callback !== undefined) {
                    callback(data);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                var message = "";
                if (jqXHR.status == 500) {
                    message = "Opps ! Some server side error occured. Please  try again.";
                } else if (jqXHR.status == 404) {
                    message = "Opps ! your internet connection is not working. Please try again.";
                }
                else if (jqXHR.status == 301 || jqXHR.status == 302) {
                    message = "Url is moved permanently";
                    alert(jqXHR);
                    var redirect = null;
                    try {
                        redirect = response.getResponseHeader('Location');
                        if (redirect) {
                            //window.location.href = redirect.replace(/\?.*$/, "?next=" + window.location.pathname);
                        }
                    } catch (e) {
                        return;
                    }
                }
                else {
                    message = "Opps ! some error occured. Please try again.";
                }

                $().toastmessage({ sticky: true, inEffectDuration: 4000, stayTime: 6000, position: 'middle-center' });
                var successToast = $().toastmessage('showSuccessToast', message);
                $().toastmessage('removeToast', successToast);
            }
        });
    }