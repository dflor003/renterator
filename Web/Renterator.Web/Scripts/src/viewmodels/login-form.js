(function (global, ko) {
    // Imports
    var Utils = global.renterator.common.Utils;
    var ServiceHelper = global.renterator.common.ServiceHelper;
    var MessageLog = global.renterator.common.MessageLog;

    // Define the View Model
    function LoginForm() {
        // Alias self to this (helps when
        var self = this;

        // Helpers
        self.serviceHelper = new ServiceHelper();
        self.log = new MessageLog();

        // Knockout properties
        self.email = ko.observable('').extend({ required: true });
        self.password = ko.observable('').extend({ required: true });

        // Register to be tracked by the message log
        self.log.track(self);
    }

    LoginForm.prototype = {
        login: function () {
            var self = this;
            self.log.clearErrors();
            self.serviceHelper
                .post('~/api/users/login', { Email: self.email(), Password: self.password() })
                .fail(function (response) {
                    self.log.logMessages(response.Messages);
                });
        }
    };

    // Exports
    global.renterator = global.renterator || {};
    global.renterator.viewmodels = global.renterator.viewmodels || {};
    global.renterator.viewmodels.LoginForm = LoginForm;
})(window, window.ko);