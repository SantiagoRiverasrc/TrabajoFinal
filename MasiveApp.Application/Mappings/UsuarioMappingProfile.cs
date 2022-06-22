using AutoMapper;
using MasiveApi.Api.Data;
using MasiveApp.Application.Request;
using MasiveApp.Application.Request.Usuario;
using MasiveApp.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasiveApp.Application.Mappings
{
    public class UsuarioMappingProfile : Profile
    {
        public UsuarioMappingProfile()
        {
            CreateMap<CreateUsuarioRequest, Usuario>();
            CreateMap<Usuario, CreateUsuarioRequest>();

            CreateMap<UpdateUsuarioRequest, Usuario>();
            CreateMap<Usuario, UpdateUsuarioRequest>();

            CreateMap<Usuario, UsuarioResponse>();
        }

    }
}
