// Type definitions for jquery.bbq 1.2
// Project: http://benalman.com/projects/jquery-bbq-plugin/
// Definitions by: Adam R. Smith <https://github.com/sunetos>
// Definitions: https://github.com/borisyankov/DefinitelyTyped

/// <reference path="../jquery/jquery.d.ts" />

interface JQueryDeparam {
    /**
    * Deserialize a params string into an object, optionally coercing numbers,
    * booleans, null and undefined values; this method is the counterpart to the
    * internal jQuery.param method.
    * 
    * @name params A params string to be parsed.
    * @name coerce If true, coerces any numbers or true, false, null, and undefined to their actual value. Defaults to false if omitted.
    */
    (params: string, coerce?: bool): any;

}

interface JQueryStatic {

    deparam: JQueryDeparam;
}