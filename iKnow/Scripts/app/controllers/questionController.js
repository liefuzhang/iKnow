var questionController = (function () {
    var submitAnswer = function() {
        var $editor = $(".ql-editor");
        if ($editor.text().trim().length === 0 && $editor.find('img').length === 0) {
            showWarning("Content can not be empty.");
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

    var showAddAnswerPanel = function(edit) {
        if ($(".add-answer-panel").hasClass("hide")) {
            // if not already opened
            if (iknow.isUserAuthorized !== true) {
                showWarning("Please log in before you write question");
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
                    function(e) {
                        preventOuterScrolling(e);
                    });

            $(".full-screen")
                .on("click",
                    function(e) {
                        $("body").toggleClass("modal-open editor-full-screen");
                    });

            $("html, body")
                .animate({
                        scrollTop: $(".main-content-container").offset().top - 100
                    },
                    0,
                    function() {
                        $(".add-answer-panel.hide").slideDown(100).removeClass("hide");
                        $(".ql-editor").get(0).focus();
                    });
        } else {
            $("html, body").scrollTop($(".main-content-container").offset().top - 100);
            $(".ql-editor").get(0).focus();
        }
    };

    var init = function () {
        $(".add-answer-panel").on("submit", "form", submitAnswer);
        $(".write-answer").on("click", showAddAnswerPanel);
        $(".edit-answer").on("click", function () { showAddAnswerPanel(true); });
    };

    return {
        init: init
    }
})();

