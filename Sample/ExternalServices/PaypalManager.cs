using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayPal.Api;

using iPractice.Models.DTO;

namespace iPractice
{
   public class PaypalManager
    {
        private PayPal.Api.Payment payment;
        public Payment CreatePayment(APIContext apiContext, string redirectUrl, PricingPlanDto objInvoiceItem)
        {
            //similar to credit card create itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };
            itemList.items.Add(new Item()
            {
                name = objInvoiceItem.PlanName,
                currency = "USD",
                quantity = "1",
                sku = "sku",
                price = objInvoiceItem.PlanPricing.ToString()              
            });

            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            // similar as we did for credit card, do here and create details object
            var details = new Details()
            {                
                subtotal = objInvoiceItem.PlanPricing.ToString()
            };

            // similar as we did for credit card, do here and create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = (Convert.ToDouble(objInvoiceItem.PlanPricing)).ToString(),                
                details =details         
            };

            var transactionList = new List<Transaction>();
            Random rnd = new Random();
            int invoiceID = rnd.Next(1, 1300);
            transactionList.Add(new Transaction()
            {
                description = "Transaction description.",
                invoice_number = invoiceID.ToString(),
                amount = amount,
                item_list = itemList
            });

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return this.payment.Create(apiContext);

        }
        public Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }
    }
}
