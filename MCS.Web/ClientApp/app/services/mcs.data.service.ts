import { Injectable } from '@angular/core';
import { Router, NavigationExtras } from "@angular/router";
import { Http, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/observable/forkJoin';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';

import { MCSDataServiceEndpoint } from './mcs.dataservice.endpoint.service';
import { AuthService } from './auth.service';
import { EventHistory, SystemMap } from '../models/mcs.model';

@Injectable()
export class MCSDataService {
    constructor(private router: Router, private http: Http, private authService: AuthService,
        private eventHistoryEndpoint: MCSDataServiceEndpoint) {
    }

    getEventHistory(stationId?: number) {
        return this.eventHistoryEndpoint.getEventHistoryEndpoint(stationId)
            .map((response: Response) => <EventHistory[]>response.json());
    }

    getEventHistoryAll(stationId: number): Observable<EventHistory[]> {
        return this.http.get('/api/mcs/eventhistory/all/' + stationId)
            .map(this.extractData)
            .catch(this.handleError);
    }

    getSystemMap(): Observable<SystemMap[]> {
        return this.http.get('/api/mcs/systemmap/all')
            .map(this.extractData)
            .catch(this.handleError);
    }

    get currentUser() {
        return this.authService.currentUser;
    }

    private extractData(res: Response) {
        let body = res.json();
        return body || [];
    }

    private handleError(error: any) {
        // In a real world app, we might use a remote logging infrastructure
        // We'd also dig deeper into the error to get a better message
        let errMsg = (error.message) ? error.message :
            error.status ? `${error.status} - ${error.statusText}` : 'Server error';
        console.error(errMsg); // log to console instead
        return Observable.throw(errMsg);
    }
}