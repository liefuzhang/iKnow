var loadMoreController = (function () {
    var loadMoreHandler = function (controllerName) {
        var $this = $(this);
        $this.hide();
        $this.next().show();

        var $questionList = $(".load-more-list");
        var currentPage = $questionList.attr("data-current-page");
        $.ajax({
            url: "/" + controllerName + "/loadmore/" + currentPage,
            dataType: "html",
            success: function (html) {
                if (html) {
                    $questionList.append(html);
                    $this.show();
                    $this.next().hide();
                    $questionList.attr("data-current-page", currentPage + 1);
                } else {
                    $this.parent().addClass("end-of-list");
                }
            }
        });
    }

    
    var init = function (controllerName) {
        $(".to-load").on("click", function () { loadMoreHandler.call(this, controllerName); });
    };

    return {
        init: init
    }
})();

