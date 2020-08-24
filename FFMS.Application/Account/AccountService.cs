using System.Threading.Tasks;
using FFMS.Application.Account.Dto;
using FFMS.Application.Common;
using FFMS.EntityFrameWorkCore.Entitys;
using Wei.Repository;

namespace FFMS.Application.Account
{
    public class AccountService:IAccountService
    {
        private readonly IRepository<BasUser> _repository;

        public AccountService(IRepository<BasUser> repository)
        {
            _repository = repository;
           
        }

        public async Task<ReturnValueModel> Login(AccountDto input)
        {
            var entity = await _repository.FirstOrDefaultAsync(x => x.UserName == input.UserName);
            string strMessage = string.Empty;
            bool IfSuccess = true;
            ReturnValueModel model = new ReturnValueModel();
            if (entity != null && PasswordHasher.VerifyHashedPassword(input.PassWord, entity.PassWord))
            {
                if (!entity.IsActive)
                {
                    strMessage = "该用户已被锁定！请联系管理员！";
                    IfSuccess = false;
                }
                else
                {
                    IfSuccess = true;
                    model.Model = entity;
                }
               
            }
            else
            {
                strMessage = "用户名或密码错误，请重新输入！";
                IfSuccess = false;
            }
            model.IfSuccess = IfSuccess;
            model.Message = strMessage;
            return model;
        }

        
    }
}
