var RegisterService = (function () {
    var getRegisterForm = function(success, queryString) {
        $.ajax({
            url: "/account/register" + queryString,
            dataType: "html",
            success: success,
            fail: function () {
                console.error("Something went wrong with RegisterService getRegisterForm.");
            }
        });
    };

    return {
        getRegisterForm: getRegisterForm
    }
})();