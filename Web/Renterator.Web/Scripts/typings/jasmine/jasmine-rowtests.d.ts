// Type definitions for jasmine-rowtests.js 1.0
// Project: https://github.com/dflor003/jasmine-rowtests
// Definitions by: Danil Flores <https://github.com/dflor003/>
// Definitions: https://github.com/borisyankov/DefinitelyTyped

declare function given(values: any[]): jasmine.ItRowSingleSpec;
declare function given(values: any[][]): jasmine.ItRowSpec;

declare module jasmine {
    interface ItRowSingleSpec
    {
        it(description: string, specFunction: (value: any) => void );
        xit(description: string, specFunction: (value: any) => void );
    }

    interface ItRowSpec
    {
        it(description: string, specFunction: (...value: any[]) => void );
        xit(description: string, specFunction: (...value: any[]) => void );
    }
}