using FluentValidation;
using OrderReadApi.Endpoints.GetOrders;

namespace OrderReadApi.Validators
{
    public class GetOrdersRequestValidator : AbstractValidator<GetOrdersRequest>
    {
        public GetOrdersRequestValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0).LessThan(100);
        }
    }
}