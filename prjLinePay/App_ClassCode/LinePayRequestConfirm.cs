using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjLinePay.App_ClassCode
{
    public class LinePayRequestConfirm
    {
        public class LienPayRequest
        {
            public int amount { get; set; }
            public string productImageUrl { get; set; }
            public string confirmUrl { get; set; }
            public string productName { get; set; }
            public string orderId { get; set; }
            public string currency { get; set; }
        }

        public class PaymentUrl
        {
            public string web { get; set; }
            public string app { get; set; }
        }

        public class Info
        {
            public PaymentUrl paymentUrl { get; set; }
            public long transactionId { get; set; }
            public string paymentAccessToken { get; set; }
        }

        public class RootObject
        {
            public string returnCode { get; set; }
            public string returnMessage { get; set; }
            public Info info { get; set; }
        }

        public class ConfirmPayInfo
        {
            public string Confirmmethod { get; set; }
            public int Confirmamount { get; set; }
            public string ConfirmmaskedCreditCardNumber { get; set; }
        }

        public class ConfirmInfo
        {
            public long ConfirmtransactionId { get; set; }
            public string ConfirmorderId { get; set; }
            public List<ConfirmPayInfo> ConfirmpayInfo { get; set; }
        }

        public class ConfirmObject
        {
            public string ConfirmreturnCode { get; set; }
            public string ConfirmreturnMessage { get; set; }
            public Info Confirminfo { get; set; }
        }

    }
}