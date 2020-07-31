using System;
using System.Linq;
using AutoMapper;
using CursoAPI.Domain;
using CursoAPI.Dto;

namespace CursoAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Evento, EventoDto>()
                .ForMember(dest => dest.Palestrantes, opt => {
                    opt.MapFrom(src => src.PalestranteEventos.Select(x => x.Palestrante).ToList());
                });
            CreateMap<Palestrante, PalestranteDto>()
                .ForMember(dest => dest.Eventos, opt => {
                    opt.MapFrom(src => src.PalestranteEventos.Select(x => x.Evento).ToList());
                });
            CreateMap<Lote, LoteDto>();
            CreateMap<RedeSocial, RedeSocialDto>();
        }
    }
}
