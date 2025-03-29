using FluentValidation;
using OrderWriteApi.Endpoints.UpdateOrder;

namespace OrderWriteApi.Validators
{
    public class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderRequest>
    {
        public UpdateOrderRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.ProductId)
                .NotEmpty().When(x => x.ProductId != null);

            RuleFor(x => x.CustomerId)
                .NotEmpty().When(x => x.CustomerId != null);

            RuleFor(x => x.Quantity)
                .GreaterThan(0).When(x => x.Quantity != null);
        }
    }
}
