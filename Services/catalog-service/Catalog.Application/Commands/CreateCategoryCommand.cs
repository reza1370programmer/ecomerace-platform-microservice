

using MediatR;

namespace Catalog.Application.Commands
{
    public record CreateCategoryCommand(string Name) : IRequest<Guid>;


}
