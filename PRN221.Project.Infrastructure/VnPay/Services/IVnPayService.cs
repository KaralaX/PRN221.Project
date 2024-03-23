using Microsoft.AspNetCore.Http;
using PRN221.Project.Infrastructure.VnPay.Models;

namespace PRN221.Project.Infrastructure.VnPay.Services;

public interface IVnPayService
{
    string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
    PaymentResponseModel PaymentExecute(IQueryCollection collections);
}