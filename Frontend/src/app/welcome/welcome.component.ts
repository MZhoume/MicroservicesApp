import { Component, OnInit } from '@angular/core';
import {UserService} from "../user.service";
import {User} from "../User";
import {Router} from "@angular/router";

@Component({
    selector: 'app-welcome',
    templateUrl: './welcome.component.html',
    styleUrls: ['./welcome.component.css']
})
export class WelcomeComponent implements OnInit {
    user: User;

    constructor(private userService: UserService,
                private router: Router,
    ) { }


    ngOnInit() {
        this.user = new User();
        // prevent unlog usr get in
        if (this.userService.getUser() == undefined){
            this.router.navigate(['/']);
            console.log("you should not be here")
        }else {
            this.user = this.userService.getUser();
        }
    }

}
