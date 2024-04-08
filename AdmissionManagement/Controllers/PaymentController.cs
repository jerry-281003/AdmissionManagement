using AdmissionManagement.Data;
using AdmissionManagement.Models;
using Microsoft.AspNetCore.Mvc;
using PayPal.Api;

namespace AdmissionManagement.Controllers
{
    public class PaymentController : Controller
    {
        private readonly string _clientId = "AcrXA9KMHUuNr-K_lplWhjv00epr5zxnCUgSzbkwnOxDGEdr--u8UvtlxWPFW8L1P0zK93ym8K44qV3R";
        private readonly string _clientSecret = "EBfWuDfcPiJMG87QE_CeWTeWPl84jPIXVlgg4eVRy3WT4927COzpt17seV5D0YpCv8W9PUGmXaUE8eNH";

        private readonly AdmissionManagement2Context _context;

        public PaymentController(AdmissionManagement2Context context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult CreatePayment(string currency, decimal total)
        {
            var apiContext = PaypalService.GetAPIContext(_clientId, _clientSecret);

            var payment = new Payment
            {
                intent = "sale",
                payer = new Payer { payment_method = "paypal" },
                transactions = new List<Transaction>
        {
            new Transaction
            {
                amount = new Amount
                {
                    currency = currency,
                    total = total.ToString()
                },
                description = "Payment description"
            }
        },
                redirect_urls = new RedirectUrls
                {
                    return_url = Url.Action("ExecutePayment", "Payment", null, Request.Scheme),
                    cancel_url = Url.Action("Index", "Home", null, Request.Scheme)
                }
            };

            var createdPayment = payment.Create(apiContext);

            // Redirect the user to the PayPal payment page
            var redirectUrl = createdPayment.links.Find(l => l.rel.Equals("approval_url")).href;
            return Redirect(redirectUrl);
        }

        public IActionResult ExecutePayment(string paymentId, string token, string PayerID)
        {
            var apiContext = PaypalService.GetAPIContext(_clientId, _clientSecret);

            var paymentExecution = new PaymentExecution { payer_id = PayerID };
            var payment = new Payment { id = paymentId };

            var executedPayment = payment.Execute(apiContext, paymentExecution);

            var cadicate = _context.ApplicationUser.Where(a => a.Email == User.Identity.Name).FirstOrDefault();
            // Xử lý kết quả thanh toán

            var paymentData = new PaymentDetails
            {
                PaymentId = paymentId,
                CadidateName = cadicate.FullName,
                Email = executedPayment.payer.payer_info.email,
                Amount = executedPayment.transactions.FirstOrDefault().amount.total,
                Status = executedPayment.state,
                PaymentDate = DateTime.Now,

            };

            var cadidate = _context.Cadidate.Where(c => c.UserId == User.Identity.Name).FirstOrDefault();
            cadidate.PaymentStatus = true;

			// Lưu paymentData vào cơ sở dữ liệu
			_context.PaymentDetails.Add(paymentData);
            _context.Cadidate.Update(cadidate);
            _context.SaveChanges();

            return View("PaymentSuccess", paymentData);


        }
    }
}
