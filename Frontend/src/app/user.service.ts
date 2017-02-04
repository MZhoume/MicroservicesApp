import { Injectable } from '@angular/core';
import {User} from "./User";
import {USER_Response1} from './mock-user';

@Injectable()
export class UserService {

    user: User;

    getUser(): User {
        return this.user;
    }

    getOffUser(): void {
        this.user = undefined;
    }

    loginUserRemote(): Promise<any> {
        return Promise.resolve(USER_Response1).then(
            (response) => {
                if (response.flag == 'success'){
                    console.log(this.user);
                    this.user = new User();
                    this.user.JWT = response.user.JWT;
                    this.user.name = response.user.name;
                    this.user.uid = response.user.uid;
                    return Promise.resolve(response)
                }else{
                    return Promise.resolve(response)
                }
            }
        ).catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }

    constructor() { }

}
