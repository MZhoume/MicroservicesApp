import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing/app-routing.module';
import { UserLoginComponent } from './user-login/user-login.component';
import {UserService} from "./user.service";
import { WelcomeComponent } from './welcome/welcome.component';
import { SignOutComponent } from './sign-out/sign-out.component';
import { RegisterComponent } from './register/register.component';
import {ItemService} from "./item.service";
import { NavigationBarComponent } from './navigation-bar/navigation-bar.component';
import { ShoppingComponent } from './shopping/shopping.component';
import { FooterComponent } from './footer/footer.component';


@NgModule({
    declarations: [
        AppComponent,
        UserLoginComponent,
        WelcomeComponent,
        SignOutComponent,
        RegisterComponent,
        NavigationBarComponent,
        ShoppingComponent,
        FooterComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        AppRoutingModule
    ],
    providers: [UserService, ItemService],
    bootstrap: [AppComponent]
})


export class AppModule { }
