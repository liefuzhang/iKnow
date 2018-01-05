$(document).ready(function () {
    $(".topic-list").on("click", "li", selectTopic);
    $(".topic-form-container .js-button-delete").on("click", deleteTopic);
    $(".js-button-add-question").on("click", (e) => toggleModal(e, "addQuestion"));
    $(".modal-container").on("click", (e) => toggleModal(e, "close"));
    $(".question-header-panel .js-edit-question").on("click", (e) => toggleModal(e, "editQuestion"));
    $(".question-header-panel .js-edit-topic").on("click", (e) => toggleModal(e, "editTopic"));
    $(".register-container a").on("click", (e) => toggleModal(e, "register"));
    $(".write-answer").on("click", showAddAnswerPanel);
    $(".question-answer-container").on("mouseenter", ".short-answer-container", toggleMoreAnswerUnderline);
    $(".question-answer-container").on("mouseleave", ".short-answer-container", toggleMoreAnswerUnderline);
    $(".question-answer-container").on("click", ".short-answer-container", showMoreAnswer);
    $(".question-answer-container").on("click", ".collapse-answer", hideMoreAnswer);
    $(".modal-container").on("input", "textarea", textareaAutoGrow);
    $(".user-profile-inner").on("click", showUserProfileDropDown);
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

function deleteTopic(e) {
    var button = $(this);
    return confirm("Are you sure you want to delete this topic?");
}

function toggleModal(e, action) {
    var $modalContainer = $(".modal-container");
    var $body = $(document.body);
    var $target = $(e.target);
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
                success: commonCallback
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

function showAddAnswerPanel() {
    $(".add-answer-panel.hide").slideDown(100);
    $('html, body').scrollTop($(".add-answer-panel").offset().top - 100);
}

function toggleMoreAnswerUnderline(e) {
    $(e.currentTarget).find(".showFullAnswer").toggleClass("underline");
}

function showMoreAnswer(e) {
    var $this = $(e.currentTarget);
    $this.addClass("hide");
    $this.next().removeClass("hide");
}

function hideMoreAnswer(e) {
    var $this = $(e.currentTarget);
    var $container = $this.closest(".expaneded-answer-container");
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
    var scrollHeight = $(this).get(0).scrollHeight;
    $(this).css('height', scrollHeight + 'px');
}

function showUserProfileDropDown() {
    event.stopPropagation();
    $(".user-profile-dropdown").show();
}

function pageClickHandler() {
    $(".user-profile-dropdown").hide();
}