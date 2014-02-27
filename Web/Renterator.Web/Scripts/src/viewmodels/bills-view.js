(function (global, ko) {
    // Imports
    var ServiceHelper = global.renterator.common.ServiceHelper;
    var Dialog = global.renterator.common.Dialog;

    // Define the BillType view model
    function BillType(data) {
        var self = this;

        self.id = data.Id;
        self.name = data.Name;
    }

    // Define the Bill view model 
    function Bill(data) {
        // Alias this to self
        var self = this;

        // Allows us to create a new instance with default constructor
        data = data || {};

        // Properties
        self.id = data.Id;
        self.date = ko.observable(data.Date ? moment(data.Date) : moment()).extend({ required: true });
        self.description = ko.observable(data.Description || '').extend({ required: true, maxLength: 200 });
        self.amount = ko.observable(data.Amount || 0.0).extend({ required: true });
        self.billTypeId = ko.observable(data.BillTypeId || 1).extend({ required: true });

    }

    // Define the BillsView view model
    function BillsView(data) {
        // Alias this to self
        var self = this;

        // Helpers for messages & services
        self.serviceHelper = new ServiceHelper();

        // Map bill types
        self.billTypes = ko.utils.arrayMap(data.BillTypes || [], function (item) { return new BillType(item); });
        
        // Helper to identify type names
        self.billTypeMap = Enumerable.from(self.billTypes).toObject(function (x) { return x.id; }, function (x) { return x.name; });

        // Map bills
        var bills = ko.utils.arrayMap(data.Bills || [], function (item) { return new Bill(item); });
        self.bills = ko.observableArray(bills);
    }

    // Methods for BillsView class
    BillsView.prototype = {
        formatCurrency: function (numericValue) {
            var isNegative = numericValue < 0,
                stringValue = '$' + Math.abs(numericValue).toFixed(2);

            return isNegative ? '(' + stringValue + ')' : stringValue;
        },
        newBillDialog: function () {
            var self = this,
                viewModel = {
                    billTypes: self.billTypes,
                    bill: new Bill()
                };

            Dialog.show(viewModel, 'New Bill', 'new-bill-dialog', function (model, dialog) {
                self.bills.unshift(viewModel.bill);
                dialog.closeDialog();
            });
        }
    };

    // Exports
    global.renterator = global.renterator || {};
    global.renterator.viewmodels = global.renterator.viewmodels || {};
    global.renterator.viewmodels.BillsView = BillsView;
})(window, window.ko);