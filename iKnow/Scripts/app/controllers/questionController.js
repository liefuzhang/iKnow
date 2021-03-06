﻿var QuestionController = (function (questionService) {
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

