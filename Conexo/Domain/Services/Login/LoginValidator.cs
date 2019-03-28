using System;
using Common.Dependencies.Resources;
using Domain.Models.Request;
using FluentValidation;

namespace Domain.Services.Login
{
    class LoginValidator : AbstractValidator<LoginRequestModel>
    {
        public LoginValidator(IResourcesDependency resourceDependency)
        {
            RuleFor(pp => pp.UserName).NotNull().NotEmpty().WithMessage(resourceDependency.ResolveString("UserValidation"));
            RuleFor(pp => pp.Password).NotNull().NotEmpty().WithMessage(resourceDependency.ResolveString("PasswordValidation"));
        }
    }
}
