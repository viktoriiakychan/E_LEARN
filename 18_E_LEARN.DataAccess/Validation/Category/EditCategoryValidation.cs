using _18_E_LEARN.DataAccess.Data.ViewModels.Category;
using _18_E_LEARN.DataAccess.Data.ViewModels.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18_E_LEARN.DataAccess.Validation.Category
{
    public class EditCategoryValidation : AbstractValidator<CategoryVM>
    {
        public EditCategoryValidation()
        {
            RuleFor(r => r.Name).NotEmpty();
        }
    }
}
