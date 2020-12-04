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

    var getTotalCommentText = function (commentCount) {
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

    var getTotalLikeText = function (likeCount, liked) {
        return likeCount > 0 ? (liked ? `Liked ${likeCount}` : `Like ${likeCount}`) : "Like";
    }

    var toggleLike = function () {
        var $likeButton = $(this);
        var $footerBar = $likeButton.parent(".answer-footer-bar");
        var answerId = $footerBar.attr("data-answer-id");
        var totalLikeCount = $likeButton.attr("data-like-count");
        var $likeButtonText = $likeButton.find(".like-button-text");
        if ($likeButton.hasClass("answer-liked")) {
            var unlikeSuccess = function () {
                $likeButton.removeClass("answer-liked");
                totalLikeCount = +totalLikeCount - 1;
                $likeButtonText.html(getTotalLikeText(totalLikeCount, false));
                $likeButton.attr("data-like-count", totalLikeCount);
            }
            answerFooterService.unlikeAnswer(unlikeSuccess, answerId);
        } else {
            var likeSuccess = function () {
                $likeButton.addClass("answer-liked");
                totalLikeCount = +totalLikeCount + 1;
                $likeButtonText.html(getTotalLikeText(totalLikeCount, true));
                $likeButton.attr("data-like-count", totalLikeCount);
            }
            answerFooterService.likeAnswer(likeSuccess, answerId);
        }
    }

    var init = function () {
        var $container = $(".question-answer-container, .activity-whole-panel");
        $container.on("click", ".answer-footer-bar .comment-button", toggleComment);
        $container.on("input", ".answer-comment textarea", appController.textareaAutoGrow);
        $container.on("click", ".answer-comment .write-comment .btn", postComment);
        $container.on("click", ".answer-comment .comment-pagination-container .comment-page", getPageComments);
        $container.on("click", ".answer-footer-bar .like-button", toggleLike);
    };

    return {
        init: init
    }
})(AnswerFooterService);

