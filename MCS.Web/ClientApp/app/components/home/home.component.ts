// ======================================



// 

// ======================================

import { Component, OnInit } from '@angular/core';
import { fadeInOut } from '../../services/animations';
import { ConfigurationService } from '../../services/configuration.service';
import { FeedService } from '../../services/mcs.feed.service';
import { SignalRConnectionStatus } from '../../interfaces';
import { AlertService, AlertDialog, DialogType, AlertMessage, MessageSeverity } from '../../services/alert.service';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
    animations: [fadeInOut]
})
export class HomeComponent implements OnInit {
    constructor(private configurations: ConfigurationService, private feedService: FeedService, private alertService: AlertService) {
    }

    connectionId: string;
    error: any;

    ngOnInit() {
        //this.feedService.start(true).subscribe(
        //    connectionState => {
        //        if (connectionState == SignalRConnectionStatus.Connected) {
        //            this.feedService.subscribeToFeed(1);
        //            this.alertService.showStickyMessage("HomeComponent - SignalR Connection", "Connected to the Server", MessageSeverity.info);
        //        }
        //    },
        //    error => console.log('Error on init: ' + error));
    }
}
