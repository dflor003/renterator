var renterator;
(function (renterator) {
    /// <reference path="../../typings/jquery/jquery.d.ts" />
    /// <reference path="../../typings/knockout/knockout.d.ts" />
    /// <reference path="../../typings/knockout.validation/knockout.validation.d.ts" />
    /// <reference path="../../typings/bootstrap/bootstrap.d.ts" />
    (function (common) {
        var Dialog = (function () {
            function Dialog(viewModel, templateName, title, settings) {
                this.$dialogElement = null;
                this.errors = ko.observableArray();
                this.viewModel = viewModel;
                this.templateName = templateName;
                this.title = title;
                this.dom = settings.dom || $('body');
                this.onSave = settings.save;
                this.closeDialogOnSave = typeof settings.closeOnSave === 'boolean' ? settings.closeOnSave : true;
                this.showModalDialog = this.showModalDialog.bind(this);

                this.viewModelValidation = ko.validatedObservable(viewModel);
            }
            Dialog.prototype.init = function () {
                // Setup
                var $appendTarget = this.dom, $dialogElement = $('<div data-bind="template: { name: \'' + Dialog.modalDialogTemplate + '\', afterRender: showModalDialog }"></div>');

                // Add element to the dom
                $appendTarget.append($dialogElement);

                // Bind it with knockout
                this.$dialogElement = $dialogElement;
                ko.applyBindings(this, $dialogElement[0]);
            };

            Dialog.prototype.showModalDialog = function () {
                var _this = this;
                this.$dialogElement.find('.modal').modal({ show: true, keyboard: true }).on('hidden.bs.modal', function () {
                    _this.$dialogElement.remove();
                });
            };

            Dialog.prototype.closeDialog = function () {
                this.$dialogElement.find('.modal').modal('hide');
            };

            Dialog.prototype.save = function () {
                this.clearErrors();

                if (this.viewModelValidation.isValid()) {
                    this.onSave(this.viewModel, this);

                    // If true, then closing of dialog is delegated to the caller
                    if (this.closeDialogOnSave) {
                        this.closeDialog();
                    }
                }
            };

            Dialog.prototype.addErrors = function (messages) {
                ko.utils.arrayPushAll(this.errors, messages);
            };

            Dialog.prototype.clearErrors = function () {
                this.errors.removeAll();
            };

            Dialog.show = function (viewModel, templateName, title, settings) {
                var dialog = new Dialog(viewModel, templateName, title, settings);
                dialog.init();
                return dialog;
            };
            Dialog.modalDialogTemplate = 'common/modal-dialog';
            Dialog.templateExtension = '.tmpl.html';
            return Dialog;
        })();
        common.Dialog = Dialog;
    })(renterator.common || (renterator.common = {}));
    var common = renterator.common;
})(renterator || (renterator = {}));
//# sourceMappingURL=dialog.js.map
