$(document).ready(function () {
    $(".topic-list").on("click", "li", selectTopic);
    $(".add-topic-container .js-button-delete").on("click", deleteTopic);
    $(".js-button-add-question").on("click", toggleAddQuestionModal);
    $(".question-modal-container").on("click", toggleAddQuestionModal);
    $(".topic-select").chosen({width: "100%", max_selected_options: 5});
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
    $(".question-modal-container").toggleClass("open");
}