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

    var getComments = function (success, answerId) {
        $.ajax({
            url: "/question/getcomments/" + answerId,
            dataType: "html",
            success: success,
            fail: function () {
                console.error("Something went wrong with QuestionService getComments.");
            }
        });
    };

    var postComment = function (success, answerId, comment) {
        $.ajax({
            url: "/api/answercomment",
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ "answerId": answerId, "comment": comment }),
            success: success,
            fail: function () {
                console.error("Something went wrong with QuestionService postComment.");
            }
        });
    }

    return {
        getForm: getForm,
        getTopic: getTopic,
        getComments: getComments,
        postComment: postComment
    }
})();