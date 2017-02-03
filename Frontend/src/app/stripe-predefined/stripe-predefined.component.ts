import { Component, OnInit } from '@angular/core';

@Component({
    moduleId: module.id,
    selector: 'app-stripe-predefined',
    templateUrl: './stripe-predefined.component.html',
    styleUrls: ['./stripe-predefined.component.css']
})
export class StripePredefinedComponent implements OnInit {
    price: number;
    myToken: any;

    constructor() { }

    ngOnInit() {

    }

    openCheckout(): void{
        let handler = (<any>window).StripeCheckout.configure({
            key: 'pk_test_XGmc8VOUVttNbHcEyQhodzwX',
            locale: 'auto',
            token: (token: any) => {
                // You can access the token ID with `token.id`.
                // Get the token ID to your server-side code for use.
                console.log(token);
                this.myToken = token.id;
            }
        });

        handler.open({
            name: 'Demo Site',
            description: '2 widgets',
            amount: Number(this.price) * 100,
        });

        console.log('finished');

    }
}
