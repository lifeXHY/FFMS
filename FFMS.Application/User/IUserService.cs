using System.Threading.Tasks;
using FFMS.Application.AutoRegisterService;
using FFMS.Application.User.Dto;

namespace FFMS.Application.User
{
    public interface IUserService: IDenpendency
    {
        Task<ReturnValueModel> CreateNewUser(BasUserDto input);

        Task<ReturnValueModel> UpdateUser(UpdateBasUserDto input);
        Task<ReturnValueModel> ChangeUserPassword(ChangePasswordDto input);
    }
}
