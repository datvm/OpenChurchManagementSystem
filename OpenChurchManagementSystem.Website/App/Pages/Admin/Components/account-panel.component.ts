import { Component } from "@angular/core";

import { IdentityService } from "./../Services/identity.service";

@Component({
    moduleId: module.id,
    selector: "account-panel",
    templateUrl: "/Admin/Template/AccountPanel",
})
export class AccountPanelComponent {

    constructor(public Identity: IdentityService) {
    }

}