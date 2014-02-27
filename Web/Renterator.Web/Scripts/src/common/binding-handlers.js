(function (global, $, ko) {
    
    ko.bindingHandlers.combo = {
        init: function (element, valueAccessor) {
            $(element).select2();
        }
    };

})(window, window.jQuery, window.ko);