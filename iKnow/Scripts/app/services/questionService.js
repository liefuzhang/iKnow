var QuestionService = (function () {
    var getForm = function (success, questionId) {
        $.ajax({
            url: "/question/getform" + (questionId ? "/" + questionId : ""),
            dataType: "html",
            success: success,
            fail: function () {
                console.error("Something went wrong with QuestionService getForm.");
            }
        });
    };

    var getTopic = function (success, questionId) {
        $.ajax({
            url: "/question/gettopic/" + questionId,
            dataType: "html",
            success: success,
            fail: function () {
                console.error("Something went wrong with QuestionService getForm.");
            }
        });
    };

    return {
        getForm: getForm,
        getTopic: getTopic
    }
})();