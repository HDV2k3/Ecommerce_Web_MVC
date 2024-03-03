using AutoMapper;
using WebBanHang.Data;
using WebBanHang.ViewModels;

namespace WebBanHang.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterVM, KhachHang>();
            //   .ForMember(kh => kh.HoTen, option => option.MapFrom(RegisterVM => RegisterVM.HoTen))
            // .ReverseMap();
        }
    }
}
