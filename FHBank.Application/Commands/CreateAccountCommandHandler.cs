using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FHBank.Application.Commands
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, bool>
    {
        public Task<bool> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
