using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.Api.DTOs.AlbumDtos
{
    public class AlbumPostDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile ImageFiles { get; set; }


    }
    public class AlbumPostValidatorDto : AbstractValidator<AlbumPostDto>
    {
        public AlbumPostValidatorDto()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.ImageFiles).NotEmpty();
        }
    }

}
