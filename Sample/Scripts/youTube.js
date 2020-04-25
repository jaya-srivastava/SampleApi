$(document).ready(function () {
    CreateVideo("divLcm", "435", "580", "https://www.youtube.com/embed/AdfqsLq1mCE?list=PLJ-ma5dJyAqp7SsptXjw96DE1qfwMZJ9D");
    function CreateVideo(divId, height, width, url) {
        var str = "<iframe width=" + width + " height=" + height + " src=" + url + " frameborder='0'></iframe>";
        $("#" + divId).html(str);
    }
});