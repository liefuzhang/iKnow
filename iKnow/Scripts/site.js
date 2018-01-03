$(document).ready(function () {
    $(".topic-list").on("click", "li", selectTopic);
    $(".topic-form-container .js-button-delete").on("click", deleteTopic);
    $(".js-button-add-question").on("click", toggleAddQuestionModal);
    $(".question-modal-container").on("click", toggleAddQuestionModal);
    $(".question-header-panel .js-edit-topic").on("click", editTopic);
    $(".question-header-panel .js-edit-question").on("click", editQuestion);
    $(".write-answer").on("click", showAddAnswerPanel);
    $(".question-answer-container").on("mouseenter", ".short-answer-container", toggleMoreAnswerUnderline);
    $(".question-answer-container").on("mouseleave", ".short-answer-container", toggleMoreAnswerUnderline);
    $(".question-answer-container").on("click", ".short-answer-container", showMoreAnswer);
    $(".question-answer-container").on("click", ".collapse-answer", hideMoreAnswer);
    $(".question-modal-container").on("input", "textarea", textareaAutoGrow);
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

function toggleAddQuestionModal(e) {
    if (e.target != e.currentTarget) {
        // click on the Modal form
        return;
    }
    var $modalContainer = $(".question-modal-container");
    var $body = $(document.body);

    if ($modalContainer.hasClass("open")) {
        $modalContainer.removeClass("open");
        $body.removeClass("modal-open");
        return;
    }

    if ($modalContainer.hasClass("new-form-loaded")) {
        $modalContainer.addClass("open");
        $body.addClass("modal-open");
        return;
    }

    $.ajax({
        url: "/question/getform",
        dataType: "html",
        success: function (html) {
            if (html) {
                $modalContainer.html(html);
                $modalContainer.addClass("new-form-loaded open");
                $body.addClass("modal-open");
                $(".topic-select").chosen({ width: "100%", max_selected_options: 5 });
            }
        }
    });
}

function editQuestion() {
    var questionId = $(this).attr("data-question-id");
    var $modalContainer = $(".question-modal-container");
    $modalContainer.removeClass("new-form-loaded");

    $.ajax({
        url: "/question/getform/" + questionId,
        dataType: "html",
        success: function (html) {
            if (html) {
                $modalContainer.html(html);
                $modalContainer.addClass("open");
                $(document.body).addClass("modal-open");
                $(".topic-select").chosen({ width: "100%", max_selected_options: 5 });
            }
        }
    });
}

function editTopic() {
    var questionId = $(this).attr("data-question-id");
    var $modalContainer = $(".question-modal-container");
    $modalContainer.removeClass("new-form-loaded");

    $.ajax({
        url: "/question/gettopic/" + questionId,
        dataType: "html",
        success: function (html) {
            if (html) {
                $modalContainer.html(html);
                $modalContainer.addClass("open");
                $(".topic-select").chosen({ width: "100%", max_selected_options: 5 });
            }
        }
    });
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
    var scroll_height = $(this).get(0).scrollHeight;
    $(this).css('height', scroll_height + 'px');
}