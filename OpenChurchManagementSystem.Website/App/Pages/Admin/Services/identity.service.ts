import { Injectable } from "@angular/core";
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import "rxjs/add/operator/map";

@Injectable()
export class IdentityService {
    
    public Authorized: boolean = false;
    public Username: string;

    constructor(private HttpService: Http) {
        this.RefreshState();
    }

    public RefreshState() {
        this.HttpService.get("/api/v1/account/ping")
            .forEach(response => {
                this.Authorized = response.status == 200;

                if (this.Authorized) {
                    var result = response.json();
                    this.Username = result.Username;
                }
            });
    }

    public NoticeUnauthorized() {
    }

}