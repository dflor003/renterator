(function (global, ko) {
    // Imports
    var Utils = global.renterator.common.Utils;
    var ServiceHelper = global.renterator.common.ServiceHelper;
    var MessageLog = global.renterator.common.MessageLog;

    // Define the View Model
    function LoginForm() {
        // Alias this to self
        var self = this;

        // Helpers for messages & services
        self.serviceHelper = new ServiceHelper();
        self.log = new MessageLog();

        // Knockout properties
        self.email = ko.observable('').extend({ required: true });
        self.password = ko.observable('').extend({ required: true });
        self.isLoggingIn = ko.observable(false);

        // Register to be tracked by the message log
        self.log.track(self);
    }

    // Methods of the view model
    LoginForm.prototype = {
        login: function () {
            var self = this,
                log = self.log;

            // Clear out prior errors
            log.clearErrors();
            log.logInfo('Logging in...');

            // Set logging in flag to disable UI
            self.isLoggingIn(true);

            // Call into service to do an ajax login
            self.serviceHelper
                .post('~/api/users/login', { Email: self.email(), Password: self.password() })
                .done(function () {
                    // Login succeeded, token acquired, redirect to Home/Index
                    log.clearMessages();
                    log.logSuccess('Successfully logged in!');
                    Utils.navigate("~/Home/Index");
                })
                .fail(function (response) {
                    // Log any errors
                    log.logMessages(response.Messages);
                    self.isLoggingIn(false);
                });
        }
    };

    // Exports
    global.renterator = global.renterator || {};
    global.renterator.viewmodels = global.renterator.viewmodels || {};
    global.renterator.viewmodels.LoginForm = LoginForm;
})(window, window.ko);