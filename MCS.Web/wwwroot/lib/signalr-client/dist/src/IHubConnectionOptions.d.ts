import { IHubProtocol } from "./IHubProtocol";
import { ILogger, LogLevel } from "./ILogger";
export interface IHubConnectionOptions {
    protocol?: IHubProtocol;
    logging?: ILogger | LogLevel;
}
