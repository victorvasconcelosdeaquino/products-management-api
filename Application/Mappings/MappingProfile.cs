using Domain.ViewModels;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.DTO;

namespace Domain.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap(); 
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.SupplierId, m => m.MapFrom(sourc => sourc.Supplier.Id))
                .ForMember(dest => dest.SupplierName, m => m.MapFrom(sourc => sourc.Supplier.Name))
                .ForMember(dest => dest.CorporateTaxId, m => m.MapFrom(sourc => sourc.Supplier.CorporateTaxId));

            CreateMap<Supplier, SupplierDTO>().ReverseMap();
            CreateMap<Supplier, SupplierViewModel>().ReverseMap();
        }
    }
}
