import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import * as Components from "./Components/all.component";

const Routes: any = [
    {
        path: "",
        component: Components.PageHomeComponent,
    },
    {
        path: "Account",
        component: Components.PageAccountComponent,
    },
];

@NgModule({
    imports: [RouterModule.forRoot(Routes)],
    exports: [RouterModule],
})
export class AdminRoutingModule { }