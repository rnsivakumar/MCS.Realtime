export declare enum LogLevel {
    Information = 0,
    Warning = 1,
    Error = 2,
    None = 3,
}
export interface ILogger {
    log(logLevel: LogLevel, message: string): void;
}
