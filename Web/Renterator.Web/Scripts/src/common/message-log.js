(function (global, ko, Enumerable) {

    // MessageType Enum
    /* jshint ignore:start */
    var MessageType = {};
    MessageType[MessageType['Error'] = 0] = 'Error';
    MessageType[MessageType['Warning'] = 1] = 'Warning';
    MessageType[MessageType['Info'] = 2] = 'Info';
    MessageType[MessageType['Success'] = 3] = 'Success';
    /* jshint ignore:end */

    // Log Entry Class
    function LogEntry(type, message) {
        this.type = type;
        this.message = message;
    }

    function MessageLog() {
        var self = this;
        self.typeToCollectionMap = {};

        self.manualErrors = ko.observableArray([]);
        self.warnings = ko.observableArray([]);
        self.infos = ko.observableArray([]);
        self.successes = ko.observableArray([]);
        self.trackedViewModels = ko.observableArray([]);

        // Computed to have collection of manual errors and viewmodel errors
        self.errors = ko.computed({
            read: function () {
                var models = self.trackedViewModels(),
                    otherErrors = self.manualErrors();

                return Enumerable
                    .from(models)
                    .where(function (state) { return state.model.isAnyMessageShown(); })
                    .selectMany(function (state) { return self.getValidatables(state.model); })
                    .where(function (validatable) { return validatable.isModified() && !validatable.isValid(); })
                    .select(function (validatable) { return new LogEntry(MessageType.Error, validatable.error()); })
                    .union(otherErrors)
                    .toArray();
            },
            deferEvaluation: true
        });

        // Map from type to collection
        self.typeToCollectionMap[MessageType.Error] = self.manualErrors;
        self.typeToCollectionMap[MessageType.Warning] = self.warnings;
        self.typeToCollectionMap[MessageType.Info] = self.infos;
        self.typeToCollectionMap[MessageType.Success] = self.successes;

        self.logMessages = self.logMessages.bind(self);
    }

    MessageLog.prototype = {
        logError: function (message) {
            this.log(MessageType.Error, message);
        },

        logWarning: function (message) {
            this.log(MessageType.Warning, message);
        },

        logInfo: function (message) {
            this.log(MessageType.Info, message);
        },

        logSuccess: function (message) {
            this.log(MessageType.Success, message);
        },

        log: function (type, message) {
            if (typeof type !== 'number') {
                throw new Error('MessageLog: No message type passed');
            }
            if (typeof message !== 'string') {
                throw new Error('MessageLog: No message specified');
            }

            // Add based on enum value
            var logEntry = new LogEntry(type, message), collectionToAddTo = this.typeToCollectionMap[type];
            collectionToAddTo.push(logEntry);
        },

        logMessages: function (messages) {
            if (!messages || !messages.length) {
                return;
            }

            var self = this;
            ko.utils.arrayForEach(messages, function (entry) {
                return self.log(entry.Type, entry.Message);
            });
        },

        clearErrors: function () {
            this.manualErrors.removeAll();
        },
        
        clearWarnings: function () {
            this.warnings.removeAll();
        },

        clearInfos: function () {
            this.infos.removeAll();
        },

        clearSuccesses: function () {
            this.successes.removeAll();
        },

        clearMessages: function () {
            this.clearErrors();
            this.clearWarnings();
            this.clearInfos();
            this.clearSuccesses();
        },

        track: function (viewModel) {
            this.trackedViewModels.push({
                model: viewModel,
                validator: ko.validatedObservable(viewModel)
            });
        },

        untrack: function(viewModel) {
            this.trackedViewModels.remove(function (m) {
                 return m.model === viewModel;
            });
        },

        getValidatables: function (viewModel) {
            var results = [];

            for(var prop in viewModel) {
                var current = viewModel[prop];
                if (current && ko.validation.utils.isValidatable(current)) {
                    results.push(current);
                }
            }

            return results;
        }
    };

    // Exports
    global.renterator = global.renterator || {};
    global.renterator.common = global.renterator.common || {};
    global.renterator.common.MessageLog = MessageLog;
    global.renterator.common.LogEntry = LogEntry;
    global.renterator.common.MessageType = MessageType;
})(window, window.ko, window.Enumerable);