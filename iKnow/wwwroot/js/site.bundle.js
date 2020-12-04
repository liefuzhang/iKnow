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

    var unlikeAnswer = function (success, answerId) {
        $.ajax({
            url: "/answerFooter/unlikeAnswer/" + answerId,
            type: 'DELETE',
            contentType: "application/json; charset=utf-8",
            success: success,
            fail: function () {
                console.error("Something went wrong with AnswerFooterService unlikeAnswer.");
            }
        });
    }

    return {
        getComments: getComments,
        postComment: postComment,
        likeAnswer: likeAnswer,
        unlikeAnswer: unlikeAnswer
    }
})();
var LoadMoreService = (function () {
    var loadMore = function (controllerName, currentPage, success, queryString) {
        $.ajax({
            url: "/" + controllerName + "/loadmore/" + currentPage + (queryString ? queryString : ""),
            dataType: "html",
            success: success,
            fail: function () {
                console.error("Something went wrong with LoadMoreService loadMore.");
            }
        });
    };

    return {
        loadMore: loadMore
    }
})();
var PhotoUploadService = (function () {
    var getForm = function (success, userId) {
        $.ajax({
            url: "/account/editProfilePhoto/" + userId,
            dataType: "html",
            success: success,
            fail: function () {
                console.error("Something went wrong with PhotoUploadService getForm.");
            }
        });
    };

    var savePhoto = function (userId, dataURL, done) {
        $.ajax({
            url: "/api/account",
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ "userId": userId, "dataURL": dataURL }),
            success: done,
            fail: function() {
                console.error("Something went wrong with PhotoUploadService savePhoto.");
            }
        });
    };

    return {
        getForm: getForm,
        savePhoto: savePhoto
    }
})();
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
var RegisterService = (function () {
    var getRegisterForm = function(success, queryString) {
        $.ajax({
            url: "/account/register" + queryString,
            dataType: "html",
            success: success,
            fail: function () {
                console.error("Something went wrong with RegisterService getRegisterForm.");
            }
        });
    };

    return {
        getRegisterForm: getRegisterForm
    }
})();
var SearchService = (function () {
    var search = function (term, success) {
        $.ajax({
            url: "/search/getresult?input=" + term,
            dataType: "html",
            success: success,
            fail: function () {
                console.error("Something went wrong with SearchService search.");
            }
        });
    }

    return {
        search: search
    }
})();
var TopicService = (function () {
    var selectTopic = function (topicId, success) {
        $.ajax({
            url: "/topic/about/" + topicId,
            dataType: "html",
            success: success,
            fail: function () {
                console.error("Something went wrong with TopicService selectTopic.");
            }
        });
    };

    var followTopic = function (topicId, done, fail) {
        $.post("/api/topicfollowing", { "": topicId })
            .done(done)
            .fail(fail);
    }

    var unfollowTopic = function (topicId, done, fail) {
        $.ajax({
            url: "/api/topicfollowing/" + topicId,
            method: "DELETE"
        })
            .done(done)
            .fail(fail);
    }

    return {
        selectTopic: selectTopic,
        follow: followTopic,
        unfollow: unfollowTopic
    }
})();
var AnswerController = (function () {
    var toggleMoreAnswerUnderline = function (e) {
        $(e.currentTarget).find(".blue-link-color").toggleClass("underline");
    }

    var showMoreAnswer = function (e) {
        var $this = $(e.currentTarget);
        $this.addClass("hide");
        $this.next().removeClass("hide");
    }

    var hideMoreAnswer = function (e) {
        var $this = $(e.currentTarget);
        var $container = $this.closest(".expanded-answer-container");
        var $author = $container.siblings(".question-author");
        var $window = $(window);
        var $outerContainer = $container.closest(".question-answer-inner");
        var diff = $outerContainer.height() - $window.scrollTop();

        $container.addClass("hide");
        $container.prev().removeClass("hide");

        if ($window.scrollTop() > $author.offset().top) {
            var scrollTo = $outerContainer.height() - diff;
            $window.scrollTop(scrollTo);
        }
    }

    var init = function () {
        var $container = $(".question-answer-container, .activity-whole-panel");
        $container.on("mouseenter", ".short-answer-container", toggleMoreAnswerUnderline);
        $container.on("mouseleave", ".short-answer-container", toggleMoreAnswerUnderline);
        $container.on("click", ".short-answer-container", showMoreAnswer);
        $container.on("click", ".collapse-answer", hideMoreAnswer);

        AnswerFooterController.init();
    };

    return {
        init: init
    }
})();


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


