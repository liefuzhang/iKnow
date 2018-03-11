var TopicController = (function (topicService) {
    var selectTopic = function () {
        var $item = $(this);
        if ($item.hasClass("active")) {
            return;
        }

        var topicId = $item.attr("data-topic-id");
        topicService.selectTopic(topicId,
            function (html) {
                if (html) {
                    $item.siblings().removeClass("active");
                    $item.addClass("active");
                    $(".topic-body").html(html);
                }
            });
    };

    var init = function () {
        $(".topic-list").on("click", "li", selectTopic);
    };

    return {
        init: init
    }
})(TopicService);

