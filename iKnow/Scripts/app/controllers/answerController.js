﻿var answerController = (function () {
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
        var expandedAnswerBottom = $outerContainer.height() - $window.scrollTop();

        $container.addClass("hide");
        $container.prev().removeClass("hide");

        if ($window.scrollTop() > $author.offset().top) {
            var scrollTo = $outerContainer.height() - expandedAnswerBottom;
            $window.scrollTop(scrollTo);
        }
    }

    var init = function () {
        $(".question-answer-container").on("mouseenter", ".short-answer-container", toggleMoreAnswerUnderline);
        $(".question-answer-container").on("mouseleave", ".short-answer-container", toggleMoreAnswerUnderline);
        $(".question-answer-container").on("click", ".short-answer-container", showMoreAnswer);
        $(".question-answer-container").on("click", ".collapse-answer", hideMoreAnswer);
    };

    return {
        init: init
    }
})();
