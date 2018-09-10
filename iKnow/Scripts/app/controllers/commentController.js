var CommentController = (function (commentService) {
    var getComments = function ($commentList, answerId, pageNumber) {
        $commentList.html('');
        $commentList.next(".comment-loading").removeClass("hide");

        var success = function (html) {
            $commentList.html(html);
            $commentList.next(".comment-loading").addClass("hide");
        }

        commentService.getComments(success, answerId, pageNumber);
    }

    var getPageComments = function () {
        var $commentPage = $(this);
        var pageNumber = $commentPage.attr("data-page-number");
        var $commentList = $commentPage.closest(".comment-list");
        var $answerComment = $commentList.closest(".answer-comment");
        var answerId = $answerComment.attr("data-answer-id");

        getComments($commentList, answerId, +pageNumber);
    }

    var toggleComment = function () {
        var $footerBar = $(this).parent(".answer-footer-bar");
        var $commentButtonText = $footerBar.find(".comment-button-text");
        var $answerComment = $footerBar.next(".answer-comment");
        if ($answerComment.hasClass("hide")) {
            var answerId = $answerComment.attr("data-answer-id");
            var $commentList = $answerComment.find(".comment-list");
            getComments($commentList, answerId, 1);
            $answerComment.removeClass("hide");
            $commentButtonText.html("Hide Comment");
        } else {
            $answerComment.addClass("hide");
            var totalCommentCount = $footerBar.attr("data-total-comment");
            $commentButtonText.html(totalCommentCount);
        }
    }

    var postComment = function () {
        var $this = $(this);
        var answerId = $this.attr("data-answer-id");
        var $comment = $this.prev();
        var comment = $comment.val().trim();
        if (!comment)
            return;

        var success = function (newTotalPageCount) {
            $comment.val('');
            var $commentContainer = $this.closest(".comment-container");
            var $commentList = $commentContainer.find(".comment-list");

            getComments($commentList, answerId, newTotalPageCount);
        };
        commentService.postComment(success, answerId, comment);
    }

    var init = function () {
        $(".answer-footer-bar .comment-button").on("click", toggleComment);
        $(".answer-comment").on("input", "textarea", appController.textareaAutoGrow);
        $(".answer-comment").on("click", ".write-comment .btn", postComment);
        $(".answer-comment").on("click", ".comment-pagination-container .comment-page", getPageComments);
    };

    return {
        init: init
    }
})(CommentService);

