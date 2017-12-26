$(document).ready(function () {
    $(".topic-list").on("click", "li", selectTopic);
});

function selectTopic(e) {
    var topicId = $(this).attr("data-topic-id");

}