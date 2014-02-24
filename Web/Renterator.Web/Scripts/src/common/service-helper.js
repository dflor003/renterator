(function (global, $) {

    // HttpMethod Enum
    /* jshint ignore:start */
    var HttpMethod = {};
    HttpMethod[HttpMethod['POST'] = 0] = 'POST';
    HttpMethod[HttpMethod['GET'] = 1] = 'GET';
    HttpMethod[HttpMethod['PUT'] = 2] = 'PUT';
    HttpMethod[HttpMethod['DELETE'] = 3] = 'DELETE';
    /* jshint ignore:end */
    
    var globalVars = global.GlobalVars || {};
    var Utils = global.renterator.common.Utils;

    function ServiceHelper(rootPath) {
        this.rootPath = typeof (rootPath) === 'string'
            ? rootPath
            : globalVars.RootPath === '/'
                ? ''
                : globalVars.RootPath || '';
    }

    ServiceHelper.prototype = {
        post: function (url, params) {
            return this.callService(HttpMethod.POST, this.makeUrl(url), params);
        },

        get: function (url, queryParams) {
            return this.callService(HttpMethod.GET, this.makeUrl(url, queryParams));
        },

        put: function (url, params) {
            return this.callService(HttpMethod.PUT, this.makeUrl(url), params);
        },

        delete: function (url, queryParams) {
            return this.callService(HttpMethod.DELETE, this.makeUrl(url, queryParams));
        },

        makeUrl: function (url, queryParams) {
            return Utils.resolveUrl(url, queryParams);
        },

        callService: function (method, url, postParams) {
            if (!HttpMethod[method]) {
                throw new Error('SericeHelper: Invalid HTTP method');
            }
            if (typeof url !== 'string') {
                throw new Error('ServiceHelper: No url specified');
            }

            var asyncResult = $.Deferred();
            $.ajax({
                url: url,
                contentType: 'application/json',
                cache: false,
                data: postParams ? JSON.stringify(postParams) : undefined,
                type: HttpMethod[method],
                success: function (data) {
                    asyncResult.resolve(data);
                },
                error: function (jqXhr, status, description) {
                    var errorResult = jqXhr.responseJSON || jqXhr.responseText || description;
                    asyncResult.reject(errorResult);
                }
            });

            return asyncResult;
        }
    };

    // Exports
    global.renterator = global.renterator || {};
    global.renterator.common = global.renterator.common || {};
    global.renterator.common.ServiceHelper = ServiceHelper;
    global.renterator.common.HttpMethod = HttpMethod;
})(window, window.jQuery);
