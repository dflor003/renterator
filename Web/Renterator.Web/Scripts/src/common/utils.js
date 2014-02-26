(function (global, $) {
    function QueryStringDictionary(queryString) {
        var self = this;
        if (typeof queryString === "undefined") {
             queryString = window.location.search;
        }

        self.isInitialized = false;
        self.queryStringDict = {};
        if (typeof queryString !== 'string') {
            throw new Error('QueryStringDictionary: queryString must be a string');
        }

        // Save it for on-demand initialization
        self.queryString = queryString;
    }

    QueryStringDictionary.prototype = {
        get: function (key) {
            if (!this.isInitialized) {
                this.initialize();
            }

            var value = this.queryStringDict[key];
            return Utils.isNullOrUndefined(value) ? null : value;
        },

        initialize: function () {
            // Setup
            var queryString = this.queryString;
            this.isInitialized = true;

            if (!queryString || queryString.indexOf('?') !== 0)
                return;

            // User jquery.bbq's deparams method to parse query string
            var trimmedQueryString = queryString.substring(1);
            this.queryStringDict = $.deparam(trimmedQueryString, true);
        }
    };

    var Utils = {
        format: function (formatString, params) {
            if (!formatString)
                return formatString;
            var result = formatString;

            // If invoked using a name-value map, replace {key} with value.toString(), otherwise, treat it like string.format
            var formatArgs = arguments.length === 2 && $.isPlainObject(arguments[1]) ? arguments[1] : $.makeArray(arguments).slice(1);
            $.each(formatArgs, function (keyOrIndex, value) {
                var replacedValue = typeof value === 'undefined' || value === null ? '' : value.toString();
                result = result.replace('{' + keyOrIndex + '}', replacedValue);
            });

            return result;
        },

        isNullOrUndefined: function (value) {
            return value === null || typeof value === 'undefined';
        },

        coalesce: function (value, defaultValue) {
            return Utils.isNullOrUndefined(value) ? defaultValue : value;
        },

        resolveUrl: function (url, queryParams) {
            if (typeof url !== 'string') {
                throw new Error('ServiceHelper: No url passed');
            }

            // Resolve path by replacing '~' with root
            var rootPath = GlobalVars && GlobalVars.RootPath ? GlobalVars.RootPath : '', resolvedPath = url.indexOf('~') === 0 ? url.replace('~', rootPath) : url.indexOf('/') === 0 ? rootPath + url : rootPath + '/' + url;

            // Get query params if any
            var queryString = queryParams ? $.param(queryParams) : null;

            // Concatenate on any query string params
            var actualUrl = !queryString ? resolvedPath : url.indexOf('?') > 0 ? resolvedPath + '&' + queryString : resolvedPath + '?' + queryString;

            return actualUrl;
        },

        navigateToRoute: function () {
            var routeParts = [];
            for (var _i = 0; _i < (arguments.length - 0); _i++) {
                routeParts[_i] = arguments[_i + 0];
            }
            var routePath = ['~'].concat(routeParts).join('/');
            Utils.navigate(routePath);
        },

        navigate: function (url, queryParams) {
            var resovledUrl = Utils.resolveUrl(url, queryParams);
            window.location.assign(resovledUrl);
        },

        override: function (obj, methodName, newImpl) {
            // Store old function
            var old = obj[methodName];

            if (typeof old !== 'function') {
                throw new Error("Utils.overload: '" + methodName + "' is not a function on the provided object");
            }
            if (typeof newImpl !== 'function') {
                throw new Error("Utils.overload: New implementation is not a valid function");
            }

            // Override it
            obj[methodName] = function () {
                // Keep the args passed and add on the old func as the first arg
                var args = $.makeArray(arguments);
                args.splice(0, 0, old);

                // Call into the new implementation
                return newImpl.apply(this, args);
            };
        },

        formatAsCurrency: function (numericValue) {
            var isNegative = numericValue < 0,
                stringValue = '$' + Math.abs(numericValue).toFixed(2);

            return isNegative ? '(' + stringValue + ')' : stringValue;
        },

        queryString: new QueryStringDictionary()
    };

    // Exports
    global.renterator = global.renterator || {};
    global.renterator.common = global.renterator.common || {};
    global.renterator.common.Utils = Utils;
})(window, window.jQuery);
