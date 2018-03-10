var TopicController = (function () {
    var selectTopic = function () {
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
    };

    var init = function () {
        $(".topic-list").on("click", "li", selectTopic);
    };

    return {
        init: init
    }
})();

