var Endpoints = function() {};
var Settings = function() {};

var _endpoints = new Endpoints();
var _settings = new Settings();
var PageViewModel =
    function() {
        var self = this;
        this.viewMode = ko.observable("night-mode");
        this.setViewMode = function (item, event) {
            const viewMode = $(event.target).prop("checked") ? "night-mode" : "day-mode";

            _cookieFunctions.setCookie("day-night-view-mode", viewMode, 3650);

            const theme = viewMode === "night-mode" ? _endpoints.darkTheme : _endpoints.lightTheme;

            $("head").append(`<link rel="stylesheet" type="text/css" href="${theme}">`);

            self.viewMode(viewMode);
        };
        this.viewModeChecked = ko.computed(function () {
            return self.viewMode() === "night-mode";
        });

        this.bindClipboard = function() {
            const clipboard = new window.ClipboardJS(".clipboard");
            clipboard.on("success",
                function(e) {
                    $(".clipboard-success").fadeIn("slow",
                        function() {
                            setTimeout(function() {
                                    $(".clipboard-success").fadeOut("slow");
                                },
                                3000);
                        });
                });
        };
    };

var _cookieFunctions = {
    setCookie: function (name, value, days) {
        var expires;

        if (days) {
            const date = new Date();
            date.setTime(date.getTime() + days * 24 * 60 * 60 * 1000);
            expires = `; expires=${date.toGMTString()}`;
        } else {
            expires = "";
        }
        document.cookie = encodeURIComponent(name) + "=" + encodeURIComponent(value) + expires + "; path=/";
    },
    getCookie: function (name) {
        const nameEq = encodeURIComponent(name) + "=";
        const ca = document.cookie.split(';');
        for (let i = 0; i < ca.length; i++) {
            let c = ca[i];
            while (c.charAt(0) === " ")
                c = c.substring(1, c.length);
            if (c.indexOf(nameEq) === 0)
                return decodeURIComponent(c.substring(nameEq.length, c.length));
        }
        return null;
    },
    removeCookie: function (name) {
        setCookie(name, "", -1);
    }
};

var _pageViewModel = new PageViewModel();

$(document).ready(function() {
    ko.applyBindings(_pageViewModel);
    _pageViewModel.viewMode(_settings.viewMode);
    _pageViewModel.bindClipboard();
});

