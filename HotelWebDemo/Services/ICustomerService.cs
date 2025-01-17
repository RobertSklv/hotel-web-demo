﻿using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HotelWebDemo.Services;

public interface ICustomerService : ICrudService<Customer, CustomerViewModel>
{
    Task Upsert(CustomerViewModel customerViewModel, ModelStateDictionary modelState);

    bool CompareResetPasswordToken(Customer customer, string token);

    Task<bool> CompareResetPasswordToken(int userId, string token, ModelStateDictionary modelState);

    Task<Customer?> CompareResetPasswordToken(ResetPasswordModel model, ModelStateDictionary modelState);

    string UrlEncodeToken(byte[] token);

    byte[] GenerateResetPasswordBytes();

    Task<bool> SendResetPasswordEmail(Customer customer, string subject, string emailTemplate, byte[] token);

    Task<bool> InitiateResetPasswordNewAccountAndNotify(Customer customer);

    Task<bool> InitiateResetPasswordAndNotify(int customerId);

    Task<bool> ResetPassword(ResetPasswordModel model, ModelStateDictionary modelState);

    Task<Customer?> GetByNationalId(string nationalId);

    Task<Customer?> GetByPassportId(string passportId);
}