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
        var container = $(".question-answer-container, .activity-panel");
        container.on("mouseenter", ".short-answer-container", toggleMoreAnswerUnderline);
        container.on("mouseleave", ".short-answer-container", toggleMoreAnswerUnderline);
        container.on("click", ".short-answer-container", showMoreAnswer);
        container.on("click", ".collapse-answer", hideMoreAnswer);

        AnswerFooterController.init();
    };

    return {
        init: init
    }
})();

