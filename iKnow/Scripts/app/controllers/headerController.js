var headerController = (function () {
    var toggleModalAddQuestion = function () {
        var $modalContainer = $(".modal-container");
        if (iknow.isUserAuthorized !== true) {
            warningErrorController.showWarning("Please log in before you add question");
            return;
        }
        if ($modalContainer.hasClass("new-question-form-loaded")) {
            // already loaded
            $modalContainer.addClass("open");
            $(document.body).addClass("modal-open");
            return;
        }

        $.ajax({
            url: "/question/getform",
            dataType: "html",
            success: function (html) {
                modalController.toggleModalCommonCallback(html);
                $modalContainer.addClass("new-question-form-loaded");
            }
        });
    }

    var showUserProfileDropDown = function () {
        event.stopPropagation();
        $(".user-profile-dropdown").show();
    }

    var search = function (e) {
        var input = $(".search").val();
        if (input === "" ||
        ($(e.currentTarget).hasClass("search") && e.keyCode === 27)) {
            $(".search-result-container").html("");
            return;
        }

        if ($(e.currentTarget).hasClass("search") && e.keyCode === 13 ||
            $(e.currentTarget).hasClass("btn")) {
            $.ajax({
                url: "/search/getresult?input=" + input,
                dataType: "html",
                success: function (html) {
                    if (html) {
                        $(".search-result-container").html(html);
                    }
                }
            });
        }
    }

    var init = function () {
        $(".add-question-button").on("click", toggleModalAddQuestion);
        $(".user-profile-inner").on("click", showUserProfileDropDown);
        $(".search").on("keyup", search);
        $(".search-container .btn").on("click", search);
    };

    return {
        init: init
    }
})();

