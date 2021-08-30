using FluentValidation;
using ProductManagementSample.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSample.Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.CategoryId).NotEmpty();
            RuleFor(p => p.Price).NotEmpty();
            RuleFor(p => p.Stock).NotEmpty();
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.ProductName).MaximumLength(50);
            RuleFor(p => p.Price).GreaterThan(0);
            RuleFor(p => p.Stock).GreaterThanOrEqualTo(0);
        }
    }
}
