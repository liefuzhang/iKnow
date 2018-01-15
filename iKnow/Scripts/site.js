$(document).ready(function () {
    $(".topic-list").on("click", "li", selectTopic);
    $(".topic-form-container .js-button-delete").on("click", deleteEntity);
    $(".modal-container").on("click", ".js-button-delete", deleteEntity);
    $(".add-answer-panel").on("click", ".js-button-delete", deleteEntity);
    $(".add-answer-panel").on("submit", "form", submitAnswer);
    $(".add-question-button").on("click", (e) => toggleModal(e, "addQuestion"));
    $(".modal-container").on("click", (e) => toggleModal(e, "close"));
    $(".question-header-panel .js-edit-question").on("click", (e) => toggleModal(e, "editQuestion"));
    $(".question-header-panel .js-edit-topic").on("click", (e) => toggleModal(e, "editTopic"));
    $(".register-container a").on("click", (e) => toggleModal(e, "register"));
    $(".write-answer").on("click", showAddAnswerPanel);
    $(".edit-answer").on("click", () => { showAddAnswerPanel(true); });
    $(".question-answer-container").on("mouseenter", ".short-answer-container", toggleMoreAnswerUnderline);
    $(".question-answer-container").on("mouseleave", ".short-answer-container", toggleMoreAnswerUnderline);
    $(".question-answer-container").on("click", ".short-answer-container", showMoreAnswer);
    $(".question-answer-container").on("click", ".collapse-answer", hideMoreAnswer);
    $(".modal-container").on("input", "textarea", textareaAutoGrow);
    $(".user-profile-inner").on("click", showUserProfileDropDown);
    $(".js-profile-photo-upload").on("change", (e) => readURL(e, ".edit-profile-photo img"));
    $(".js-topic-photo-upload").on("change", (e) => readURL(e, ".topic-form-container .img-container img"));
    $(".mask-content").on("click", () => { $(".js-profile-photo-upload").click(); });
    $(".search").on("keyup", search);
    $(".search-container .btn").on("click", search);
    $(".error").on("click", "li", hideError);
    $(".modal-container").on("click", ".error li", hideError);
    $(".warning").on("click", "div", hideError);
    $(document).on("click", pageClickHandler);
});

function selectTopic() {
    var $this = $(this);
    if ($this.hasClass("active")) {
        return;
    }

    var topicId = $this.attr("data-topic-id");
    $.ajax({
        url: "/topic/about/" + topicId,
        dataType: "html",
        success: function (html) {
            if (html) {
                $this.siblings().removeClass("active");
                $this.addClass("active");
                $(".topic-body").html(html);
            }
        }
    });
}

function deleteEntity(e) {
    var button = $(this);
    var message = button.attr("data-delete-message");
    return confirm(message);
}

function toggleModal(e, action) {
    var $modalContainer = $(".modal-container");
    var $body = $(document.body);
    var $target = $(e.currentTarget);
    var commonCallback = function (html) {
        if (html) {
            $modalContainer.html(html);
            $modalContainer.addClass("open");
            $body.addClass("modal-open");
            $(".topic-select").chosen({ width: "100%", max_selected_options: 5 });
        }
    };

    switch (action) {
        case "addQuestion":
            if (iknow.isUserAuthorized !== true) {
                showWarning("Please log in before you add question");
                return;
            }
            if ($modalContainer.hasClass("new-form-loaded")) {
                // already loaded
                $modalContainer.addClass("open");
                $body.addClass("modal-open");
                return;
            }

            $.ajax({
                url: "/question/getform",
                dataType: "html",
                success: (html) => {
                    commonCallback(html);
                    $modalContainer.addClass("new-form-loaded");
                }
            });
            break;
        case "editQuestion":
            var questionId = $target.attr("data-question-id");
            $modalContainer.removeClass("new-form-loaded");

            $.ajax({
                url: "/question/getform/" + questionId,
                dataType: "html",
                success: (html) => {
                    commonCallback(html);
                    var $textArea = $(".add-question-description textarea");
                    var scrollHeight = $(".add-question-description textarea").get(0).scrollHeight;
                    $textArea.css('height', scrollHeight + 'px');
                }
            });
            break;
        case "editTopic":
            questionId = $target.attr("data-question-id");
            $modalContainer.removeClass("new-form-loaded");

            $.ajax({
                url: "/question/gettopic/" + questionId,
                dataType: "html",
                success: commonCallback
            });
            break;
        case "register":
            $modalContainer.removeClass("new-form-loaded");
            var queryString = $target.attr("data-return-url") ? "?returnUrl=" + $target.attr("data-return-url") : "";
            $.ajax({
                url: "/account/register" + queryString,
                dataType: "html",
                success: commonCallback
            });
            break;
        case "close":
        default:
            if (e.target !== e.currentTarget) {
                // click on the Modal form
                return;
            }

            if ($modalContainer.hasClass("open")) {
                $modalContainer.removeClass("open");
                $body.removeClass("modal-open");
                return;
            }
    }
}

