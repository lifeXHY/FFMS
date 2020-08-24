using System.Linq;
using System.Threading.Tasks;
using FFMS.Application.AutoRegisterService;
using FFMS.Application.Bill.Dto;
using FFMS.EntityFrameWorkCore.Entitys;

namespace FFMS.Application.Bill
{
    public interface IAccountBillService: IDenpendency
    {
        Task<ReturnValueModel> Create(AccountBillDto input);
        Task<ReturnValueModel> Update(UpdateAccountBillDto input);
        Task<ReturnValueModel> Delete(int Id);
        IQueryable<AccountBill> GetAllBillsQuery(SearchAccountBillDto input);
        Task<AccountBill> GetBillEntity(int Id);
    }
}
