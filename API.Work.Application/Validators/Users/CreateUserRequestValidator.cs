using API.Work.Application.Commands.Users;
using API.Work.Application.Contract.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Work.Application.Validators.User
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.CreateUserDto.UserName).NotEmpty().WithMessage("Username is required.");
            RuleFor(x => x.CreateUserDto.UserEmail).EmailAddress().WithMessage("Invalid email.");
        }
    }

}
