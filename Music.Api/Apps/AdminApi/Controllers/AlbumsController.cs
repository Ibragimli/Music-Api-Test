using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Api.DTOs.AlbumDtos;
using Music.Api.Helper;
using Music.Core.Entities;
using Music.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.Api.Controllers
{
    [Route("admin/api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class AlbumsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public AlbumsController(DataContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }
        [HttpPost]
        public IActionResult Create([FromForm] AlbumPostDto albumPost)
        {

            Album album = new Album
            {
                Name = albumPost.Name,
                Image = Filemanager.Save(_env.WebRootPath, "uploads/albums", albumPost.ImageFiles),
            };
            _context.Albums.Add(album);
            _context.SaveChanges();
            return StatusCode(200, album);
        }

        [HttpGet("")]
        public IActionResult GetAll(int page = 1)
        {
            var albums = _context.Albums.Include(x => x.Musics).Where(x => !x.IsDeleted);
            AlbumListDto albumList = new AlbumListDto
            {
                albumListItems = new List<AlbumListItemDto>(),
                TotalPage = (int)Math.Ceiling(albums.Count() / 3d),
            };
            albums = albums.Skip((page - 1) * 3).Take(3);
            albumList.albumListItems = _mapper.Map<List<AlbumListItemDto>>(albums.ToList());
            return Ok(albumList);
        }
    }
}
