using MailKit.Net.Smtp;
using MimeKit;
using Project3Travelin.Dtos.EmailDtos;

namespace Project3Travelin.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(EmailRequestDto emailRequestDto)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin", "ozkanyilmaz.dev@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", emailRequestDto.Email);
            mimeMessage.To.Add(mailboxAddressTo);

            mimeMessage.Subject = "Tur Rezervasyonu Bilgileriniz";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $@"
<div style='font-family: Arial, sans-serif; color: #333; max-width: 600px; margin: auto; border: 1px solid #eaedf1; border-radius: 15px; overflow: hidden; box-shadow: 0 4px 15px rgba(0,0,0,0.05);'>
    
    <div style='background-color: #22c55e; padding: 25px; text-align: center; color: white;'>
        <h2 style='margin: 0; font-size: 24px;'>Rezervasyon Talebiniz Alındı!</h2>
    </div>
    
    <div style='padding: 30px;'>
        <p style='font-size: 16px;'>Merhaba <strong>{emailRequestDto.FirstName} {emailRequestDto.LastName}</strong>,</p>
        <p style='font-size: 15px; line-height: 1.6;'>
            <strong>{emailRequestDto.Title}</strong> turu için yapmış olduğunuz ön rezervasyon talebiniz <strong>{emailRequestDto.BookingStatus}</strong>. Bizi tercih ettiğiniz için teşekkür ederiz!
        </p>
        
        <h3 style='border-bottom: 2px solid #22c55e; padding-bottom: 8px; color: #2b3445; margin-top: 30px;'>Rezervasyon Detaylarınız</h3>
        <ul style='list-style-type: none; padding: 0; font-size: 15px; line-height: 2;'>
            <li><strong>🏷️ Tur Adı:</strong> {emailRequestDto.Title}</li>
            <li><strong>👥 Kişi Sayısı:</strong> {emailRequestDto.PassengerCount} Kişi</li>
            <li><strong>📅 İşlem Tarihi:</strong> {emailRequestDto.BookingDate.ToString("dd MMMM yyyy HH:mm")}</li>
        </ul>
        
        <h3 style='border-bottom: 2px solid #22c55e; padding-bottom: 8px; color: #2b3445; margin-top: 30px;'>İletişim Bilgileriniz</h3>
        <ul style='list-style-type: none; padding: 0; font-size: 15px; line-height: 2;'>
            <li><strong>📞 Telefon:</strong> {emailRequestDto.Phone}</li>
            <li><strong>✉️ E-posta:</strong> {emailRequestDto.Email}</li>
        </ul>
        
        <p style='margin-top: 35px; font-size: 15px; line-height: 1.6; color: #555;'>
            Müşteri temsilcilerimiz, kayıt işlemlerinizi tamamlamak ve detayları görüşmek üzere en kısa sürede sizinle iletişime geçecektir.
        </p>
        
        <p style='font-size: 15px; font-weight: bold; color: #2b3445; margin-top: 20px;'>
            Harika bir tatil geçirmeniz dileğiyle...
        </p>
    </div>
    
    <div style='background-color: #f8f9fa; padding: 15px; text-align: center; font-size: 12px; color: #888;'>
        Bu e-posta bilgilendirme amacıyla otomatik olarak gönderilmiştir. Lütfen bu e-postaya yanıt vermeyiniz.
    </div>
</div>";

            mimeMessage.Body = bodyBuilder.ToMessageBody();

            SmtpClient client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, false);
            await client.AuthenticateAsync("YOUR_EMAIL", "YOUR_API_KEY");
            await client.SendAsync(mimeMessage);
            await client.DisconnectAsync(true);
        }
    }
}
