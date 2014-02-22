
declare module Ladda {

    interface ILaddaButton {
        start(): ILaddaButton;

        stop(): ILaddaButton;

        toggle(): ILaddaButton;

        setProgress(progress: number): ILaddaButton;

        enable(): ILaddaButton;

        disable(): ILaddaButton;

        isLoading(): boolean;
    }

    interface ILaddaOptions {
        timeout?: number;
        callback?: (instance: ILaddaButton) => void;
    }

    function bind(target: HTMLElement, options?: ILaddaOptions);
    function bind(cssSelector: string, options?: ILaddaOptions);

    function create(button: HTMLElement): ILaddaButton;
    
    function stopAll(): void;
}