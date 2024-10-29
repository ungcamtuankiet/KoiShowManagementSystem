using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Repository.Dtos.Email;
using Service.IService;
using MailKit.Net.Smtp;


namespace Service.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(EmailDTO request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration["EmailUserName"]));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = request.Body
            };

            using var smtp = new SmtpClient();
            try
            {
                await smtp.ConnectAsync(_configuration["EmailHost"], 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_configuration["EmailUserName"], _configuration["EmailPassword"]);
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your needs
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }
        }

        //Register Account
        public async Task SendEmailRegisterMemberAccount(string email)
        {
            var emailDto = new EmailDTO
            {
                To = email,
                Subject = "Register Member Account Successfully",
                Body = $@"
                    <html>
                    <body style='font-family: Arial, sans-serif; line-height: 1.6;'>
                        <div style='max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px;'>
                            <h2 style='color: #333;'>Thank you for registering as a member!</h2>
                            <p style='color: #555;'>We have received your registration</p>
                            <p style='color: #555;'>Welcome to our website.</p>
                            <p style='color: #555;'>Best regards,<br />Koi Show System Management</p>
                        </div>
                    </body>
                    </html>"
            };

            await SendEmailAsync(emailDto);
        }

        // Add new koi
        public async Task SendEmailAddNewKoi(string email)
        {
            var emailDto = new EmailDTO
            {
                To = email,
                Subject = "Add New Koi",
                Body = $@"
                    <html>
                    <body style='font-family: Arial, sans-serif; line-height: 1.6;'>
                        <div style='max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px;'>
                            <h2 style='color: #333;'>You just add new koi to my website!</h2>
                            <p style='color: #555;'>I have received your koi information.</p>
                            <p style='color: #555;'>Your koi is pending admin approval.</p>
                            <p style='color: #555;'>We will notify you of the results of your koi addition as soon as possible.</p>
                            <p style='color: #555;'>Thank you for your understanding.</p>
                            <p style='color: #555;'>Best regards,<br />Koi Show System Management</p>
                        </div>
                    </body>
                    </html>"
            };

            await SendEmailAsync(emailDto);
        }

        // Approvel Add New Koi
        public async Task SendApprovalAddNewKoi(string email)
        {
            var emailDto = new EmailDTO
            {
                To = email,
                Subject = "Koi Approval",
                Body = $@"
                    <html>
                    <body style='font-family: Arial, sans-serif; line-height: 1.6;'>
                        <div style='max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px;'>
                            <h2 style='color: #333;'>Congratulations!</h2>
                            <p style='color: #555;'>Your koi has been approved. Your koi may appear on our homepage.</p>
                            <p style='color: #555;'>Thanks for your kind contribution to our website.</p>
                            <p style='color: #555;'>Best regards,<br />Koi Show System Management</p>
                        </div>
                    </body>
                    </html>"
            };

            await SendEmailAsync(emailDto);
        }

        // Reject Add New Account
        public async Task SendRejectAddNewKoi(string email, string reason)
        {
            var emailDto = new EmailDTO
            {
                To = email,
                Subject = "Koi Rejection",
                Body = $@"
                    <html>
                    <body style='font-family: Arial, sans-serif; line-height: 1.6;'>
                        <div style='max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px;'>
                            <h2 style='color: #333;'>Koi Rejection</h2>
                            <p style='color: #555;'>We are sorry we are unable to approve your koi.</p>
                            <p style='color: #555;'>Reason: {reason}</p>
                            <p style='color: #555;'>If you have any questions or need further assistance, please contact our support team.</p>
                            <p style='color: #555;'>Best regards,<br />Koi Show System Management</p>
                        </div>
                    </body>
                    </html>"
            };

            await SendEmailAsync(emailDto);
        }

        public async Task SendAcceptKoiRegistration(string email)
        {
            var emailDto = new EmailDTO
            {
                To = email,
                Subject = "Koi Registration Competition was accepted",
                Body = $@"
                    <html>
                    <body style='font-family: Arial, sans-serif; line-height: 1.6;'>
                        <div style='max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px;'>
                            <h2 style='color: #333;'>Koi Registration Competition was accepted</h2>
                            <p style='color: #555;'>Your koi has been accepted into the competition.</p>
                            <p style='color: #555;'>Please read the contest rules and contest schedule to prepare for the upcoming contest.</p>
                            <p style='color: #555;'>Best regards,<br />Koi Show System Management</p>
                        </div>
                    </body>
                    </html>"
            };

            await SendEmailAsync(emailDto);
        }

        public async Task SendRejectKoiRegistration(string email, string reason)
        {
            var emailDto = new EmailDTO
            {
                To = email,
                Subject = "Koi Registration Competition was rejected",
                Body = $@"
                    <html>
                    <body style='font-family: Arial, sans-serif; line-height: 1.6;'>
                        <div style='max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px;'>
                            <h2 style='color: #333;'>Koi Registration Competition was rejected</h2>
                            <p style='color: #555;'>Your koi registration to competition was reject</p>
                            <p style='color: #555;'>Reason: {reason}</p>
                            <p style='color: #555;'>If you have any questions or need further assistance, please contact our support team.</p>
                            <p style='color: #555;'>Best regards,<br />Koi Show System Management</p>
                        </div>
                    </body>
                    </html>"
            };

            await SendEmailAsync(emailDto);
        }

        public async Task SendRejectKoiRegistrationAuto(string email)
        {
            var emailDto = new EmailDTO
            {
                To = email,
                Subject = "Koi Registration Competition was rejected",
                Body = $@"
                    <html>
                    <body style='font-family: Arial, sans-serif; line-height: 1.6;'>
                        <div style='max-width: 600px; margin: auto; padding: 20px; border: 1px solid #ddd; border-radius: 10px;'>
                            <h2 style='color: #333;'>Koi Registration Competition was rejected</h2>
                            <p style='color: #555;'>We apologize that the contest is full. See you again in the next contest.</p>
                            <p style='color: #555;'>If you have any questions or need further assistance, please contact our support team.</p>
                            <p style='color: #555;'>Best regards,<br />Koi Show System Management</p>
                        </div>
                    </body>
                    </html>"
            };

            await SendEmailAsync(emailDto);
        }
    }
}
