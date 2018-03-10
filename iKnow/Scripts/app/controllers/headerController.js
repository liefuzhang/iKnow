var HeaderController = (function (questionService, searchService) {
    var toggleModalAddQuestion = function () {
        var $modalContainer = $(".modal-container");
        if (iknow.isUserAuthorized !== true) {
            WarningErrorController.showWarning("Please log in before you add question");
            return;
        }
        if ($modalContainer.hasClass("new-question-form-loaded")) {
            // already loaded
            $modalContainer.addClass("open");
            $(document.body).addClass("modal-open");
            return;
        }

        questionService.getForm(function (html) {
            ModalController.toggleModalCommonCallback(html);
            $modalContainer.addClass("new-question-form-loaded");
        });
    }

    var showUserProfileDropDown = function () {
        event.stopPropagation();
        $(".user-profile-dropdown").show();
    }

    var search = function (e) {
        var input = $(".search").val();
        if (input === "" || ($(e.currentTarget).hasClass("search") && e.keyCode === 27)) {
            $(".search-result-container").html("");
            return;
        }

        if ($(e.currentTarget).hasClass("search") && e.keyCode === 13 ||
            $(e.currentTarget).hasClass("btn")) {
            searchService.search(input,
                function (html) {
                    if (html) {
                        $(".search-result-container").html(html);
                    }
                });
        }
    }

    var closeSearchResult = function (e) {
        if ($(e.target).parents(".search-container").length > 0) {
            return;
        }

        $(".search-result-container").html("");
    }

    var pageClickHandler = function () {
        $(".user-profile-dropdown").hide();
        closeSearchResult(event);
    }

    var init = function () {
        $(".add-question-button").on("click", toggleModalAddQuestion);
        $(".user-profile-inner").on("click", showUserProfileDropDown);
        $(".search").on("keyup", search);
        $(".search-container .btn").on("click", search);
        $(document).on("click", pageClickHandler);
    };

    return {
        init: init
    }
})(QuestionService, SearchService);

