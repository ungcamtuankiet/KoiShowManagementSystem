using Repository.Dtos.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailDTO request);
        Task SendEmailRegisterMemberAccount(string email);
        Task SendEmailAddNewKoi(string email);
        Task SendApprovalAddNewKoi(string email);
        Task SendRejectAddNewKoi(string email, string reason);
    }
}
