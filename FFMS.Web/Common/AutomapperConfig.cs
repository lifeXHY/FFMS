using AutoMapper;
using FFMS.Application.Bill.Dto;
using FFMS.Application.Items.Dto;
using FFMS.Application.User.Dto;
using FFMS.EntityFrameWorkCore.Entitys;

namespace FFMS.Application
{
    public class AutomapperConfig: Profile
    {
        public AutomapperConfig()
        {
            CreateMap<BasUser, BasUserDto>();
            CreateMap<BasUserDto, BasUser>();
            CreateMap<UpdateBasUserDto, BasUser>();

            CreateMap<BasItemsDto, BasItems>();
            CreateMap<BasItems, BasItemsDto>();
            CreateMap<UpdateItemsDto, BasItems>() ;
            CreateMap<BasItems, UpdateItemsDto>().ForMember(dest => dest.OldItemType, opt => opt.Ignore());

            CreateMap<AccountBill, AccountBillDto>();
            CreateMap<AccountBillDto, AccountBill>();
            CreateMap<UpdateAccountBillDto, AccountBill>();
            CreateMap<AccountBill, UpdateAccountBillDto>();
            // Use CreateMap... Etc.. here (Profile methods are the same as configuration methods)
        }
    }
}
