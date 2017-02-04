import { Component, OnInit } from '@angular/core';
import {User} from "../User";
import {Router} from "@angular/router";
import {UserService} from "../user.service";
@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
    regUser: User;
    message: string;

    constructor(
        private userService: UserService,
        private router: Router,
    ) { }

    ngOnInit() {
        this.regUser = new User();
        // if usr log in, redirect to welcome page
        if (this.userService.getUser() == undefined){
            console.log("please register");
        }else {
            this.forward();
        }
        
    }

    forward() {
        this.router.navigate(['/welcome']);
    }

    onSubmit() {
        console.log("going to register");
        this.message = 'Loading';
        this.userService.registerUserRemote(this.regUser).then(response => {
            if (response.flag == 'success') {
                this.regUser = new User();
                this.message ='register success';
                console.log('register success');
                this.forward();
            } else {
                console.log(response);
                this.message = response.reason;
                console.log('register fail');
            }
        });
    }


}
