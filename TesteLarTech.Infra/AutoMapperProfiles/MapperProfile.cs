using AutoMapper;
using TesteLarTech.Domain.Entities;
using TesteLarTech.Domain.ViewModels;

namespace TesteLarTech.Infra.AutoMapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<PessoaViewModel, Pessoa>().ReverseMap();
            CreateMap<TelefoneViewModel, Telefone>().ReverseMap();

        }

    }
}
