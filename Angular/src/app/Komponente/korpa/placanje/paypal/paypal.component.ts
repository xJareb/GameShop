import {AfterViewInit, Component, EventEmitter, Input, Output} from '@angular/core';

declare var paypal: any;

@Component({
  selector: 'app-paypal',
  standalone: true,
  imports: [],
  templateUrl: './paypal.component.html',
  styleUrl: './paypal.component.css'
})
export class PaypalComponent implements AfterViewInit{

  @Input() amount: number = 0;
  @Output() triggerCreatePurchase = new EventEmitter<void>();

  ngAfterViewInit(): void {
    if (!this.amount || this.amount <= 0) {
      return;
    }

    paypal.Buttons({
      createOrder: (data: any, actions: any) => {
        return actions.order.create({
          purchase_units: [{
            amount: {
              value: this.amount.toString()
            }
          }]
        });
      },
      onApprove: (data: any, actions: any) => {
        return actions.order.capture().then((details: any) => {
          this.triggerCreatePurchase.emit();
        });
      },
      onError: (err: any) => {
        console.error('PayPal error:', err);
      },
      commit: true,
      intent: 'sale'
    }).render('#paypal-button-container');
  }
}
