(function (global, $) {

    // Dialog viewmodel
    function Dialog(viewModel, title, templateName, onSave) {
        var self = this;

        self.viewModel = viewModel;
        self.templateName = templateName;
        self.onSave = onSave;
        self.$dialogElement = null;

        self.viewModel.dialog = {
            show: function () { self.showModalDialog(); },
            save: function () { self.save(); },
            title: title
        };
    }

    // Static helper function
    Dialog.show = function (viewModel, title, templateName, onSave) {
        var dialog = new Dialog(viewModel, title, templateName, onSave);
        dialog.init();
        return dialog;
    };

    Dialog.prototype = {
        init: function () {
            var $appendTarget = $('body'),
                $dialogElement = $('<div data-bind="template: { name: \'' + this.templateName + '\', afterRender: dialog.show }"></div>');

            $appendTarget.append($dialogElement);

            this.$dialogElement = $dialogElement;
            ko.applyBindings(this.viewModel, this.$dialogElement[0]);
        },

        showModalDialog: function () {
            var self = this;

            self.$dialogElement
                .find('.modal')
                .modal({ show: true, keyboard: true })
                .on('hidden.bs.modal', function () {
                    self.$dialogElement.remove();
                });
        },

        closeDialog: function () {
            this.$dialogElement
                .find('.modal')
                .modal('hide');
        },

        save: function () {
            this.onSave(this.viewModel, this);
        }
    };

    // Exports
    global.renterator = global.renterator || {};
    global.renterator.common = global.renterator.common || {};
    global.renterator.common.Dialog = Dialog;
})(window, window.jQuery);