var HeaderController = (function (questionService, searchService) {
    var toggleModalAddQuestion = function () {
        var $modalContainer = $(".modal-container");
        if (iknow.isUserAuthorized !== true) {
            WarningErrorController.showWarning("Please log in before you add question");
            return;
        }
        if ($modalContainer.hasClass("new-question-form-loaded")) {
            ModalController.open();
            return;
        }

        questionService.getForm(function (html) {
            ModalController.toggleModalCommonCallback(html);
            $modalContainer.addClass("new-question-form-loaded");
        });
    }

    var showUserProfileDropDown = function () {
        event.stopPropagation();
        $(".user-profile-dropdown").show();
    }

    var search = function (e) {
        var input = $(".search").val();
        if (input === "" || e.keyCode === 27) {
            $(".search-result-container").html("");
            return;
        }

        searchService.search(input, function (html) {
            if (html) {
                $(".search-result-container").html(html);
            }
        });
    }

    var closeSearchResult = function (e) {
        if ($(e.target).parents(".search-container").length > 0) {
            return;
        }

        $(".search-result-container").html("");
    }

    var pageClickHandler = function () {
        $(".user-profile-dropdown").hide();
        closeSearchResult(event);
    }

    var init = function () {
        $(".add-question-button").on("click", toggleModalAddQuestion);
        $(".user-profile-inner").on("click", showUserProfileDropDown);
        $(".search").on("keyup", search);
        $(document).on("click", pageClickHandler);
    };

    return {
        init: init
    }
})(QuestionService, SearchService);


var LoadMoreController = (function (loadMoreService) {
    var controllerName;
    var button;
    var $questionList;
    var currentPage;
    var queryString;
    var successCallBack;

    var loadMoreSuccessHandler = function (html) {
        if (html) {
            $questionList.append(html);
            button.show();
            button.next().hide();
            currentPage++;
        } else {
            button.parent().addClass("end-of-list");
        }

        if (successCallBack)
            successCallBack();
    }

    var loadMoreHandler = function () {
        button = $(this);
        button.hide();
        button.next().show();

        loadMoreService.loadMore(controllerName, currentPage, loadMoreSuccessHandler, queryString);
    }

    var init = function (controller, query, success) {
        controllerName = controller;
        $questionList = $(".load-more-list");
        currentPage = 0;
        queryString = query;
        successCallBack = success;

        $(".to-load").on("click", loadMoreHandler);
    };

    return {
        init: init
    }
})(LoadMoreService);


var ModalController = (function () {
    var toggleModalClose = function () {
        if (event.target !== event.currentTarget) {
            // click on the Modal form
            return;
        }

        var $modalContainer = $(".modal-container");

        if ($modalContainer.hasClass("open")) {
            $modalContainer.removeClass("open");
            $(document.body).removeClass("modal-open");
            return;
        }
    };

    var open = function () {
        $(".modal-container").addClass("open");
        $(document.body).addClass("modal-open");
    }

    var close = function () {
        $(".modal-container").removeClass("open");
        $(document.body).removeClass("modal-open");
    }

    var toggleModalCommonCallback = function (html) {
        if (html) {
            var $modalContainer = $(".modal-container");
            $modalContainer.html(html);
            open();
            $(".topic-select").chosen({ width: "100%", max_selected_options: 5 });
        }
    }
    
    var init = function () {
        $(".modal-container").on("click", toggleModalClose);
        $(".modal-container").on("click", ".js-button-delete", appController.deleteEntity);
        $(".modal-container").on("input", "textarea", appController.textareaAutoGrow);
        $(".modal-container").on("click", ".error li", WarningErrorController.hideError);
    };

    return {
        init: init,
        toggleModalCommonCallback: toggleModalCommonCallback,
        open: open,
        close: close
    }
})();


