/// <reference path="jquery-1.7.1.js" />
var consoleEnable = true;
if (!window.console) { var console = {}; }
if (!console.log) { console.log = function () { }; }

function Question(id) {
    this.Id = id;
    this.QuestionText = "";
    this.QuestionFormatJson = "";
    this.FormatType = "";
    this.Opt1 = "ABCCC";
    this.Opt2 = "B1";
    this.Opt3 = "C1";
    this.Opt4 = null;
    this.CorrectAnswer = "";
    this.selectedAnswer = "";
    this.Hint = "";
    var self = this;
    self.setOption = function (opt1, opt2, opt3, opt4) {
        this.Opt1 = opt1; this.Opt2 = opt2;
        this.Opt3 = opt3; this.Opt4 = opt4;
    }
    self.UserProgress = null;
}

$('#star').raty({
    path: "/content/images",
    number: 10,
    readOnly: true,
    //      starHalf     : 'star-half.png',                                // The image of the half star.
    //	    starOff      : 'star-off.png',                                 // Name of the star image off.
    //	    starOn       : 'star-on.png' ,                                 // Name of the star image on.
    starHalf: 'star-half.png',
    starOff: 'gray_star.png',
    starOn: 'green_star.png', //'star-on.png'
    score: 0,
    setStarRating: function (score) {
        $('#star').raty('readOnly', false);
        $('#star').raty('score', score);
        $('#star').raty('readOnly', true);
    }
});

var setSartRating = function (score) {
    $('#star').raty('readOnly', false);
    $('#star').raty('score', score);
    $('#star').raty('readOnly', true);
};

var getSartRating2 = function (qId, isCorrect, selectedAnswer) {
    var $url = $(".starRating").attr('href');
    alert($url);
    $url = $url + "&qId=" + qId + "&isCorrect=" + isCorrect + "&opt=" + selectedAnswer;
    
    ajaxGet($url.toLowerCase(), null, function (result) {
        console.log(result);
        setSartRating(result);
    });

};
function IsNumeric(input) {
    var RE = /^-{0,1}\d*\.{0,1}\d+$/;
    return (RE.test(input));
}

