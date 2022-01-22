using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music.Api.Apps.AdminApi.DTOs.AccountDtos
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty().MaximumLength(50).MinimumLength(5);
            RuleFor(x => x.Password).NotEmpty().MaximumLength(25).MinimumLength(8);
        }
    }
}