var PhotoUploadController = (function (questionService, photoUploadService) {
    var targetSelector;

    var toggleModalEditProfilePhoto = function (event) {
        var canvas, ctx, image, startX, startY, endX, endY, imageSize = 160, scale;
        var userId = targetSelector.attr("data-user-id");
        $(".modal-container").removeClass("new-question-form-loaded");
        var eventState = {};

        var initImageParams = function () {
            startX = (canvas.width - imageSize) / 2;
            startY = (canvas.height - imageSize) / 2;
            endX = imageSize + startX;
            endY = imageSize + startY;
            if (!eventState.currentX) eventState.currentX = startX;
            if (!eventState.currentY) eventState.currentY = startY;
            if (!eventState.currentWidth) eventState.currentWidth = imageSize;
            if (!eventState.currentHeight) eventState.currentHeight = imageSize;
            scale = $('.photo-resizer input').val();
        }

        var drawBackground = function () {
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            ctx.fillStyle = "#f6f6f6";
            ctx.fillRect(0, 0, canvas.width, canvas.height);
        }

        var drawImage = function (offsetX, offsetY) {
            var newWidth = image.width * scale;
            var newHeight = image.height * scale;
            var newX = eventState.currentX - (newWidth - eventState.currentWidth) / 2 + (offsetX ? offsetX : 0);
            if (newX > startX) newX = startX;
            if (newX < startX - (newWidth - imageSize)) newX = startX - (newWidth - imageSize);
            var newY = eventState.currentY - (newHeight - eventState.currentHeight) / 2 + (offsetY ? offsetY : 0);
            if (newY > startY) newY = startX;
            if (newY < startY - (newHeight - imageSize)) newY = startY - (newHeight - imageSize);

            ctx.drawImage(image, newX, newY, newWidth, newHeight);
            eventState.currentX = newX;
            eventState.currentY = newY;
            eventState.currentWidth = newWidth;
            eventState.currentHeight = newHeight;
        }

        var drawImageViewSurrounding = function () {
            ctx.globalAlpha = 0.5;
            ctx.fillRect(startX, 0, imageSize, startY);
            ctx.fillRect(0, 0, startX, canvas.height);
            ctx.fillRect(startX, endY, imageSize, canvas.height - endY);
            ctx.fillRect(endX, 0, canvas.width - endX, canvas.height);
            ctx.globalAlpha = 1.0;
        }

        var redrawImage = function (offsetX, offsetY) {
            initImageParams();
            drawBackground();
            drawImage(offsetX, offsetY);
            drawImageViewSurrounding();
        }

        var moving = function (e) {
            var mouse = {}, touches;
            e.preventDefault();
            e.stopPropagation();

            touches = e.originalEvent.touches;

            mouse.x = (e.clientX || e.pageX || touches[0].clientX) + $(window).scrollLeft();
            mouse.y = (e.clientY || e.pageY || touches[0].clientY) + $(window).scrollTop();
            redrawImage(mouse.x - eventState.originalX, mouse.y - eventState.originalY);
            eventState.originalX = mouse.x;
            eventState.originalY = mouse.y;
        };

        var startMoving = function (e) {
            e.preventDefault();
            e.stopPropagation();
            eventState.originalX = (e.clientX || e.pageX || touches[0].clientX) + $(window).scrollLeft();
            eventState.originalY = (e.clientY || e.pageY || touches[0].clientY) + $(window).scrollTop();
            $(document).on('mousemove touchmove', moving);
            $(document).on('mouseup touchend', endMoving);
        };

        var endMoving = function (e) {
            e.preventDefault();
            $(document).off('mouseup touchend', endMoving);
            $(document).off('mousemove touchmove', moving);
        };

        var fileReaderOnload = function (e) {
            $('#photo-canvas').off('mousedown touchstart', startMoving);
            canvas = $('#photo-canvas')[0];
            ctx = canvas.getContext("2d");

            image = new Image();
            image.src = e.currentTarget.result;
            image.onload = function () { redrawImage() };

            var range = $('.photo-resizer input');
            range.on('input change', function () {
                redrawImage();
            });
            range.val(1);

            $('#photo-canvas').on('mousedown touchstart', startMoving);
        };

        var savePhotoHandler = function () {
            var cropCanvas = document.createElement('canvas');
            cropCanvas.width = imageSize;
            cropCanvas.height = imageSize;

            cropCanvas.getContext('2d').drawImage(canvas, startX, startY, imageSize, imageSize,
                0, 0, imageSize, imageSize);
            var dataURL = cropCanvas.toDataURL("image/png");
            photoUploadService.savePhoto(userId, dataURL, function () {
                targetSelector.attr("src", dataURL);
                $('.profile-photo').attr("src", dataURL);
                ModalController.close();
            });
        }

        var success = function (html) {
            if (event.currentTarget.files && event.currentTarget.files[0]) {
                ModalController.toggleModalCommonCallback(html);

                var reader = new FileReader();
                reader.onload = fileReaderOnload;
                reader.readAsDataURL(event.currentTarget.files[0]);

                $('.save-photo').click(savePhotoHandler);
            }
        }

        photoUploadService.getForm(success, userId);
    }

    var clearUpload = function () {
        $(this).val('');
    }

    var init = function (upload, target) {
        targetSelector = $(target);
        $(upload).on("change", toggleModalEditProfilePhoto);
        $(upload).on("click", clearUpload);
    };

    return {
        init: init
    }
})(QuestionService, PhotoUploadService);


