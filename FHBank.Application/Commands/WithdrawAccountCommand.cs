using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FHBank.Application.Commands
{
    public class WithdrawAccountCommand : IRequest<bool>
    {
        public string Id { get; private set; }
        public decimal Amount { get; private set; }

        public WithdrawAccountCommand(string id, decimal amount)
        {
            Id = id;
            Amount = amount;
        }
    }
}
