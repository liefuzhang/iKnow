var appController = (function() {
    var deleteEntity = function(e) {
        var button = $(this);
        var message = button.attr("data-delete-message");
        return confirm(message);
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
        ModalController.init();
        HeaderController.init();
        WarningErrorController.init();

        $(window).scroll(backToTopDetect);
        $(".back-top").on("click", goToTop);
    };

    return {
        init: init, 
        deleteEntity: deleteEntity
    }
})();
