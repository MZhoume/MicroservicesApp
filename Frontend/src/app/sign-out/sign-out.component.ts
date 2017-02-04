import { Component, OnInit } from '@angular/core';
import {UserService} from "../user.service";
import {Router} from "@angular/router";

@Component({
    selector: 'app-sign-out',
    templateUrl: './sign-out.component.html',
    styleUrls: ['./sign-out.component.css']
})
export class SignOutComponent implements OnInit {

    message: string;

    constructor(
        private userService: UserService,
        private router: Router,
    ) { }

    ngOnInit() {
        this.message = "loading";
        // prevent unlog usr get in
        if (this.userService.getUser() == undefined){
            this.router.navigate(['/']);
            console.log("you should not be here")
        }else {
            this.signout();
        }
    }

    signout(){
        this.userService.getOffUser();
        this.message ="Success log out"
    }

}
