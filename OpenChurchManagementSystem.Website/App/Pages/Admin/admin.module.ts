import {NgModule} from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { RouterModule } from "@angular/router";

import * as Components from "./Components/all.component";
import { AdminRoutingModule } from "./admin.router";

@NgModule({
    imports: [
        BrowserModule,
        AdminRoutingModule,
    ],
    declarations: [Components.HomeComponent, Components.ShellComponent, Components.SidebarComponent],
    entryComponents: [Components.HomeComponent],
    bootstrap: [Components.ShellComponent],
})
export class AdminModule {

}