namespace HotelWebDemo.Services;

public interface ILinkGeneratorSerivce
{
    string GenerateResetPasswordLink(string token);
}