var LoadMoreService = (function () {
    var loadMore = function(controllerName, currentPage, success) {
        $.ajax({
            url: "/" + controllerName + "/loadmore/" + currentPage,
            dataType: "html",
            success: success
        });
    };

    return {
        loadMore: loadMore
    }
})();