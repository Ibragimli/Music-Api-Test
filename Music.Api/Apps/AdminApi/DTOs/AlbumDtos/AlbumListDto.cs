using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.Api.DTOs.AlbumDtos
{
    public class AlbumListDto
    {
        public List<AlbumListItemDto> albumListItems { get; set; }
        public int TotalPage { get; set; }
    }
}
