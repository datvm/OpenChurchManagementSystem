import { NgModule } from "@angular/core";

import { ShellComponent } from "./shell.component";
import { SidebarComponent } from "./sidebar.component";
import { HomeComponent } from "./home.component";

export * from "./home.component";
export * from "./shell.component";
export * from "./sidebar.component";

export const AllComponents: any[] = [
    ShellComponent,
    SidebarComponent,
    HomeComponent,
];