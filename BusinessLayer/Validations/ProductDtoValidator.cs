using CoreLayer.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Validations
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("Bu alan boş geçilemez").NotEmpty();
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("En fazla 50 karakter girilmelidir").NotEmpty();
            RuleFor(x => x.Name).MinimumLength(2).WithMessage("En az 2 karakter girilmelidir").NotEmpty();

            RuleFor(x => x.Title).NotNull().WithMessage("Bu alan boş geçilemez").NotEmpty();

            RuleFor(x => x.Price).InclusiveBetween(1, int.MaxValue).WithMessage("Girilen değer 0'dan büyük olmalı");
        }
    }
}
