var TopicService = (function () {
    var selectTopic = function (topicId, success) {
        $.ajax({
            url: "/topic/about/" + topicId,
            dataType: "html",
            success: success,
            fail: function () {
                console.error("Something went wrong with TopicService selectTopic.");
            }
        });
    };

    var followTopic = function (topicId, done, fail) {
        $.post("/api/topicfollowing", { "": topicId })
            .done(done)
            .fail(fail);
    }

    return {
        selectTopic: selectTopic,
        follow: followTopic
    }
})();