import { Injectable } from '@angular/core';
import { Cart } from "./Cart";
import {Item} from "./Item";
import { Headers, RequestOptions, URLSearchParams} from '@angular/http';
import { Http, Response } from '@angular/http';



@Injectable()
export class CartService {

    myCart: Cart;

    constructor(private http: Http) {
        console.log("initial cart");
        this.initialCart();
    }

    initialCart():void {
        // todo get cart from server
        this.myCart = new Cart();
    }

    addToCart(item:Item, num:number): void {
        // todo send to server
        this.myCart.addToLocalCart(item, num);
        console.log(this.myCart)
    }

    updateCart(keys: string[], items: Item[], nums: number[]) {
        // todo send to server
        this.myCart.updateLocalCart(keys, items, nums);
    }


    getCartContent(items: Item[], nums: number[], keys: string[]) {
        for (let key of this.myCart.count.keys()){
            keys.push(key);
            nums.push(this.myCart.count.get(key));
            items.push(this.myCart.content.get(key));
        }
    }


    getCartTotalPrice():number  {
        return this.myCart.getTotalPrice();
    }


    clearCart() {
        this.myCart = new Cart();
    }

    private UrlToken = 'https://6k1n8i5jx5.execute-api.us-east-1.amazonaws.com/prod/payments/';

    async checkoutCart(JWT: string, StripToken: string, Charge: number) : Promise<any> {
        let headers = new Headers({ 'Content-Type': 'application/json',
                                    'Authorization': 'Bearer '+JWT});
        let options = new RequestOptions({ headers: headers });

        try {
                let res = await this.http.post(this.UrlToken,  { StripToken : StripToken, Charge : Charge }, options).toPromise();
                console.log(res);
                return res.json();
            } catch (ex) {
                console.log(ex);
                this.handleError(ex);
            }

        // send order to server
        this.clearCart();
    }

    async sendOrderToServer(JWT: string, items: string[], numbers: string[]): Promise<any> {
        try{

        } catch (ex) {
            console.log(ex);
            this.handleError(ex);
        }
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error); // for demo purposes only
        return Promise.reject(error.message || error);
    }

}
