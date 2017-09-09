import { Injectable, Injector } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

import { EndpointFactory } from './endpoint-factory.service';
import { ConfigurationService } from './configuration.service';


@Injectable()
export class MCSDataServiceEndpoint extends EndpointFactory {

    private readonly _historyUrl: string = "/api/mcs/eventhistory/all";

    get historyUrl() { return this.configurations.baseUrl + this._historyUrl; }

    constructor(http: Http, configurations: ConfigurationService, injector: Injector) {

        super(http, configurations, injector);
    }

    getEventHistoryEndpoint(stationId?: number): Observable<Response> {
        let endpointUrl = stationId > 0 ? `${this.historyUrl}/${stationId}` : `${this.historyUrl}`;
        return this.http.get(endpointUrl, this.getAuthHeader())
            .map((response: Response) => {
                return response;
            })
            .catch(error => {
                return this.handleError(error, () => this.getEventHistoryEndpoint(stationId));
            });
    }
}