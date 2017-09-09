// ======================================



// 

// ======================================

import { Component, OnInit, OnDestroy } from '@angular/core';
import { fadeInOut } from '../../services/animations';
import { MCSDataService } from '../../services/mcs.data.service';
import { FeedService } from '../../services/mcs.feed.service';

import { SignalRConnectionStatus } from '../../interfaces';
import { AlertService, AlertDialog, DialogType, AlertMessage, MessageSeverity } from '../../services/alert.service';
import { SystemMap } from '../../models/mcs.model';

@Component({
    selector: 'systemmap',
    templateUrl: './systemmap.component.html',
    styleUrls: ['./systemmap.component.css'],
    animations: [fadeInOut]
})
export class SystemMapComponent implements OnInit{

    connectionId: string;
    error: any;
    isLoading = false;
    public filterQuery = "";
    public rowsOnPage = 25;
    public sortBy = "email";
    public sortOrder = "asc";
    private systemMapData: SystemMap[] = [];

    constructor(private alertService: AlertService, private dataService: MCSDataService, private feedService: FeedService) {
    }

    ngOnInit() {
        console.log('on init system map');
        this.systemMap();
    }

    OnDestroy() {
        let self = this;
    }

    systemMap() {
        this.isLoading = true;
        this.alertService.startLoadingMessage("", "Attempting to retrieve data...");
        this.dataService.getSystemMap()
            .subscribe((data) => {
                this.systemMapData = data;
                this.alertService.stopLoadingMessage();
                this.isLoading = false;
            });
    }

    selectStation(stationId: number) {
        if (this.feedService.stationId == stationId)
        {
            return;
        }

        this.feedService.stop();
        this.alertService.startLoadingMessage("Station Selected", "Attempting to switch station..." + stationId);
        this.feedService.setStationId(stationId);
        this.feedService.start(true).subscribe(connState => {
            if (connState == SignalRConnectionStatus.Connected) {
                this.feedService.subscribeToEventHistory();
            }
        });
        
        this.alertService.stopLoadingMessage();
        this.isLoading = false;
    }

    public toInt(num: string) {
        return +num;
    }

    public sortByWordLength = (a: any) => {
        return a.city.length;
    }
}
