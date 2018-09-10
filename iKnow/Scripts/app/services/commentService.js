var CommentService = (function () {
    var getComments = function (success, answerId, currentPage) {
        $.ajax({
            url: "/question/getcomments/" + answerId + "/" + currentPage,
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
        getComments: getComments,
        postComment: postComment
    }
})();