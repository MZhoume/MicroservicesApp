import { Injectable } from '@angular/core';
import {User} from './User';
import { Headers, RequestOptions } from '@angular/http';
import { Http, Response } from '@angular/http';
import 'rxjs/add/operator/toPromise';
import {USER_Login_Response1, USER_Reg_Response1, USER_Reg_Response2} from './mock-data/mock-user';

@Injectable()
export class UserService {
    private Urll = 'https://6k1n8i5jx5.execute-api.us-east-1.amazonaws.com/prod/users/login';
    private Urlr = 'https://6k1n8i5jx5.execute-api.us-east-1.amazonaws.com/prod/users/signup';
    user: User;

    getUser(): User {
        return this.user;
    }

    getOffUser(): void {
        this.user = undefined;
    }


    async  loginUserRemote(user: User): Promise<any> {
        const headers = new Headers({'Content-Type': 'application/json'});
        const options = new RequestOptions({ headers: headers });

        try {
            const res = await this.http.post(this.Urll, { Password : user.password, Email : user.email }, options).toPromise();
            console.log(res);
            this.user = new User();
            this.user.JWT = res.json().payload.JWT;
            this.user.firstname = res.json().payload.firstname;
            this.user.lastname = res.json().payload.lastname;
            console.log(res.json());
            return res.json();
        } catch (ex) {
            this.handleError(ex);
        }
    }



    async registerUserRemote(user: User): Promise<any> {
        console.log('enter register');
        const headers = new Headers({ 'Content-Type': 'application/json' });
        const options = new RequestOptions({ headers: headers });

        try {
            const res = await this.http.post(this.Urlr, { FirstName: user.firstname, LastName: user.lastname,
                Password : user.password, Email : user.email, PhoneNumber: user.phone }, options)
                .toPromise();
            console.log(res.json());
            return true;
        } catch (ex) {
            console.log(ex);
            // this.handleError(ex);
            return false;
        }
    }



    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }

    constructor (private http: Http) {
    }

}

