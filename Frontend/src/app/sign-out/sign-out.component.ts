import { Component, OnInit } from '@angular/core';
import {UserService} from "../user.service";
import {Router} from "@angular/router";
import {User} from "../User";

@Component({
    selector: 'app-sign-out',
    templateUrl: './sign-out.component.html',
    styleUrls: ['./sign-out.component.css']
})
export class SignOutComponent implements OnInit {
    user: User;
    message: string;

    constructor(
        private userService: UserService,
        private router: Router,
    ) { }

    ngOnInit() {
        this.message = "loading";
        this.user = new User();
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

    onSubmit() {
        console.log("going to log in");
        this.message = 'Loading';
        this.userService.loginUserRemote(this.user).then(response => {
            if (response.flag == 'success') {
                this.user = this.userService.getUser();
                this.message ='login success';
                console.log('login success');
                this.forward();
            } else {
                console.log(response);
                this.message = response.reason;
                console.log('login fail');
            }
        });

    }

    forward() {
        this.router.navigate(['/welcome']);
    }

    onClick() {
        this.router.navigate(['/register']);
    }

}
