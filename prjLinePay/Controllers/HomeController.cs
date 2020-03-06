using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using prjLinePay.App_ClassCode;

namespace prjLinePay.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
        //ROUTE改由Create開始
        public ActionResult Create()
        {
            return View();
        }
        //請求付款畫面 
        //使用手機QRCODE測試付款
        //第一階段REQUEST傳送LinePay API所需參數
        [HttpPost]
        public ActionResult Create(string productname)
        {
            LinePayRequestConfirm.LienPayRequest Payrequest = new LinePayRequestConfirm.LienPayRequest
            {
                amount = 1111,
                productImageUrl = "https://i.imgur.com/4TKeElB.jpg",
                confirmUrl = "http://localhost:65309/Home/Confirm",
                productName= productname,
                orderId= Guid.NewGuid().ToString(),
                currency= "TWD"
            };
            using(var client=new HttpClient())
            {

                var requestjson = JsonConvert.SerializeObject(Payrequest);
                HttpContent httpContent = new StringContent(requestjson);
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                httpContent.Headers.Add("X-LINE-ChannelId", "1653922835");
                httpContent.Headers.Add("X-LINE-ChannelSecret", "7fba445413cb5ddd1494db8f4fe4fca9");

                var result = client.PostAsync("https://sandbox-api-pay.line.me/v2/payments/request/", httpContent)
                    .Result
                    .Content
                    .ReadAsStringAsync().Result;
                if (!string.IsNullOrEmpty(result))
                {
                    var item = JsonConvert.DeserializeObject<LinePayRequestConfirm.RootObject>(result);

                    if(item.returnCode == "0000")
                    {
                        var transcationId = item.info.transactionId.ToString();
                        var RedirectpaymentUrl = item.info.paymentUrl.web;
                        return Redirect(RedirectpaymentUrl);
                    }
                }
                else
                {

                }
                return View();
            }
        }
        //接收RESPONSE
        //啟動核對是否付款成功
        //付款成功導向Index的View
        public ActionResult Confirm(string transactionId)
        {
            LinePayRequestConfirm.LienPayRequest confirmquest = new LinePayRequestConfirm.LienPayRequest
            {
                amount = 1111,
                currency = "TWD"
            };
            using (var client = new HttpClient())
            {

                var requestjson = JsonConvert.SerializeObject(confirmquest);
                HttpContent httpContent = new StringContent(requestjson);
                httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                httpContent.Headers.Add("X-LINE-ChannelId", "1653922835");
                httpContent.Headers.Add("X-LINE-ChannelSecret", "7fba445413cb5ddd1494db8f4fe4fca9");
                var url = string.Format("https://sandbox-api-pay.line.me/v2/payments/{0}/confirm", transactionId);

                var result = client.PostAsync(url, httpContent)
                    .Result
                    .Content
                    .ReadAsStringAsync().Result;
                if (!string.IsNullOrEmpty(result))
                {
                    var item = JsonConvert.DeserializeObject<LinePayRequestConfirm.ConfirmObject>(result);

                    if (item.ConfirmreturnCode == "0000")
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {

                }
                return View();
            }
        }

    }
}