var QuestionController = (function (questionService) {
    var $lastDisplayedAnswer;

    var submitAnswer = function () {
        var $editor = $(".ql-editor");
        if ($editor.text().trim().length === 0 && $editor.find('img').length === 0) {
            WarningErrorController.showWarning("Content can not be empty.");
            return false;
        }
        if ($(".ql-editor").html())
            $(this).get(0).elements["AnswerPanelContent"].value = $(".ql-editor").html();
        return true;
    };

    var getEditor = function () {
        return new Quill(".rich-editor-inner", {
            placeholder: "Write your answer here...",
            modules: {
                toolbar: [['bold', 'italic'],
                [{ 'header': 2 }, 'blockquote'],
                [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                ['link', 'image'],
                ['clean']]
            },
            theme: "snow",
            formats: [
                'bold',
                'italic',
                'link',
                'header',
                'list',
                'blockquote',
                'image',
                'indent'
            ],
            clipboard: {
                matchVisual: false
            },
            bounds: ".rich-editor"
        });
    }

    var preventOuterScrolling = function (e) {
        if (e.currentTarget.scrollHeight === e.currentTarget.offsetHeight) {
            return;
        }

        var delta = 0;
        if (e.originalEvent.wheelDelta) { // will work in most cases
            delta = e.originalEvent.wheelDelta;
        } else if (e.originalEvent.detail) { // fallback for Firefox
            delta = -e.originalEvent.detail;
        }
        var scrollTop = $(e.currentTarget).scrollTop();
        if (delta < 0 && scrollTop > e.currentTarget.scrollHeight - e.currentTarget.offsetHeight - 5 ||
            delta > 0 && scrollTop < 5) {
            e.preventDefault();
        }
    }

    var setupClickHandlers = function () {
        $(".ql-editor")
            .on("mousewheel DOMMouseScroll",
                function (e) {
                    preventOuterScrolling(e);
                });

        $(".full-screen")
            .on("click",
                function (e) {
                    $("body").toggleClass("modal-open editor-full-screen");
                });
    };

    var scrollToAnswerPanel = function () {
        $("html, body").scrollTop($(".main-content-container").offset().top - 100);
        $(".ql-editor").get(0).focus();
    };

    var showAddAnswerPanel = function (edit) {
        if ($(".add-answer-panel").hasClass("hide")) {
            // if not already opened
            if (iknow.isUserAuthorized !== true) {
                WarningErrorController.showWarning("Please log in before you write question");
                return;
            }

            if (edit === true) {
                // copy content
                var content = $(event.currentTarget).siblings(".answer-panel-content-inner").html();
                $(".rich-editor-inner").html(content);
            }

            getEditor();
            setupClickHandlers();

            $(".add-answer-panel.hide").slideDown(100).removeClass("hide");
        }

        scrollToAnswerPanel();
    }

    var toggleModalEditQuestion = function () {
        var questionId = $(event.currentTarget).attr("data-question-id");
        $(".modal-container").removeClass("new-question-form-loaded");

        var success = function (html) {
            ModalController.toggleModalCommonCallback(html);
            var $textArea = $(".add-question-description textarea");
            var scrollHeight = $(".add-question-description textarea").get(0).scrollHeight;
            $textArea.css('height', scrollHeight + 'px');
        }

        questionService.getForm(success, questionId);
    }

    var toggleModalEditTopic = function () {
        var questionId = $(event.currentTarget).attr("data-question-id");
        $(".modal-container").removeClass("new-question-form-loaded");

        questionService.getTopic(ModalController.toggleModalCommonCallback, questionId);
    }

    var hideLoadMoreWhenAllAnswersDisplayed = function () {
        var $list = $(".load-more-list");
        var answerCount = $list.attr("data-answer-count");
        if ($list.find(".answer-panel").length === answerCount) {
            $(".load-more").addClass("end-of-list");
        }
    }

    var toggleCollapse = function () {
        event.stopPropagation();
        var $answerContainer = $(this).closest(".answer-panel-content-container");
        var diff;
        if (!$answerContainer.hasClass("is-collapsed")) {
            diff = $answerContainer.height() - $(window).scrollTop();
        }

        $answerContainer.toggleClass("is-collapsed");

        if (diff) {
            $(window).scrollTop($answerContainer.height() - diff);
        }
    }

    var hideOrShowCollapseAnswerForShortAnswerInner = function ($selector) {
        var $answers = $selector.closest(".answer-panel-content-container");

        if ($answers.outerHeight() < 800) {
            $answers.find(".collapse-answer").addClass("hide");
        } else {
            $answers.find(".collapse-answer").removeClass("hide");
        }
    }

    var hideCollapseAnswerForShortAnswer = function ($answer) {
        var $images = $answer.find("img");
        hideOrShowCollapseAnswerForShortAnswerInner($answer);
        if ($images.length > 0) {
            $images.on("load", function () {
                hideOrShowCollapseAnswerForShortAnswerInner($answer);
            });
        }
    }

    var hideCollapseAnswerForShortAnswers = function () {
        var $answers;
        if ($lastDisplayedAnswer)
            $answers = $lastDisplayedAnswer.nextAll().find(".answer-panel-content");
        else
            $answers = $(".whole-panel, .current-answer-panel").find(".answer-panel-content");

        $.each($answers, function (index) {
            hideCollapseAnswerForShortAnswer($(this));

            if (index === $answers.length - 1)
                $lastDisplayedAnswer = $(this).closest(".answer-panel");
        });
    }

    var loadAnswerCallBack = function () {
        hideLoadMoreWhenAllAnswersDisplayed();
        hideCollapseAnswerForShortAnswers();
    }

    var collapseAnswersInMoreAnswersPanel = function () {
        $(".more-answer-panel .answer-panel-content-container").addClass("is-collapsed");
    }

    var init = function () {
        $(".add-answer-panel").on("submit", "form", submitAnswer);
        $(".add-answer-panel").on("click", ".js-button-delete", appController.deleteEntity);
        $(".write-answer").on("click", showAddAnswerPanel);
        $(".edit-answer").on("click", function () { showAddAnswerPanel(true); });
        $(".question-header-panel .js-edit-question").on("click", toggleModalEditQuestion);
        $(".question-header-panel .js-edit-topic").on("click", toggleModalEditTopic);
        $(".whole-panel, .answer-panel-container").on("click", ".collapse-answer", toggleCollapse);
        $(".whole-panel, .answer-panel-container").on("click", ".answer-panel-content-container.is-collapsed", toggleCollapse);

        AnswerFooterController.init();
        loadAnswerCallBack();
    };

    return {
        init: init,
        loadAnswerCallBack: loadAnswerCallBack,
        collapseAnswersInMoreAnswersPanel: collapseAnswersInMoreAnswersPanel
    }
})(QuestionService);


var RegisterController = (function (registerService) {
    var toggleModalRegister = function () {
        if ($(".modal-container").hasClass("register-form-loaded")) {
            ModalController.open();
            return;
        }

        var queryString = $(event.currentTarget).attr("data-return-url") ? "?returnUrl=" + $(event.currentTarget).attr("data-return-url") : "";
        registerService.getRegisterForm(
            function (html) {
                ModalController.toggleModalCommonCallback(html);
                $(".modal-container").addClass("register-form-loaded");
            },
            queryString);
    };

    var init = function () {
        $(".register-container a").on("click", toggleModalRegister);
    };

    return {
        init: init
    }
})(RegisterService);


var TopicController = (function (topicService) {
    var $button;

    var selectTopic = function () {
        var $item = $(this);
        if ($item.hasClass("active")) {
            return;
        }

        var topicId = $item.attr("data-topic-id");
        topicService.selectTopic(topicId,
            function (html) {
                if (html) {
                    $item.siblings().removeClass("active");
                    $item.addClass("active");
                    $(".topic-body").html(html);
                }
            });
    };

    var toggleFollow = function () {
        $button = $(this);
        var topicId = $button.attr("data-topic-id");

        if ($button.hasClass("btn-grey")) {
            topicService.unfollow(topicId, done, fail);
        } else {
            topicService.follow(topicId,done, fail);
        }
    };

    var done = function () {
        var text = $button.hasClass("btn-grey") ? "Follow" : "Following";
        $button.toggleClass("btn-grey").text(text);
    };

    var fail = function () {
        var func = $button.hasClass("btn-grey") ? "unfollow" : "follow";
        console.error("Something went wrong with TopicService " + func + ".");
    };

    var init = function () {
        $(".topic-list").on("click", "li", selectTopic);
        $(".js-topic-toggle").on("click", toggleFollow);
    };

    return {
        init: init
    }
})(TopicService);


var TopicPhotoUploadController = (function () {
    var targetSelector;

    var readUrl = function () {
        if (event.currentTarget.files && event.currentTarget.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                targetSelector.attr("src", e.currentTarget.result);
            };
            reader.readAsDataURL(event.currentTarget.files[0]);
        }
    }

    var init = function (upload, target) {
        targetSelector = $(target);
        $(upload).on("change", readUrl);
    };

    return {
        init: init
    }
})();

