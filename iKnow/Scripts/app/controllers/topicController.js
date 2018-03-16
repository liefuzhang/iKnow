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

    var followTopic = function() {
        var $button = $(this);
        var topicId = $button.attr("data-topic-id");
        topicService.follow(topicId,
            function() {
                $button.addClass("btn-grey");
            },
            function() {
                console.error("Something went wrong with TopicService followTopic.");
            });
    };

    var init = function () {
        $(".topic-list").on("click", "li", selectTopic);
        $(".js-button-follow").on("click", followTopic);
    };

    return {
        init: init
    }
})(TopicService);

