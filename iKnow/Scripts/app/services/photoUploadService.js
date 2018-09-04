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

    var savePhoto = function (userId, dataURL, done) {
        $.ajax({
            url: "/api/account",
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ "userId": userId, "dataURL": dataURL }),
            success: done,
            fail: function() {
                console.error("Something went wrong with PhotoUploadService savePhoto.");
            }
        });
    };

    return {
        getForm: getForm,
        savePhoto: savePhoto
    }
})();