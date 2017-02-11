import { Component, OnInit } from '@angular/core';
import {UserService} from "../user.service";
import {User} from "../User";
import {Router} from "@angular/router";
import {Item} from "../Item";
import {ItemService} from "../item.service";

@Component({
    selector: 'app-welcome',
    templateUrl: './welcome.component.html',
    styleUrls: ['./welcome.component.css']
})
export class WelcomeComponent implements OnInit {
    user: User;
    myToken: any;
    items: Item[];
    message: string;

    constructor(
        private userService: UserService,
        private router: Router,
        private itemService: ItemService,
    ) { }


    async ngOnInit() : Promise<any>{
        this.user = new User();
        // prevent unlog usr get in
        if (this.userService.getUser() == undefined){
            this.router.navigate(['/']);
            console.log("you should not be here")
        }else {
            this.user = this.userService.getUser();
        }
        // get items from server
        try {
            let itemResult = await this.itemService.getItemsRemote(this.user);
            this.items = itemResult;
            console.log('get items success');
        } catch (ex) {
            console.error('An error occurred', ex);
        }
        // this.itemService.getItemsRemote(this.user).then(response => {
        //     if (response.result == 'success') {
        //         this.items = response.items;
        //         console.log('get items success');
        //     } else {
        //         console.log(response);
        //         this.message = 'get items fail';
        //         console.log('get items fail');
        //     }
        // });

    }


    openCheckout(price:number,id: string, descr: string): void{
        let handler = (<any>window).StripeCheckout.configure({
            key: 'pk_test_XGmc8VOUVttNbHcEyQhodzwX',
            locale: 'auto',
            token: (token: any) => {
                console.log(token);
                this.myToken = token.id;
                // TODO: send to server
                this.itemService.sendTokenToServer(this.myToken, this.user.JWT, id);
                console.log('pay end.');
            }
        });

        handler.open({
            name: 'Please Pay',
            description: descr,
            amount: Number(price) * 100,
        });

        console.log('pay start');
    }



}
