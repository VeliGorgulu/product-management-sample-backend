using FluentValidation;
using ProductManagementSample.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSample.Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Email).NotEmpty();
            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.LastName).NotEmpty();
            RuleFor(p => p.PasswordHash).NotEmpty();
            RuleFor(p => p.PasswordSalt).NotEmpty();
            RuleFor(p => p.Status).NotEmpty();
        }
    }
}
