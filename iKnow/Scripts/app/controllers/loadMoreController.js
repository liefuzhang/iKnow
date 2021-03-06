﻿var LoadMoreController = (function (loadMoreService) {
    var controllerName;
    var button;
    var $questionList;
    var currentPage;
    var queryString;
    var successCallBack;

    var loadMoreSuccessHandler = function (html) {
        if (html) {
            $questionList.append(html);
            button.show();
            button.next().hide();
            currentPage++;
        } else {
            button.parent().addClass("end-of-list");
        }

        if (successCallBack)
            successCallBack();
    }

    var loadMoreHandler = function () {
        button = $(this);
        button.hide();
        button.next().show();

        loadMoreService.loadMore(controllerName, currentPage, loadMoreSuccessHandler, queryString);
    }

    var init = function (controller, query, success) {
        controllerName = controller;
        $questionList = $(".load-more-list");
        currentPage = 0;
        queryString = query;
        successCallBack = success;

        $(".to-load").on("click", loadMoreHandler);
    };

    return {
        init: init
    }
})(LoadMoreService);

