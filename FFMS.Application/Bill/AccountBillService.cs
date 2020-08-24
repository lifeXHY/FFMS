using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FFMS.Application.Bill.Dto;
using FFMS.Application.Common;
using FFMS.EntityFrameWorkCore.Entitys;
using Wei.Repository;

namespace FFMS.Application.Bill
{
    public class AccountBillService:IAccountBillService
    {
        #region 构造函数注入
        private readonly IMapper _mapper;
        private readonly IRepository<AccountBill> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public AccountBillService(IMapper mapper, IUnitOfWork unitOfWork, IRepository<AccountBill> repository)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region CRUD
        public async Task<ReturnValueModel> Create(AccountBillDto input)
        {
            string strMessage = string.Empty;
            bool IfSuccess = true;
            ReturnValueModel model = new ReturnValueModel();
            try
            {
                var bill = _mapper.Map<AccountBill>(input);
                await _repository.InsertAsync(bill);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                IfSuccess = false;
                strMessage = ex.Message;
            }
            model.IfSuccess = IfSuccess;
            model.Message = strMessage;
            return model;
        }


        public async Task<ReturnValueModel> Update(UpdateAccountBillDto input)
        {
            string strMessage = string.Empty;
            bool IfSuccess = true;
            ReturnValueModel model = new ReturnValueModel();
            try
            {
                var bill = _mapper.Map<AccountBill>(input);
                await _repository.UpdateAsync(bill);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                IfSuccess = false;
                strMessage = ex.Message;
            }
            model.IfSuccess = IfSuccess;
            model.Message = strMessage;
            return model;
        }

        public async Task<ReturnValueModel> Delete(int Id)
        {
            string strMessage = string.Empty;
            bool IfSuccess = true;
            ReturnValueModel model = new ReturnValueModel();
            try
            {
                //逻辑删除 标记IsDelete = 1
                await _repository.DeleteAsync(Id);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                IfSuccess = false;
                strMessage = ex.Message;
            }
            model.IfSuccess = IfSuccess;
            model.Message = strMessage;
            return model;
        }

        public IQueryable<AccountBill> GetAllBillsQuery(SearchAccountBillDto input)
        {
            return  _repository.QueryNoTracking()
                    .WhereIf(!string.IsNullOrWhiteSpace(input.ItemType), p => p.ItemType.Contains(input.ItemType))
                    .WhereIf(input.BegDate.HasValue, x => x.AccountDate >= input.BegDate.Value)
                    .WhereIf(input.EndDate.HasValue, x => x.AccountDate <= input.EndDate.Value.AddDays(1).AddSeconds(-1))
                    .Where(x => x.IsDelete == false && x.CreateUserID == input.UserID)
                    ;
        }

        public async Task<AccountBill> GetBillEntity(int Id)
        {
            return await _repository.FirstOrDefaultAsync(x => x.Id == Id);
        }
        #endregion
    }
}
