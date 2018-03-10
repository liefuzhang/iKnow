var SearchService = (function () {
    var search = function (term, success) {
        $.ajax({
            url: "/search/getresult?input=" + term,
            dataType: "html",
            success: success,
            fail: function () {
                console.error("Something went wrong with SearchService search.");
            }
        });
    }

    return {
        search: search
    }
})();