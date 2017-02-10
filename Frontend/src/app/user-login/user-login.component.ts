import { Component, OnInit } from '@angular/core';
import {User} from "../User";
import {UserService} from "../user.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})


export class UserLoginComponent implements OnInit {
    user: User;
    message: string;

    constructor(
        private userService: UserService,
        private router: Router,
    ) { }

    ngOnInit() {
        this.user = new User();
        // if usr log in, redirect to welcome page
        if (this.userService.getUser() == undefined){
            console.log("please log in");
        }else {
            this.forward();
        }
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
}
