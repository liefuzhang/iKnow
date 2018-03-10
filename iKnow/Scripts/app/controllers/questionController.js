var QuestionController = (function () {
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

            var quill = getEditor();

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

            $("html, body")
                .animate({
                    scrollTop: $(".main-content-container").offset().top - 100
                },
                    0,
                    function () {
                        $(".add-answer-panel.hide").slideDown(100).removeClass("hide");
                        $(".ql-editor").get(0).focus();
                    });
        } else {
            $("html, body").scrollTop($(".main-content-container").offset().top - 100);
            $(".ql-editor").get(0).focus();
        }
    };

    var toggleModalEditQuestion = function () {
        var questionId = $(event.currentTarget).attr("data-question-id");
        $(".modal-container").removeClass("new-question-form-loaded");

        $.ajax({
            url: "/question/getform/" + questionId,
            dataType: "html",
            success: function (html) {
                ModalController.toggleModalCommonCallback(html);
                var $textArea = $(".add-question-description textarea");
                var scrollHeight = $(".add-question-description textarea").get(0).scrollHeight;
                $textArea.css('height', scrollHeight + 'px');
            }
        });
    }

    var toggleModalEditTopic = function () {
        var questionId = $(event.currentTarget).attr("data-question-id");
        $(".modal-container").removeClass("new-question-form-loaded");

        $.ajax({
            url: "/question/gettopic/" + questionId,
            dataType: "html",
            success: ModalController.toggleModalCommonCallback
        });
    }

    var init = function () {
        $(".add-answer-panel").on("submit", "form", submitAnswer);
        $(".add-answer-panel").on("click", ".js-button-delete", appController.deleteEntity);
        $(".write-answer").on("click", showAddAnswerPanel);
        $(".edit-answer").on("click", function () { showAddAnswerPanel(true); });
        $(".question-header-panel .js-edit-question").on("click", toggleModalEditQuestion);
        $(".question-header-panel .js-edit-topic").on("click", toggleModalEditTopic);
    };

    return {
        init: init
    }
})();

