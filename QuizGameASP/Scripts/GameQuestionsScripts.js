//ZASTANÓW SIĘ!! KLASY .current i .hidden są NIEPOTRZEBNE

//VARIABLES
var questionNumber = 0;
var questions = document.getElementsByClassName("question-content");;

var points = 0;
var answerString = "";

var progressBarInterval;
//var progressBarCurrent = $(".progress-bar").width();
var progressBarTotal = 8000; //total time in ms
var progressBarCurrent = progressBarTotal;
var step = 5;

//FUNCTIONS
function progressBarUpdate() {
    //progressBarCurrent -= 0.5;
    progressBarCurrent -= step;
    var percent = progressBarCurrent / progressBarTotal * 100;

    if (progressBarCurrent < 0) {
        clearInterval(progressBarInterval);
        removeAnswerClickHandlers();
        answerString += "0;";
        setTimeout(function () {
            questionContainer.hide("slow");
            if (questionNumber <= 1) {
                questionCategoryContainer.show("slow");
            }
            setTimeout(nextQuestion, 500);
        }, 1000);
        return;
    }

    percent += "%";
    $(".progress-bar-above").width(percent);
}

function progressBarReset() {
    progressBarCurrent = progressBarTotal;
    $(".progress-bar-above").width("100%");
}

function progressBarGo() {
    progressBarInterval = setInterval(progressBarUpdate, step);
}

function progressBarStop() {
    clearInterval(progressBarInterval);
}

function nextQuestion() {
    var question = $(questions[questionNumber]);
    question.removeClass("question-current");
    question.addClass("question-hidden");

    questionNumber++;

    if (questionNumber >= 3) {
        //alert("Twój wynik to: " + points);
        window.location = "/Game/Result?result=" + points + "&answerString=" + answerString;
        return;
    }

    question = $(questions[questionNumber]);
    question.removeClass("question-hidden");
    question.addClass("question-current");

    addAnswerClickHandlers();

    progressBarReset();
    //progressBarGo();
}

function addAnswerClickHandlers() {
    for (i = 0; i < questions.length; i++) {
        var answers = $(questions[i]).children(".question-answers").children(".question-answer");
        $(answers).on("click", answerClickEventHandler);
    }
}

function removeAnswerClickHandlers() {
    for (i = 0; i < questions.length; i++) {
        var answers = $(questions[i]).children(".question-answers").children(".question-answer");
        $(answers).off("click", answerClickEventHandler);
    }
}

var questionContainer = $(".question-container");
var questionCategoryContainer = $("#question-category-btn-container");

function answerClickEventHandler() {
    removeAnswerClickHandlers();
    progressBarStop();

    var answerBtn = $(this);
    var questionId = $(this).parent().parent().children(".question-id").attr("id"); //get .question-content
    var answerTxt = $(this).text();

    $.ajax({
        url: "/Game/CheckAnswer/",
        data: {
            id: questionId,
            answer: answerTxt,
        },
        dataType: "text",
        type: "GET",
        success: function (data) {
            if (data == "ok") {
                answerBtn.css("background-color", "green");
                points++;
                answerString += "1;";
                //setTimeout(nextQuestion, 1000);
            } else {
                if (data == "wrong") {
                    answerBtn.css("background-color", "red");
                    answerString += "0;"
                }
            }
        },
        error: function () {
            alert("Błąd.");
        },
        complete: function () {
            //setTimeout(nextQuestion, 1000);
            setTimeout(function () {
                questionContainer.hide("slow");
                if (questionNumber <= 1) {
                    questionCategoryContainer.show("slow");
                }
                setTimeout(nextQuestion, 500); //timeout żeby pytanie nie pojawiło się, nim nie zniknie questionContainer
            }, 1000);
        }
    });
}

//ADD CLASSES FOR ELEMENTS WITCH DISPLAY QUESTIONS
//PREPARE FIRST QUESTION
$(questions[0]).addClass("question-current");
$(questions[1]).addClass("question-hidden");
$(questions[2]).addClass("question-hidden");
addAnswerClickHandlers();

//START
//progressBarGo();

if ($(window).height() <= 480) {
    $("#question-category").remove();
}

questionCategoryContainer.show("slow");

questionCategoryContainer.on("click", function () {
    questionCategoryContainer.hide("slow");
    questionContainer.show("slow");
    progressBarGo();
});