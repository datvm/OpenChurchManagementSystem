(function () {
    // Set Navigation
    var pathName = window.location.pathname;
    if (pathName == "/Admin") {
        $("#lnk-navigation").find("a[href='/Admin']")
            .closest("li")
            .addClass("active");
    }
    else {
        $("#lnk-navigation").find("a[href]").each(function () {
            var href = $(this).attr("href");
            if (href == "/Admin") {
                return;
            }
            if (pathName.startsWith(href)) {
                $(this).closest("li").addClass("active");
            }
        });
    }
}());
//# sourceMappingURL=admin.js.map