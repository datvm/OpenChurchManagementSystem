import { NgModule } from "@angular/core";

import { ShellComponent } from "./shell.component";
import { SidebarComponent } from "./sidebar.component";
import { PageHomeComponent } from "./page.home.component";
import { AccountPanelComponent } from "./account-panel.component";
import { PageAccountComponent } from "./page.account.component";

export * from "./page.home.component";
export * from "./shell.component";
export * from "./sidebar.component";
export * from "./account-panel.component";
export * from "./page.account.component";

export const AllComponents: any[] = [
    ShellComponent,
    SidebarComponent,
    PageHomeComponent,
    AccountPanelComponent,
    PageAccountComponent,
];