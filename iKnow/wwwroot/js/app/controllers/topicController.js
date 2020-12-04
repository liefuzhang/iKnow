var TopicController = (function (topicService) {
    var $button;

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

    var toggleFollow = function () {
        $button = $(this);
        var topicId = $button.attr("data-topic-id");

        if ($button.hasClass("btn-grey")) {
            topicService.unfollow(topicId, done, fail);
        } else {
            topicService.follow(topicId,done, fail);
        }
    };

    var done = function () {
        var text = $button.hasClass("btn-grey") ? "Follow" : "Following";
        $button.toggleClass("btn-grey").text(text);
    };

    var fail = function () {
        var func = $button.hasClass("btn-grey") ? "unfollow" : "follow";
        console.error("Something went wrong with TopicService " + func + ".");
    };

    var init = function () {
        $(".topic-list").on("click", "li", selectTopic);
        $(".js-topic-toggle").on("click", toggleFollow);
    };

    return {
        init: init
    }
})(TopicService);

