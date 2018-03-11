var TopicService = (function () {
    var selectTopic = function(topicId, success) {
        $.ajax({
            url: "/topic/about/" + topicId,
            dataType: "html",
            success: success,
            fail: function () {
                console.error("Something went wrong with TopicService selectTopic.");
            }
        });
    };

    return {
        selectTopic: selectTopic
    }
})();