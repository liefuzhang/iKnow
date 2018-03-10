var LoadMoreService = (function () {
    var loadMore = function(controllerName, currentPage, success) {
        $.ajax({
            url: "/" + controllerName + "/loadmore/" + currentPage,
            dataType: "html",
            success: success,
            fail: function () {
                console.error("Something went wrong with LoadMoreService loadMore.");
            }
        });
    };

    return {
        loadMore: loadMore
    }
})();