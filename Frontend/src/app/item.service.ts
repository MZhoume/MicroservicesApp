import { Injectable } from '@angular/core';
import {User} from "./User";
import {Item_Response1} from "./mock-data/mock-movies";

@Injectable()
export class ItemService {

    constructor() { }

    getItemsRemote(user:User): Promise<any> {
        return Promise.resolve(Item_Response1).then(
            (response) => {
                return Promise.resolve(response);
            }
        ).catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }
}
