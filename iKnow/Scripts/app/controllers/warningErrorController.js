var WarningErrorController = (function () {
   var hideError = function () {
        $(this).hide();
    }

    var cleanUpErrorAndWarning = function () {
        $(".warning").removeClass("warning-display severe-warning");
        $(".error").removeClass("validation-summary-errors");
    }

    var showWarning = function (message, isSevere) {
        var $warning = $(".warning");
        cleanUpErrorAndWarning();
        $warning.html("");
        $("<div>").html(message).appendTo($warning);
        if (isSevere) {
            $warning.addClass("severe-warning");
        }
        setTimeout(function () {
            $warning.addClass("warning-display");
        }, 50);
    }

    var checkWarningAndErrorOnInit = function () {
        if (iknow.pageError) {
            showWarning(iknow.pageError, true);
        }

        if (iknow.statusMessage) {
            showWarning(iknow.statusMessage);
        }
    };

    var init = function () {
        $(".error").on("click", "li", hideError);
        $(".warning").on("click", "div", hideError);
        checkWarningAndErrorOnInit();
    };

    return {
        init: init,
        hideError: hideError,
        showWarning: showWarning,
        cleanUpErrorAndWarning: cleanUpErrorAndWarning
    }
})();

