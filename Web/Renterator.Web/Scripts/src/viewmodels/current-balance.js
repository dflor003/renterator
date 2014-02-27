(function (global, ko) {
    // Imports
    var ServiceHelper = global.renterator.common.ServiceHelper;
    var MessageLog = global.renterator.common.MessageLog;

    // Define the CurrentBalanceView view model
    function CurrentBalanceView(data) {
        // Alias this to self
        var self = this;

        // Helpers for messages & services
        self.serviceHelper = new ServiceHelper();
        self.log = new MessageLog();

        // Knockout properties
        self.balance = ko.observable(data.Balance || 0.0);
        self.runningTotalBase = ko.observable(data.RunningTotalBase || 0.0);

        // Knockout computed property to get balance in format $###.## when > 0 and ($###.##) when less than 0
        self.formattedBalance = ko.computed({
            read: function () {
                return self.formatCurrency(self.balance());
            }
        });

        // Map raw model data to viewmodel
        var items = ko.utils.arrayMap(data.MostRecentEntries || [], function (item) {
            return new AccountEntry(item);
        });
        self.entries = ko.observableArray(items);

        // Calculate running total for view models
        self.entries().forEach(function (item, index) {
            item.runningTotal = self.runningTotalForIndex(index);
        });
    }

    // Methods for CurrentBalanceView class
    CurrentBalanceView.prototype = {
        formatCurrency: function (numericValue) {
            var isNegative = numericValue < 0,
                stringValue = '$' + Math.abs(numericValue).toFixed(2);

            return isNegative ? '(' + stringValue + ')' : stringValue;
        },
        runningTotalForIndex: function (index) {
            var self = this,
                base = self.runningTotalBase(),
                entries = self.entries();

            return base + Enumerable
                .from(entries)
                .skip(index)
                .sum(function (entry) {
                    return entry.amount;
                });
        }
    };

    // Define the AccountEntry view model
    function AccountEntry(data) {
        // Alias this to self
        var self = this;
        
        // Prevent exceptions when data is null
        data = data || {};

        // Properties (not observable because they won't change)
        self.id = data.Id;
        self.date = moment(data.Date);
        self.description = data.Description || '';
        self.amount = data.Amount || 0.0;
        self.runningTotal = 0.0;
    }

    // Exports
    global.renterator = global.renterator || {};
    global.renterator.viewmodels = global.renterator.viewmodels || {};
    global.renterator.viewmodels.CurrentBalanceView = CurrentBalanceView;
})(window, window.ko);