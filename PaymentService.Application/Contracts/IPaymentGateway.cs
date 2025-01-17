﻿using PaymentService.Application.Models;

namespace PaymentService.Application.Contracts;
public interface IPaymentGateway
{
    Task<PaystackResponse> MakePayment(double costOfProduct, string productNumber, string customerEmail);

    //Task<VerifyBank> VerifyAccountNumber(string acNumber, string bankCode, decimal amount);

    Task<VerifyBank> VerifyByAccountNumber(string acNumber, string bankCode, decimal amount);

    Task<GenerateRecipientDTO> GenerateRecipients(VerifyBank verifyBank);

    Task<MakeATransfer> SendMoney(string recip, decimal amount);
}