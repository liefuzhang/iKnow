var AnswerFooterController = (function (answerFooterService) {
    var getComments = function ($commentList, answerId, pageNumber) {
        $commentList.html('');
        $commentList.next(".comment-loading").removeClass("hide");

        var success = function (html) {
            $commentList.html(html);
            $commentList.next(".comment-loading").addClass("hide");
        }

        answerFooterService.getComments(success, answerId, pageNumber);
    }

    var getPageComments = function () {
        var $commentPage = $(this);
        var pageNumber = $commentPage.attr("data-page-number");
        var $commentList = $commentPage.closest(".comment-list");
        var $answerComment = $commentList.closest(".answer-comment");
        var answerId = $answerComment.attr("data-answer-id");

        getComments($commentList, answerId, +pageNumber);
    }

    var getTotalCommentText = function(commentCount) {
        return commentCount > 0 ? `${commentCount} Comment(s)` : "Add Comment";
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
            var totalCommentCount = $commentButtonText.attr("data-total-count");
            $commentButtonText.html(getTotalCommentText(totalCommentCount));
        }
    }

    var postCommentUpdateTotalCount = function ($commentContainer) {
        var $commentHeaderText = $commentContainer.find(".comment-header strong");
        var $commentButtonText = $commentContainer.closest(".answer-footer").find(".comment-button-text");
        var totalCommentCount = $commentButtonText.attr("data-total-count");

        totalCommentCount = +totalCommentCount + 1;
        $commentButtonText.attr("data-total-count", totalCommentCount);
        var totalCommentCountText = getTotalCommentText(totalCommentCount);
        $commentHeaderText.html(totalCommentCountText);
        $commentButtonText.html(totalCommentCountText);
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

            postCommentUpdateTotalCount($commentContainer);
            getComments($commentList, answerId, newTotalPageCount);
        };

        answerFooterService.postComment(success, answerId, comment);
    }

    var toggleLike = function () {
        var $likeButton = $(this);
        var $footerBar = $likeButton.parent(".answer-footer-bar");
        var answerId = $footerBar.attr("data-answer-id");
        if ($likeButton.hasClass("answer-liked")) {
            
        } else {
            var success = function() {
                $likeButton.addClass("answer-liked");
            }
            answerFooterService.likeAnswer(success, answerId);
        }
    }

    var init = function () {
        $(".answer-footer-bar .comment-button").on("click", toggleComment);
        $(".answer-comment").on("input", "textarea", appController.textareaAutoGrow);
        $(".answer-comment").on("click", ".write-comment .btn", postComment);
        $(".answer-comment").on("click", ".comment-pagination-container .comment-page", getPageComments);
        $(".answer-footer-bar .like-button").on("click", toggleLike);
    };

    return {
        init: init
    }
})(AnswerFooterService);

