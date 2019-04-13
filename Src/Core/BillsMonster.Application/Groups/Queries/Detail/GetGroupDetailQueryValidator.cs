using FluentValidation;

namespace BillsMonster.Application.Groups.Queries.Detail
{
    public class GetGroupDetailQueryValidator : AbstractValidator<GetGroupDetailQuery>
    {
        public GetGroupDetailQueryValidator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }
}
