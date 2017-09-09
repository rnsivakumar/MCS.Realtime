
export class EventHistory {
    public historyId: string;
    public stationId: number;
    public stationAbbr: string;
    public equipmentNo: string;
    public eventDateTime: Date;
    public eventId: number;
    public deviceType: string;
    public eventDesc: string;
    public severity: string;
    public eventState: string;
    public eventColor: string;
}


export class SystemMap {
    stationId: number;
    stationName: string;
    stationAbbr: string;
    deviceStateName: string;
    totalDevices: number;
    color: string;
}