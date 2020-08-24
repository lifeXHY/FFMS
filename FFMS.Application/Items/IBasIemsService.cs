using System.Linq;
using System.Threading.Tasks;
using FFMS.Application.AutoRegisterService;
using FFMS.Application.Items.Dto;
using FFMS.EntityFrameWorkCore.Entitys;

namespace FFMS.Application.Items
{
    public interface IBasIemsService: IDenpendency
    {
        Task<ReturnValueModel> Create(BasItemsDto input);
        Task<ReturnValueModel> Delete(int Id);
        Task<ReturnValueModel> Update(UpdateItemsDto input);
        IQueryable<BasItems> GetAllItemsQuery(SearchItemsDto search);
        Task<BasItems> GetItemsEntity(int Id);
    }
}