function AppViewModel() {

    var self = this;
    self.CurrentQuestion = ko.observable();
    self.nextQuestion = function () {
        $('#Submit').attr("disabled", "disabled");
        var qId = self.CurrentQuestion().Id;
        var isAnsCorrect = self.checkAnswer();
        var selectedAnswer = self.CurrentQuestion().selectedAnswer;
        if (selectedAnswer != null && selectedAnswer != "") {
            if ($.isNumeric(self.CurrentQuestion().CorrectAnswer) && !($.isNumeric(selectedAnswer))) {
                $("#ErrorMessage").html("* Only Numeric answer is allowed");
                $("#ErrorMessage").css("color", "red");
                $('#Submit').removeAttr("disabled");
                return false;
            }
            $("#ErrorMessage").html("");
        }
        else {
            $("#ErrorMessage").html("* Answer is required");
            $("#ErrorMessage").css("color", "red");
            $('#Submit').removeAttr("disabled");
            return false;
        }
        if (isAnsCorrect) {
            showSuccessMessage();
            loadDataFromServer(qId + 1);
        }
        else {

            var correctAnswer = self.CurrentQuestion().CorrectAnswer;
            var AnswerFormatType = self.CurrentQuestion().AnswerFormatType;

            if (correctAnswer == null || correctAnswer == "") {
                var anserObj = self.CurrentQuestion().CorrectAnswerFormat;
                if (anserObj != null && anserObj.AnswerList != null && AnswerFormatType != null && AnswerFormatType == "StringAnswerJsonList") {
                    $.each(anserObj.AnswerList, function (i, row) {
                        correctAnswer = row;
                        return false;
                    });
                }
            }
            showWrongMessage(selectedAnswer, correctAnswer)
            //update Ratings
            loadDataFromServer(qId + 1);
        }
    };
    self.checkAnswer = function () {
        var correct = self.CurrentQuestion().CorrectAnswer.replace(/\s/g, '').trim();
        var selectedAns = self.CurrentQuestion().selectedAnswer.replace(/\s/g, '').trim();
        var AnswerFormatType = self.CurrentQuestion().AnswerFormatType;

        //condition to check answer when AnswerFormatType and AnswerForamtJson is not empty and correct answer is empty
        if (correct == "") {
            var result = false;
            var anserObj = self.CurrentQuestion().CorrectAnswerFormat;

            if (anserObj != null && anserObj.AnswerList != null && AnswerFormatType != null && AnswerFormatType == "StringAnswerJsonList") {
                $.each(anserObj.AnswerList, function (i, row) {
                    //  var correctFinal = row.replace(/\s/g, '').trim();
                    if (compareAnswers(selectedAns, row)) {
                        result = true;
                        return false; //To break the loop
                    }
                    else {
                        result = false;
                    }
                });
            }
            return result;
        }
        return compareAnswers(correct, selectedAns);
    };
    self.CompleteProgress = function () {
        $('#CompleteProgress').attr("disabled", "disabled");
        var selectedAnswer = self.CurrentQuestion().selectedAnswer;
        if (selectedAnswer != null && selectedAnswer != "") {
            $("#ErrorMessage").html("");
            var isAnsCorrect = self.checkAnswer();
            if (isAnsCorrect) {
                showSuccessMessage();
            }
            else {
                var correctAnswer = self.CurrentQuestion().CorrectAnswer;
                var selectedAnswer = self.CurrentQuestion().selectedAnswer;
                showWrongMessage(selectedAnswer, correctAnswer)
            }
            var qId = self.CurrentQuestion().Id;            
            loadDataFromServer(qId + 1);
            $("#PageOpenTimer").fadeOut();
        }
        else {
            $("#ErrorMessage").html("* Answer is required");
            $("#ErrorMessage").css("color", "red");
            $('#CompleteProgress').removeAttr("disabled");
            return false;
        }
    }
    self.isSelected = function (self) {
        var t = null;
        if (self.CurrentQuestion() != 'undefined')
            t = self.CurrentQuestion().selectedAnswer;
        if (t != null && t != "")
            return true;
        else
            return false;

        return false;
    };

    function getSampleQuestion() {
        var q = new Question(1);
        q.QuestionText = "Sample Question";

        q.setOption("A" + id, "B" + id, "C" + id, "D" + id);
        self.CurrentQuestion(q);
        console.log(self.CurrentQuestion());
    }

    function showSuccessMessage() {

        // reconfiguring the toasts as sticky
        $().toastmessage({ sticky: true, inEffectDuration: 3000, stayTime: 5000, position: 'middle-center' });
        var i = Math.floor((Math.random() * 5) + 1);
        var message = ["Good", "Great", "Awesome", "Correct", "Fantastic", "Excellent"];
        var successToast = $().toastmessage('showSuccessToast', message[i]);

        // removing the toast
        $().toastmessage('removeToast', successToast);
    }

    function showWrongMessage(yourAnswer, correctAnswer) {
        var message = "WRONG <br /> <br />" + "You answered:  " + yourAnswer + ",      Correct Answer:   " + correctAnswer;

        $().toastmessage({ inEffectDuration: 3000, stayTime: 8000, position: 'middle-center' });
        var i = Math.floor((Math.random() * 5) + 1);

        var successToast = $().toastmessage('showWarningToast', message);
        $().toastmessage('removeToast', successToast);
    }

    //Compare SelectedAnswer and CorrectAnswer
    function compareAnswers(selectedAns, correctAnswer) {
        var youranswer = selectedAns.replace(/\s/g, '').trim();
        var correct = correctAnswer.replace(/\s/g, '').trim();

        //condition to check answer and correct answer in string formate same or not
        if (correct == youranswer) {
            return true;
        }
        //Check if GivenAnswer and DB Correct answer is numeric/decimal or Not
        else if ($.isNumeric(youranswer) && $.isNumeric(correct)) {
            if (parseFloat(youranswer) == parseFloat(correct))
                return true;
            else
                return false;
        }
        else
            return false;
    }

    function loadDataFromServer(id) {
        var subTopicId = $(".subtopic").text();
        var topicId = $(".topic").text();
        var minutes = $(".textDiv_Minutes span").text();
        var seconds = $(".textDiv_Seconds span").text();
        var CompletionTime = parseInt(minutes) * 60 + parseInt(seconds);
        console.log(subTopicId);
        if (id == -1) {            
            var $url = ("/math-problem/" + topicId + "/" + subTopicId).toLowerCase();
            var data = { id: subTopicId, prevId: 0, opt: "", CompletionTime: CompletionTime };
            var json = JSON.stringify(data);
            //_gaq.push(['_trackPageview', $url]);
            ga('send', 'pageview', $url);
            ajaxAdd($url.toLowerCase(), json, function (result) {
                result.QuestionText = result.QuestionText != null ? result.QuestionText.replace("??", "<span class='question-mark-text'>?</span>") : result.QuestionText;
                self.CurrentQuestion(result);
                console.log(self.CurrentQuestion());
                var uProgress = self.CurrentQuestion() != null ? self.CurrentQuestion().UserProgress : null;
                if (uProgress != null) {
                    var starpoints = uProgress.StarPoints;
                    setSartRating(starpoints);
                    $('#Submit').show();
                    $('#CompleteProgress').hide();
                    $("#txtAnswer").focus();
                }
            });
        }
        else {

            console.log('before');
            console.log(self.CurrentQuestion());

            var selectedAnswer = self.CurrentQuestion().selectedAnswer.trim();
            var $url = ("/math-problem/" + topicId + "/" + subTopicId).toLowerCase();
            
            //_gaq.push(['_trackPageview', $url]);
            ga('send', 'pageview', $url);
            var data = { id: subTopicId, prevId: self.CurrentQuestion().Id, opt: selectedAnswer, CompletionTime: CompletionTime };
            var json = JSON.stringify(data);

            ajaxAdd($url.toLowerCase(), json, function (result) {
                result.QuestionText = result.QuestionText != null ? result.QuestionText.replace("??", "<span class='question-mark-text'>?</span>") : result.QuestionText;

                self.CurrentQuestion(result);
                console.log(self.CurrentQuestion());
                var uProgress = self.CurrentQuestion() != null ? self.CurrentQuestion().UserProgress : null;
                if (uProgress != null) {
                    var starpoints = uProgress.StarPoints;
                    setSartRating(starpoints);
                    if (starpoints == 9) {
                        $("#txtAnswer").focus();
                        $('#Submit').hide();
                        $('#CompleteProgress').show();
                    }
                    else if (starpoints > 9) {
                        self.CurrentQuestion(null);
                        console.log(self.CurrentQuestion());
                        $('.ProgressCompleted').css('display', 'block');
                        if ($("#IdAuthUser").val() == "false") {
                            $('.ProgressCompletedSignUp').css('display', 'block');
                        }
                    }
                    else {
                        $("#txtAnswer").focus();
                        $('#Submit').show();
                        $('#CompleteProgress').hide();
                    }
                }
            });
        }
    };
    loadDataFromServer(-1);
}

// Activates knockout.js
ko.applyBindings(new AppViewModel(), document.getElementById("aContainer"));