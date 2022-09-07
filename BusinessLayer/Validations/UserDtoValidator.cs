using CoreLayer.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validations
{
    public class UserDtoValidator : AbstractValidator<User>
    {
        public UserDtoValidator()
            {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Yazar adı soyadı boş geçilemez");
            RuleFor(x => x.Mail).NotEmpty().WithMessage("Mail adresi boş geçilemez");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş geçilemez");
            RuleFor(x => x.UserName).MinimumLength(2).WithMessage("En az 2 karakter girişi yapınız");
            RuleFor(x => x.UserName).MaximumLength(50).WithMessage("En fazla 50 karakter girişi yapınız");
            }
    }
}
