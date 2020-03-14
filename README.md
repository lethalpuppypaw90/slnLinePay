# slnLinePay
第三方支付API練習用

**Controllers/HomeController.cs檔案是主要CODE的內容**

**主要分兩個 Action**



**Create Action [提供API商家及產品資訊做付款動作]**

接收前端string參數，其他參數寫死做測試，使用http物件
加入LINE要的驗證資訊去POST並接收回傳的資訊做解析
，確認成功後導向第二個Action做確認的動作。



**Confirm Action [核對資料庫金額及客戶LINEPAY付款的金額]**

接收帶有LINE交易紀錄參數的URL網址，結合為了測試寫死的
資料庫參數去做第二次的POST，解析回傳資料，正確無誤後導
向我們想要的頁面。
