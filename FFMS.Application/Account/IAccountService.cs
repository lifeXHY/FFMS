using System.Threading.Tasks;
using FFMS.Application.Account.Dto;
using FFMS.Application.AutoRegisterService;

namespace FFMS.Application.Account
{
    public interface IAccountService: IDenpendency
    {
        Task<ReturnValueModel> Login(AccountDto input);
    }
}
