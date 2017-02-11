import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import {UserLoginComponent} from "../user-login/user-login.component";
import {WelcomeComponent} from "../welcome/welcome.component";
import {SignOutComponent} from "../sign-out/sign-out.component";
import {RegisterComponent} from "../register/register.component";


const routes: Routes = [
    { path: '',  component: UserLoginComponent },
    { path: 'welcome',  component: WelcomeComponent },
    { path: 'signout',  component: SignOutComponent },
    { path: 'register',  component: RegisterComponent },
];


@NgModule({
    imports: [
        CommonModule,
        RouterModule.forRoot(routes)
    ],
    exports:[
        RouterModule,
    ],
    declarations: []
})
export class AppRoutingModule { }
