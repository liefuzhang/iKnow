$(document).ready(function () {
    $(".topic-list").on("click", "li", selectTopic);
    $(".add-topic-container .js-button-delete").on("click", deleteTopic);
    $(".js-button-add-question").on("click", toggleAddQuestionModal);
    $(".question-modal-container").on("click", toggleAddQuestionModal);
    $(".question-header-panel .js-edit-topic").on("click", editTopic);
    $(".question-header-panel .js-edit-question").on("click", editQuestion);
    $(".write-answer").on("click", showAddAnswerPanel);
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

    if ($modalContainer.hasClass("open")) {
        $modalContainer.removeClass("open");
        return;
    }

    if ($modalContainer.hasClass("new-form-loaded")) {
        $(".question-modal-container").addClass("open");
        return;
    }

    $.ajax({
        url: "/question/getform",
        dataType: "html",
        success: function (html) {
            if (html) {
                $modalContainer.html(html);
                $modalContainer.addClass("new-form-loaded open");
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
                $(".topic-select").chosen({ width: "556px", max_selected_options: 5 });
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
                $(".topic-select").chosen({ width: "556px", max_selected_options: 5 });
            }
        }
    });
}

function showAddAnswerPanel() {
    $(".add-answer-panel.hide").slideDown(100);
    $('html, body').scrollTop($(".add-answer-panel").offset().top - 100);
}