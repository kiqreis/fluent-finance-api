using FluentFinance.Core.Requests.Categories;
using FluentValidation;

namespace FluentFinance.Core.Validators.Categories;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
{
  public CreateCategoryValidator()
  {
    RuleFor(c => c.Title)
      .NotEmpty().WithMessage("Title cannot be null or empty")
      .MaximumLength(80).WithMessage("Title cannot exceed 80 characters");
    
    RuleFor(c => c.Description).NotEmpty().WithMessage("Description cannot be null or empty")
      .MaximumLength(255).WithMessage("Description cannot exceed 255 characters");

    RuleFor(c => c.UserId).NotEmpty().WithMessage("Email cannot be null or empty")
      .MaximumLength(160).WithMessage("Email cannot exceed 160 characters");
  }
}