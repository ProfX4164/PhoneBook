using AutoMapper;

namespace PhoneBook.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Models.Person, DTO.Person>().ReverseMap();
            CreateMap<Models.Phone, DTO.Phone>().ReverseMap();
        }
    }
}
