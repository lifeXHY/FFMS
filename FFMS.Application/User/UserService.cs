using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FFMS.Application.Common;
using FFMS.Application.User.Dto;
using FFMS.EntityFrameWorkCore.Entitys;
using Microsoft.EntityFrameworkCore;
using Wei.Repository;

namespace FFMS.Application.User
{
    public class UserService: IUserService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<BasUser> _repository;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IMapper mapper, IUnitOfWork unitOfWork, IRepository<BasUser> repository)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ReturnValueModel> CreateNewUser(BasUserDto input)
        {
            string strMessage = string.Empty;
            bool IfSuccess = true;
            ReturnValueModel model = new ReturnValueModel();
            try
            {
                var user = _mapper.Map<BasUser>(input);
                var query = await _repository.GetAllAsync(x => x.UserName == input.UserName);
                if (query.Count() > 0)
                {
                    IfSuccess = false;
                    strMessage = string.Format("用户名【{0}】 已存在！", input.UserName);
                }
                else
                {
                    //生成加密的密码
                    user.PassWord = PasswordHasher.HashPassword(input.PassWord);
                    await _repository.InsertAsync(user);
                    await _unitOfWork.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                IfSuccess = false;
                strMessage = ex.Message;
            }
            model.IfSuccess = IfSuccess;
            model.Message = strMessage;
            return model;
        }

        public async Task<ReturnValueModel> UpdateUser(UpdateBasUserDto input)
        {
            string strMessage = string.Empty;
            bool IfSuccess = true;
            ReturnValueModel model = new ReturnValueModel();
            try
            {
                var user = _mapper.Map<BasUser>(input);
                user.PassWord = (await _repository.QueryNoTracking(x => x.UserName == user.UserName).FirstOrDefaultAsync()).PassWord;
                await _repository.UpdateAsync(user);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                IfSuccess = false;
                strMessage = ex.Message;
            }
            model.IfSuccess = IfSuccess;
            model.Message = strMessage;
            return model;
        }

        public async Task<ReturnValueModel> ChangeUserPassword(ChangePasswordDto input)
        {
            string strMessage = string.Empty;
            bool IfSuccess = true;
            ReturnValueModel model = new ReturnValueModel();
            try
            {
                var user = await _repository.GetAsync(input.Id);
                if (!PasswordHasher.VerifyHashedPassword(input.OldPassWord, user.PassWord))
                {
                    IfSuccess = false;
                    strMessage = "输入的原始密码不正确！";
                }
                else
                {
                    user.PassWord = PasswordHasher.HashPassword(input.NewPassWord);
                    await _repository.UpdateAsync(user);
                    _unitOfWork.SaveChanges();
                    IfSuccess = true;
                }
            }
            catch (Exception ex)
            {
                IfSuccess = false;
                strMessage = ex.Message;
            }
            model.IfSuccess = IfSuccess;
            model.Message = strMessage;
            return model;
        }
    }
}
