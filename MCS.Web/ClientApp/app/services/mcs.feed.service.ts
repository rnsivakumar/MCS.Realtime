import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
//import { HubConnection, HttpConnection, ConsoleLogger, IHttpClient, LogLevel, WebSocketTransport } from '../../../wwwroot/lib/signalr-client/dist/src'
import { HubConnection, HttpConnection, IHttpConnectionOptions, ConsoleLogger, IHttpClient, LogLevel, WebSocketTransport, TransportType } from '../signalr'
import 'rxjs/add/operator/toPromise';
import { Observable } from "rxjs/Observable";
import { Subject } from "rxjs/Subject";
import { EventHistory } from '../models/mcs.model';
import { FeedSignalR, FeedProxy, FeedClient, FeedServer, SignalRConnectionStatus, ChatMessage} from '../interfaces';
import { EventHistoryData } from '../interfaces';
@Injectable()
export class FeedService {

    currentState = SignalRConnectionStatus.Disconnected;
    connectionState: Observable<SignalRConnectionStatus>;

    setConnectionId: Observable<string>;
    updateEventHistory: Observable<EventHistory>;
    addEventHistory: Observable<string>;
    addChatMessage: Observable<ChatMessage>;
    eventHistoryRef: EventHistory;

    private connectionStateSubject = new Subject<SignalRConnectionStatus>();
    
    private setConnectionIdSubject = new Subject<string>();
    private updateEventHistorySubject = new Subject<EventHistory>();
    private addEventHistorySubject = new Subject<string>();
    private addChatMessageSubject = new Subject<ChatMessage>();
    private hubConn: HubConnection;

    private server: FeedServer;
    public stationId: number;

    constructor(private http: Http) {
        console.log('constructor FeedService');

        this.connectionState = this.connectionStateSubject.asObservable();
        this.setConnectionId = this.setConnectionIdSubject.asObservable();
        this.updateEventHistory = this.updateEventHistorySubject.asObservable();
        this.addEventHistory = this.addEventHistorySubject.asObservable();
        this.addChatMessage = this.addChatMessageSubject.asObservable();
        this.stationId = 301;
    }

    start(debug: boolean): Observable<SignalRConnectionStatus> {

        console.log('starting signalR Connection: ');
        //return this.connectionState;
        //let options: IHttpConnectionOptions = {
        //    httpClient: <IHttpClient>{
        //        options(url: string): Promise<string> {
        //            return Promise.resolve("{ \"connectionId\": \"42\", \"availableTransports\": [] }");
        //        },
        //        get(url: string): Promise<string> {
        //            return Promise.resolve("");
        //        }
        //    },
        //    transport: requestedTransport,
        //    logging: null
        //} as IHttpConnectionOptions;


        let logger = new ConsoleLogger(LogLevel.Information);
        //let options: {
        //    httpClient: <IHttpClient>{
        //        options(url: string): Promise<string> {
        //            return Promise.resolve("{ \"connectionId\": \"42\", \"availableTransports\": [] }");
        //        },
        //        get(url: string): Promise<string> {
        //            return Promise.resolve("");
        //        }
        //    },
        //    transport: TransportType.WebSockets,
        //    logging: logger
        //} as IHttpConnectionOptions;

        let conn = new HttpConnection(`http://${document.location.host}/hubEventHistory`, { transport: TransportType.WebSockets, logging: logger });
        //let conn = new HttpConnection(`http://${document.location.host}/hubEventHistory`, { transport: TransportType.WebSockets, logging: logger });
        this.hubConn = new HubConnection(conn);

        this.hubConn.on('updateEventHistory', eventHistory => { this.onUpdateEventHistory(eventHistory) });
        this.hubConn.on('setConnectionId', ConnectionId => { this.onSetConnectionId(ConnectionId) });
        this.hubConn.start()
            .then(() => {
                this.subscribeToEventHistory();
            })
            .catch(err => console.log(err));

        this.hubConn.onClosed = e => {
            if (e) {
                console.log('Connection closed with error: ' + e);
            }
            else {
                console.log('Disconnected');
            }
        };
        return this.connectionState;
    }

    stop() { 
        console.log('stopping signalR Connection: ');
        this.hubConn.stop();
        this.setConnectionState(SignalRConnectionStatus.Disconnected);
    }

    private setConnectionState(connectionState: SignalRConnectionStatus) {
        console.log('connection state changed to: ' + connectionState);
        this.currentState = connectionState;
        this.connectionStateSubject.next(connectionState);
    }

    // Client side methods
    private onSetConnectionId(id: string) {
        console.log('onSetConnectionId: ' + id);
        this.setConnectionIdSubject.next(id);
    }

    private onUpdateEventHistory(eventHistory: any) {
        this.eventHistoryRef = eventHistory.Value;
        //let eveHist = new EventHistory(null);
        //$.extend(eveHist, eventHistory);
        console.log('onUpdateEventHistory: Data: ' + this.eventHistoryRef);
        this.updateEventHistorySubject.next(this.eventHistoryRef);
    }

    private onAddFeed(eventHistory: string) {
        console.log('onUpdateEventHistory: ' + eventHistory.toString());
        console.log(eventHistory);
        this.addEventHistorySubject.next(eventHistory);
    }

    private onAddChatMessage(chatMessage: ChatMessage) {
        this.addChatMessageSubject.next(chatMessage);
    }

    public setStationId(stationId: number) {
        console.log('setStationId ');
        if (this.stationId == stationId) return;

        //before changing the station, unsubscribe from server
        //this.server.unsubscribe(this.stationId);

        //then change the station id
        this.stationId = stationId;

        //then subscribe for new station data
        //this.server.subscribe(this.stationId);
    }

    // Server side methods
    public subscribeToEventHistory() {
        console.log('subscribeToEventHistory: ' + this.stationId);
        this.hubConn.invoke('subscribe', this.stationId);
    }

    public unsubscribeFromEventHistory() {
        console.log('unsubscribeFromEventHistory: ' + this.stationId);
        this.hubConn.invoke('unsubscribe', this.stationId);
    }

}