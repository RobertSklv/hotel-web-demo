using HotelWebDemo.Models.Components;
using HotelWebDemo.Models.Database;
using HotelWebDemo.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HotelWebDemo.Services;

public interface ICustomerService
{
    Customer? GetFull(int id);

    Task Upsert(Customer customer, ModelStateDictionary modelState);

    Task<int> Delete(int id);

    Task<PaginatedList<Customer>> GetCustomers(string orderBy, string direction, int page, int pageSize, Dictionary<string, TableFilter>? filters);

    bool CompareResetPasswordToken(Customer customer, string token);

    bool CompareResetPasswordToken(int userId, string token, ModelStateDictionary modelState);

    Customer? CompareResetPasswordToken(ResetPasswordModel model, ModelStateDictionary modelState);

    string UrlEncodeToken(byte[] token);

    byte[] GenerateResetPasswordBytes();

    Task<bool> SendResetPasswordEmail(Customer customer, string subject, string emailTemplate, byte[] token);

    Task<bool> InitiateResetPasswordNewAccountAndNotify(Customer customer);

    Task<bool> InitiateResetPasswordAndNotify(int customerId);

    Task<bool> ResetPassword(ResetPasswordModel model, ModelStateDictionary modelState);
}