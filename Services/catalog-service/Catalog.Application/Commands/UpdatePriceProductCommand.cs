

using MediatR;

namespace Catalog.Application.Commands
{
    public record UpdatePriceProductCommand(Guid ProductId,decimal NewPrice,string currency):IRequest;
}
