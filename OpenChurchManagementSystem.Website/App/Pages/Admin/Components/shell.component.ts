import { Component } from "@angular/core";
import { IdentityService } from "./../Services/identity.service";

@Component({
    moduleId: module.id,
    selector: "admin",
    templateUrl: "/Admin/Template/Shell",
})
export class ShellComponent {

    public Title: string = "Dashboard";

    constructor(public Identity: IdentityService) {

    }
    
}