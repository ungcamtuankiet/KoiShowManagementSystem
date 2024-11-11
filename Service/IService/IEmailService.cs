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
        Task SendAcceptKoiRegistration(string email);
        Task SendRejectKoiRegistrationAuto(string email);
        Task SendRejectKoiRegistration(string email, string reason);
        Task SendApproveKoi(string email);
        Task SendRejectKoi(string email, string reason);
    }
}
