var loadMoreController = (function () {
    var controllerName;

    var loadMoreHandler = function () {
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

    var init = function (controller) {
        controllerName = controller;
        $(".to-load").on("click", loadMoreHandler);
    };

    return {
        init: init
    }
})();

