var LoadMoreService = (function () {
    var loadMore = function (controllerName, currentPage, success, queryString) {
        $.ajax({
            url: "/" + controllerName + "/loadmore/" + currentPage + (queryString ? queryString : ""),
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