using Project3Travelin.Dtos.EmailDtos;

namespace Project3Travelin.Services.EmailServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequestDto emailRequestDto);
    }
}
