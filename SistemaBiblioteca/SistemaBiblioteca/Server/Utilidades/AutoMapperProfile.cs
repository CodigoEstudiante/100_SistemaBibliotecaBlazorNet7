using AutoMapper;
using SistemaBiblioteca.Server.Models;
using SistemaBiblioteca.Shared;
using System.Globalization;

namespace SistemaBiblioteca.Server.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Usuario
            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<UsuarioDTO, Usuario>()
            .ForMember(destino =>
                    destino.EsActivo,
                    opt => opt.MapFrom(src => true)
                );
            #endregion Usuario

            #region Lector
            CreateMap<Lector, LectorDTO>();
            CreateMap<LectorDTO, Lector>()
                 .ForMember(destino =>
                    destino.Estado,
                    opt => opt.MapFrom(src => true)
                );
            #endregion Lector

            #region Categoria
            CreateMap<Categoria, CategoriaDTO>();
            CreateMap<CategoriaDTO, Categoria>()
                .ForMember(destino =>
                    destino.Estado,
                    opt => opt.MapFrom(src => true)
                );
            #endregion Categoria

            #region Libro
            CreateMap<Libro, LibroDTO>();
            CreateMap<LibroDTO, Libro>()
                .ForMember(destino =>
                    destino.Estado,
                    opt => opt.MapFrom(src => true)
                );
            #endregion Libro

            #region EstadoPrestamo
            CreateMap<EstadoPrestamo, EstadoPrestamoDTO>();
            CreateMap<EstadoPrestamoDTO, EstadoPrestamo>()
                 .ForMember(destino =>
                    destino.Estado,
                    opt => opt.MapFrom(src => true)
                );
            #endregion EstadoPrestamo

            #region Prestamo
            CreateMap<Prestamo, PrestamoDTO>();
            CreateMap<PrestamoDTO, Prestamo>()
                 .ForMember(destino =>
                    destino.Estado,
                    opt => opt.MapFrom(src => true)
                );
            #endregion Prestamo
        }
    }
}