var WarningErrorController = (function () {
   var hideError = function () {
        $(this).hide();
    }

    var cleanUpErrorAndWarning = function () {
        $(".warning").removeClass("warning-display severe-warning");
        $(".error").removeClass("validation-summary-errors");
    }

    var showWarning = function (message, isSevere) {
        var $warning = $(".warning");
        cleanUpErrorAndWarning();
        $warning.html("");
        $("<div>").html(message).appendTo($warning);
        if (isSevere) {
            $warning.addClass("severe-warning");
        }
        setTimeout(function () {
            $warning.addClass("warning-display");
        }, 50);
    }

    var checkWarningAndErrorOnInit = function () {
        if (iknow.pageError) {
            showWarning(iknow.pageError, true);
        }

        if (iknow.statusMessage) {
            showWarning(iknow.statusMessage);
        }
    };

    var init = function () {
        $(".error").on("click", "li", hideError);
        $(".warning").on("click", "div", hideError);
        checkWarningAndErrorOnInit();
    };

    return {
        init: init,
        hideError: hideError,
        showWarning: showWarning,
        cleanUpErrorAndWarning: cleanUpErrorAndWarning
    }
})();


var appController = (function() {
    var deleteEntity = function(e) {
        var button = $(this);
        var message = button.attr("data-delete-message");
        return confirm(message);
    }
    
    var backToTopDetect= function () {
        if ($(this).scrollTop() > 500) {
            $(".back-top-container").fadeIn();
        } else {
            $(".back-top-container").fadeOut();
        }
    }

    var goToTop = function () {
        $("html, body")
            .animate({
                scrollTop: 0
            },
                300);
    }

    var textareaAutoGrow = function () {
        var scrollHeight = this.scrollHeight;
        $(this).css('height', scrollHeight + 'px');
    };

    var submitSearch = function() {
        var val = $(".search-form .search").val();
        if (!val)
            return false;
        return true;
    }

    var init = function () {
        ModalController.init();
        HeaderController.init();
        WarningErrorController.init();

        $(window).scroll(backToTopDetect);
        $(".back-top").on("click", goToTop);
        $(".search-form").on("submit", submitSearch);
    };

    return {
        init: init, 
        deleteEntity: deleteEntity, 
        textareaAutoGrow: textareaAutoGrow
    }
})();
