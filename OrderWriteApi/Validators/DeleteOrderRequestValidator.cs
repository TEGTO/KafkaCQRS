using FluentValidation;
using OrderWriteApi.Endpoints.DeleteOrder;

namespace OrderWriteApi.Validators
{
    public class DeleteOrderRequestValidator : AbstractValidator<DeleteOrderRequest>
    {
        public DeleteOrderRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
