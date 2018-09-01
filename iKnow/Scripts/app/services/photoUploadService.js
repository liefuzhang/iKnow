var PhotoUploadService = (function () {
    var getForm = function (success, userId) {
        $.ajax({
            url: "/account/editProfilePhoto/" + userId,
            dataType: "html",
            success: success,
            fail: function () {
                console.error("Something went wrong with PhotoUploadService getForm.");
            }
        });
    };
    return {
        getForm: getForm
    }
})();