function showAddAnswerPanel(edit) {
    if (!$(".add-answer-panel").hasClass("hide")) {
        // if already opened, scroll to it and return
        $('html, body').scrollTop($(".add-answer-panel").offset().top - 100);
        return;
    }

    if (iknow.isUserAuthorized !== true) {
        showWarning("Please log in before you write question");
        return;
    }

    $(".add-answer-panel.hide").slideDown(100).removeClass("hide");
    $('html, body').scrollTop($(".add-answer-panel").offset().top - 100);

    if (edit === true) {
        // copy content
        var content = $(event.currentTarget).siblings(".answer-panel-content-inner").html();
        $(".rich-editor-inner").html(content);
    }

    var quill = getEditoer();

    $(".ql-editor")
        .on("mousewheel DOMMouseScroll", function (e) {
            preventOuterScrolling(e);
        });

    $(".full-screen")
        .on("click", function (e) {
            $("body").toggleClass("modal-open editor-full-screen");
        });

}

function getEditoer() {
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

function preventOuterScrolling(e) {
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
    if (delta < 0 && scrollTop > e.currentTarget.scrollHeight - e.currentTarget.offsetHeight - 5
        || delta > 0 && scrollTop < 5) {
        e.preventDefault();
    }
}

function toggleMoreAnswerUnderline(e) {
    $(e.currentTarget).find(".blue-link-color").toggleClass("underline");
}

function showMoreAnswer(e) {
    var $this = $(e.currentTarget);
    $this.addClass("hide");
    $this.next().removeClass("hide");
}

function hideMoreAnswer(e) {
    var $this = $(e.currentTarget);
    var $container = $this.closest(".expanded-answer-container");
    $container.addClass("hide");
    $container.prev().removeClass("hide");
}

function loadMoreHandler(controllerName) {
    var $this = $(this);
    $this.hide();
    $this.next().show();

    var $questionList = $(".load-more-list");
    var currentPage = $questionList.attr("data-current-page");
    $.ajax({
        url: "/" + controllerName + "/loadmore/" + currentPage,
        dataType: "html",
        success: function (html) {
            if (html) {
                $questionList.append(html);
                $this.show();
                $this.next().hide();
                $questionList.attr("data-current-page", currentPage + 1);
            } else {
                $this.parent().addClass("end-of-list");
            }
        }
    });
}

function textareaAutoGrow() {
    var scrollHeight = this.scrollHeight;
    $(this).css('height', scrollHeight + 'px');
}

function showUserProfileDropDown() {
    event.stopPropagation();
    $(".user-profile-dropdown").show();
}

function pageClickHandler() {
    $(".user-profile-dropdown").hide();
    closeSearchResult(event);
}

function readURL(event, target) {
    if (event.currentTarget.files && event.currentTarget.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $(target).attr("src", e.currentTarget.result);
        };
        reader.readAsDataURL(event.currentTarget.files[0]);
    }
}

function search(e) {
    var input = $(".search").val();
    if (input === "" ||
        ($(e.currentTarget).hasClass("search") && e.keyCode === 27)) {
        $(".search-result-container").html("");
        return;
    }

    if ($(e.currentTarget).hasClass("search") && e.keyCode === 13 ||
        $(e.currentTarget).hasClass("btn")) {
        $.ajax({
            url: "/search/getresult?input=" + input,
            dataType: "html",
            success: function (html) {
                if (html) {
                    $(".search-result-container").html(html);
                }
            }
        });
    }
}

function closeSearchResult(e) {
    if ($(e.target).parents(".search-container").length > 0) {
        return;
    }

    $(".search-result-container").html("");
}

function hideError() {
    $(this).hide();
}

function showWarning(message) {
    cleanUpErrorAndWarning();
    $(".warning").html("");
    $("<div>").html(message).appendTo($(".warning"));
    setTimeout(() => { $(".warning").addClass("warning-display"); }, 50);
}

function cleanUpErrorAndWarning() {
    $(".warning").removeClass("warning-display");
    $(".error").removeClass("validation-summary-errors");
}

function submitAnswer() {
    var $editor = $(".ql-editor");
    if ($editor.text().trim().length === 0
        && $editor.find('img').length === 0) {
        showWarning("Content can not be empty.");
        return false;
    }
    if ($(".ql-editor").html())
        $(this).get(0).elements["AnswerPanelContent"].value = $(".ql-editor").html();
}