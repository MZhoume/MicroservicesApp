import { Component, OnInit } from '@angular/core';
import {User} from "../User";
import {Router} from "@angular/router";
import {UserService} from "../user.service";
import 'rxjs/add/operator/toPromise';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.css']
})
export class UserProfileComponent implements OnInit {

    user: User;
    message: string;

    constructor(
        private userService: UserService,
        private router: Router,
    ) { }

  ngOnInit() {
      this.user = this.userService.getUser();
      // if usr log in, redirect to welcome page
      if (this.user === undefined) {
          this.forward('/welcome');
      }else {
          console.log('please modify');
      }
  }
    forward(dest: string) {
        this.router.navigate([dest]);
    }

    async onSubmit(): Promise<any> {
        console.log("modify submitted");
        this.message = 'Loading';
        try {
            let modifyResult = await this.userService.modifyUserInfoRemote(this.user);
            console.log(modifyResult);
            if (modifyResult.result  === 'success') {

                this.message ='modify success';
                console.log('modify success');

            } else {
                console.log(modifyResult);
                //this.message = response.reason;
                console.log('modify fail');
            }
        } catch (ex) {
            console.error('An error occurred', ex);
        }
    }



}
