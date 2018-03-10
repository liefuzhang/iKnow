var appController = (function() {
    var deleteEntity = function(e) {
        var button = $(this);
        var message = button.attr("data-delete-message");
        return confirm(message);
    }


    var pageClickHandler = function () {
        $(".user-profile-dropdown").hide();
        closeSearchResult(event);
    }

    var closeSearchResult = function (e) {
        if ($(e.target).parents(".search-container").length > 0) {
            return;
        }

        $(".search-result-container").html("");
    }

    var backToTopDetect= function () {
        if ($(this).scrollTop() > 500) {
            $(".back-top-container").fadeIn();
        } else {
            $(".back-top-container").fadeOut();
        }
    }

    var goToTop = function () {
        $("html, body")
            .animate({
                scrollTop: 0
            },
                300);
    }

    var init = function () {
        modalController.init();
        headerController.init();
        warningErrorController.init();

        $(document).on("click", pageClickHandler);
        $(window).scroll(backToTopDetect);
        $(".back-top").on("click", goToTop);
    };

    return {
        init: init, 
        deleteEntity: deleteEntity
    }
})();
