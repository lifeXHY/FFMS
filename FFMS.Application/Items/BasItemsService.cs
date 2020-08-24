using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FFMS.Application.Common;
using FFMS.Application.Items.Dto;
using FFMS.EntityFrameWorkCore.Entitys;
using Wei.Repository;

namespace FFMS.Application.Items
{
    public class BasItemsService: IBasIemsService
    {
        #region 构造函数注入
        private readonly IMapper _mapper;
        private readonly IRepository<BasItems> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public BasItemsService(IMapper mapper, IUnitOfWork unitOfWork, IRepository<BasItems> repository)
        {
            _mapper = mapper;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region CRUD
        public async Task<ReturnValueModel> Create(BasItemsDto input)
        {
            string strMessage = string.Empty;
            bool IfSuccess = true;
            ReturnValueModel model = new ReturnValueModel();

            try
            {
                var items = _mapper.Map<BasItems>(input);
                var lst = await _repository.GetAllAsync(p => p.ItemType == input.ItemType);

                if (lst.Count() > 0)
                {
                    IfSuccess = false;
                    strMessage = string.Format("收支类型【{0}】 已存在！", input.ItemType);
                }
                else
                {
                    IfSuccess = true;
                    await _repository.InsertAsync(items);
                    await _unitOfWork.SaveChangesAsync();
                }

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

        public async Task<ReturnValueModel> Update(UpdateItemsDto input)
        {
            string strMessage = string.Empty;
            bool IfSuccess = true;
            ReturnValueModel model = new ReturnValueModel();
            try
            {
                var entity =  _repository.QueryNoTracking(p => p.ItemType == input.ItemType).FirstOrDefault();

                if (entity != null && (input.ItemType != input.OldItemType))
                {
                    IfSuccess = false;
                    strMessage = string.Format("收支类型【{0}】 已存在！", input.ItemType);
                }
                else
                {
                    IfSuccess = true;
                    var items = _mapper.Map<BasItems>(input);
                    await _repository.UpdateAsync(items);
                    await _unitOfWork.SaveChangesAsync();
                }
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

        public IQueryable<BasItems> GetAllItemsQuery(SearchItemsDto input)
        {
            return _repository.Query()
                    .WhereIf(input.UserID.HasValue, p => p.Id == input.UserID.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(input.ItemsType), p => p.ItemType.Contains(input.ItemsType))
                    .Where(x => x.IsDelete == false)
                    ;
        }

        public async Task<BasItems> GetItemsEntity(int Id)
        {
            return await _repository.FirstOrDefaultAsync(x => x.Id == Id);
        }
        #endregion
    }
}
