using FluentValidation;
using OrderReadApi.Endpoints.GetOrderById;

namespace OrderReadApi.Validators
{
    public class GetOrderByIdRequestValidator : AbstractValidator<GetOrderByIdRequest>
    {
        public GetOrderByIdRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
