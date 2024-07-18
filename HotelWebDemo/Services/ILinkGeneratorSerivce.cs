namespace HotelWebDemo.Services;

public interface ILinkGeneratorSerivce
{
    string GenerateResetPasswordLink(int userId, string token);
}