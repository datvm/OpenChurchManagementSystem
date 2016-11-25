import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { RouterModule } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { HttpModule } from '@angular/http';

import { DataTableModule } from "./../../Libs/DataTable/datatable.module";

import * as Components from "./Components/all.component";
import { AdminRoutingModule } from "./admin.router";
import { IdentityService } from "./Services/identity.service";

@NgModule({
    imports:
    [
        BrowserModule,
        AdminRoutingModule,
        FormsModule,
        HttpModule,
        DataTableModule,
    ],
    providers: [IdentityService],
    declarations: Components.AllComponents,
    bootstrap: [Components.ShellComponent],
})
export class AdminModule {

}