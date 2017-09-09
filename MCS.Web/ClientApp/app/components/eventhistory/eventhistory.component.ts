// ======================================



// 

// ======================================

import { Component, OnInit, OnDestroy } from '@angular/core';
import { fadeInOut } from '../../services/animations';
import { FeedService } from '../../services/mcs.feed.service';
import { MCSDataService } from '../../services/mcs.data.service';
import { SignalRConnectionStatus } from '../../interfaces';
import { AlertService, AlertDialog, DialogType, AlertMessage, MessageSeverity } from '../../services/alert.service';
import { EventHistory } from '../../models/mcs.model';

@Component({
    selector: 'eventhistory',
    templateUrl: './eventhistory.component.html',
    styleUrls: ['./eventhistory.component.css'],
    animations: [fadeInOut]
})
export class EventHistoryComponent implements OnInit{

    connectionId: string;
    error: any;
    isLoading = false;
    public data;
    public filterQuery = "";
    public rowsOnPage = 25;
    public sortBy = "email";
    public sortOrder = "asc";
    private eventHistoryData: EventHistory[] = [];
    private subscribed: boolean = false;
    private pause: boolean = false;

    constructor(private feedService: FeedService, private alertService: AlertService,
        private dataService: MCSDataService) {
    }

    ngOnInit() {
        console.log("Event History - OnInit")
        this.eventHistoryList();

        console.log("Setting subscription...")
        this.setSubscription();
        //this.feedService.subscribeToEventHistory();
    }

    OnDestroy() {
        console.log("Event History - OnDestroy")
        let self = this;
        //self.feedService.unsubscribeFromEventHistory();
        console.log("Event History - unsubscribed")
    }

    eventHistoryList() {
        this.isLoading = true;
        this.alertService.startLoadingMessage("", "Attempting to retrieve data...");
        this.alertService.showMessage(this.feedService.stationId.toString());
        this.dataService.getEventHistoryAll(this.feedService.stationId)
            .subscribe((data) => {
                console.log('Received Event History : ' + data);
                this.eventHistoryData = data;
                this.alertService.stopLoadingMessage();
                this.isLoading = false;
            });
    }

    setSubscription() {
        //this.feedService.connectionState.subscribe(
        //    connectionState => {
        //        if (connectionState == SignalRConnectionStatus.Connected) {
        //            this.feedService.subscribeToEventHistory();
        //            this.subscribed = true;
        //            this.alertService.showStickyMessage("EventHistoryComponent - SignalR Connection", "Connected to the Server", MessageSeverity.info);
        //        }
        //    },
        //    error => console.log('Error on init: ' + error));

        this.feedService.updateEventHistory.subscribe(eventHistory => {
            console.log('new event received ' + eventHistory.stationAbbr);
            if (this.pause == false) {
                this.eventHistoryData.unshift(eventHistory);

                if (this.eventHistoryData.length >= 2000) {
                    this.eventHistoryData.pop();
                }
            }
        });
    }

    pauseDisplay(bVal: boolean) {
        if (this.pause == true && bVal == false)
        {
            console.log('resuming display - retrieveing event history');
            this.pause = bVal;
            //load the history again
            this.eventHistoryList();
            return;
        }

        if (bVal == true) {
            console.log('pause display - new history will not be displayed');
        }
        this.pause = bVal;
     }

    public toInt(num: string) {
        return +num;
    }

    public sortByWordLength = (a: any) => {
        return a.city.length;
    }
}
