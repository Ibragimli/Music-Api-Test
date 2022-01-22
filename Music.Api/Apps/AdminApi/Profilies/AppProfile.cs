using AutoMapper;
using Music.Api.DTOs.AlbumDtos;
using Music.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.Api.Profilies
{
    public class AppProfile:Profile
    {
        public AppProfile()
        {
            CreateMap<Album, AlbumListItemDto>();
        }
    }
}
