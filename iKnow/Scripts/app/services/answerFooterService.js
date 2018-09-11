var AnswerFooterService = (function () {
    var getComments = function (success, answerId, currentPage) {
        $.ajax({
            url: "/question/getcomments/" + answerId + "/" + currentPage,
            dataType: "html",
            success: success,
            fail: function () {
                console.error("Something went wrong with AnswerFooterService getComments.");
            }
        });
    };

    var postComment = function (success, answerId, comment) {
        $.ajax({
            url: "/answerFooter/postComment",
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ "answerId": answerId, "comment": comment }),
            success: success,
            fail: function () {
                console.error("Something went wrong with AnswerFooterService postComment.");
            }
        });
    }

    var likeAnswer = function (success, answerId) {
        $.ajax({
            url: "/answerFooter/likeAnswer/" + answerId,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            success: success,
            fail: function () {
                console.error("Something went wrong with AnswerFooterService likeAnswer.");
            }
        });
    }

    return {
        getComments: getComments,
        postComment: postComment,
        likeAnswer: likeAnswer
    }
})();