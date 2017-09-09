import { EventHistory } from './models/mcs.model';

/* SignalR related interfaces  */
export interface FeedSignalR extends SignalR {
    hubEventHistory: FeedProxy
}

export interface FeedProxy {
    client: FeedClient;
    server: FeedServer;
}

export interface FeedClient {
    setConnectionId: (id: string) => void;
    updateEventHistory: (eventHistory: EventHistory) => void;
    addEventHistory: (eventHistory: string) => void;
    //addChatMessage: (chatMessage: ChatMessage) => void;
}

export interface FeedServer {
    subscribe(stationId: number): void;
    unsubscribe(stationId: number): void;
}

export enum SignalRConnectionStatus {
    Connected = 1,
    Disconnected = 2,
    Error = 3
}

/* LiveGameFeed related interfaces */
export interface Match {
    Id: number;
    Host: string;
    Guest: string;
    HostScore: number;
    GuestScore: number;
    MatchDate: Date;
    Type: string;
    Feeds: Feed[];
}

export interface Feed {
    Id: number;
    Description: string;
    CreatedAt: Date;
    MatchId: number;
}

export interface ChatMessage {
    MatchId: number;
    Text: string;
    CreatedAt: Date;
}

export interface EventHistoryData {
    historyId: string;
    stationId: number;
    stationAbbr: string;
    equipmentNo: string;
    eventDateTime: Date;
    eventId: number;
    deviceType: string;
    eventDesc: string;
    severity: string;
    eventState: string;
    eventColor: string;
}

export interface SystemMapData {
    stationId: number;
    stationName: string;
    stationAbbr: string;
    deviceStateName: string;
    totalDevices: number;
    color: string;
}
